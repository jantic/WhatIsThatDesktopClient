using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Spatial;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
using WhatIsThat.Utilities.ImageProcessing;
using WhatIsThat.WhatIsThatServiceClient.Exceptions;
using WhatIsThat.WhatIsThatServiceClient.Response;
using WhatIsThat.WhatIsThatServiceClient.Response.Dto;

namespace WhatIsThat.WhatIsThatServiceClient
{
    public class WhatIsThatClient
    {
        private readonly string _baseUrl = ConfigurationManager.AppSettings["whatisthat_baseurl"];
        private readonly string _appKey = ConfigurationManager.AppSettings["whatisthat_appkey"];
        private const String ImageIdPath ="/api/IdentifySpecies/";
        private const String ApiInfoPath = "/api/ApiInfo/";
        private const Double MinImageSizeMultiplier = 0.10;

        //Needs lock
        private static WhatIsThatApiInfo _apiInfo;
        private readonly static Object ApiInfoLock = new Object();

        private const Double ThrottleWaitSecondsDefault = 10;

        public WhatIsThatSpeciesCandidatesCollection GetMostLikelyIdentities(Image sourceImage, GeographyPoint coordinates, Rectangle cropArea, Boolean multisample)
        {
            var formattedSourceImage = ImageConversion.ConvertTo24BitColorBitmap(sourceImage);
            //ValidateImage(formattedSourceImage);
            ValidateCropArea(cropArea);
            var croppedImage = ImageConversion.GetImageCrop(formattedSourceImage, cropArea);
            var resizedImage = ResizeImageIfNeeded(croppedImage);
            var imageByteArray = ImageConversion.ImageToJpegByteArray(resizedImage);

            var parameters = new List<Parameter>();
            var latitudeParam = new Parameter
            { 
                Name = "latitude", Value = coordinates.Latitude.ToString(CultureInfo.InvariantCulture),
                Type = ParameterType.QueryString 
            };
            parameters.Add(latitudeParam);

            var longitudeParam = new Parameter 
            { 
                Name = "longitude", Value = coordinates.Longitude.ToString(CultureInfo.InvariantCulture),
                Type = ParameterType.QueryString 
            };
            parameters.Add(longitudeParam);

            var multisampleParam = new Parameter
            { 
                Name = "multisample", Value = multisample.ToString(CultureInfo.InvariantCulture),
                Type = ParameterType.QueryString
            };
            parameters.Add(multisampleParam);

            var result = ExecutePostRequest<WhatIsThatResponseDto>(ImageIdPath, parameters, imageByteArray);
            return new WhatIsThatSpeciesCandidatesCollection(result.SpeciesCandidates);
        }

        private void ValidateCropArea(Rectangle cropArea)
        {
            if (cropArea.Width >= GetMinImageWidth() && cropArea.Height >= GetMinImageHeight()) return;
            var message = "Crop area is too small!  Min width is " + GetMinImageWidth() + " and min height is " +
                          GetMinImageHeight();
            throw new ApplicationException(message);
        }

        public Int32 GetMinImageWidth()
        {
            return Convert.ToInt32(Math.Ceiling(MinImageSizeMultiplier * GetApiInfo().MinImageSize));
        }

        public Int32 GetMinImageHeight()
        {
            return Convert.ToInt32(Math.Ceiling(MinImageSizeMultiplier * GetApiInfo().MinImageSize));
        }

        private Bitmap ResizeImageIfNeeded(Bitmap image)
        {
            var apiInfo = GetApiInfo();
            return ImageConversion.ResizeImageIfNeeded(image, Convert.ToInt32(apiInfo.MinImageSize), Convert.ToInt32(apiInfo.MaxImageSize));
        }

        public WhatIsThatApiInfo GetApiInfo()
        {
            lock (ApiInfoLock)
            {
                if (_apiInfo != null) return _apiInfo.Clone();
                var result = ExecuteGetRequest<WhatIsThatApiInfoResultDto>(ApiInfoPath);
                _apiInfo = new WhatIsThatApiInfo(result);
                return _apiInfo.Clone();
            }
        }

        private T ExecuteGetRequest<T>(String apiPath) where T : new()
        {
            var request = BuildRestRequest(apiPath, Method.GET);
            var client = BuildRestClient();
            return ExecuteRequestRobustly<T>(client, request);
        }

        private T ExecutePostRequest<T>(String apiPath, List<Parameter> parameters, byte[] imageBytes) where T : new()
        {
            var request = BuildRestRequest(apiPath, Method.POST);
            request.Parameters.AddRange(parameters);
            request.AddBody(imageBytes);
            var client = BuildRestClient();
            return ExecuteRequestRobustly<T>(client, request);
        }

        private T ExecuteRequestRobustly<T>(IRestClient client, IRestRequest request) where T : new()
        {
            IRestResponse response = null;
            var attempts = 3;
            var success = false;

            while (attempts > 0 && !success)
            {
                attempts--;
                try
                {
                    success = AttemptRequestExecution(client, request, out response);
                }
                catch (ApiThrottleError e)
                {
                    Thread.Sleep(Convert.ToInt32(e.WaitSeconds * 1000));
                }
            }

            if (response == null)
            {
                const string message = "Error retrieving response- Failed to communicate with WhatIsThat server.";
                var applicationException = new ApplicationException(message);
                throw applicationException;
            }

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var applicationException = new ApplicationException(message, response.ErrorException);
                throw applicationException;
            }

            //Using this json parser instead of RestSharp's implicit parsing in Execute's result
            //because it crashes attempting to parse the results.
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        private Boolean AttemptRequestExecution(IRestClient client, IRestRequest request, out IRestResponse response)
        {
            try
            {
                response = client.Execute(request);
                return true;
            }
            catch (HttpListenerException e)
            {
                switch (e.ErrorCode)
                {
                    case 429:
                        var waitTimeString = e.Data["X-Throttle-Wait-Seconds"].ToString();
                        var waitTime = String.IsNullOrEmpty(waitTimeString)
                            ? ThrottleWaitSecondsDefault
                            : Double.Parse(waitTimeString);
                        throw new ApiThrottleError("The client must wait and try again later.",
                            Convert.ToInt32(waitTime), e);
                }

                response = null;
                return false;
            }
            catch (Exception)
            {
                response = null;
                return false;
            }
        }

        private RestClient BuildRestClient()
        {
            var client = new RestClient { BaseUrl = new Uri(_baseUrl), Authenticator = new HttpBasicAuthenticator("", _appKey)};
            return client;
        }

        private RestRequest BuildRestRequest(String apiPath, Method method)
        {
            var request = new RestRequest(apiPath, method)
            {
                RequestFormat = DataFormat.Json
            };
            return request;
        }
    }
}
