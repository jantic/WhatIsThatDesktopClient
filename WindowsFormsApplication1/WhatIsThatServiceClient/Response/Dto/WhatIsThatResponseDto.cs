using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WhatIsThat.WhatIsThatServiceClient.Response.Dto
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [Serializable]
    public class WhatIsThatResponseDto
    {
        public String Status { get; set; }
        public String Message { get; set; }
        public List<WhatIsThatSpeciesCandidateDto> SpeciesCandidates { get; set; }
    }
}