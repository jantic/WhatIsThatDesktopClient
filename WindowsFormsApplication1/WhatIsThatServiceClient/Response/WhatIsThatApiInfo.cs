using System;
using WhatIsThat.WhatIsThatServiceClient.Response.Dto;

namespace WhatIsThat.WhatIsThatServiceClient.Response
{
    public class WhatIsThatApiInfo
    {
        private readonly Double _apiVersion;
        private readonly Int64 _maxImageSize;
        private readonly Int64 _minImageSize;
        private readonly WhatIsThatApiInfoResultDto _sourceDto;

        public WhatIsThatApiInfo(WhatIsThatApiInfoResultDto dto)
        {
            Int64.TryParse(dto.MaxImageSize, out _maxImageSize);
            Int64.TryParse(dto.MinImageSize, out _minImageSize);
            Double.TryParse(dto.ApiVersion, out _apiVersion);
            _sourceDto = dto;
        }

        public Int64 MaxImageSize
        {
            get { return _maxImageSize; }
        }

        public Double ApiVersion
        {
            get { return _apiVersion; }
        }

        public Int64 MinImageSize
        {
            get { return _minImageSize; }
        }

        public WhatIsThatApiInfo Clone()
        {
           return new WhatIsThatApiInfo(_sourceDto); 
        }
    }
}