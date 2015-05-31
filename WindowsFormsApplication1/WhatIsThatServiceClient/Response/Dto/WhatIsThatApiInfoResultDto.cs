using System;
using System.Diagnostics.CodeAnalysis;

namespace WhatIsThat.WhatIsThatServiceClient.Response.Dto
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [Serializable]
    public class WhatIsThatApiInfoResultDto
    {
        public String MaxImageSize { get; set; }
        public String MinImageSize { get; set; }
        public String ApiVersion { get; set; }
    }
}