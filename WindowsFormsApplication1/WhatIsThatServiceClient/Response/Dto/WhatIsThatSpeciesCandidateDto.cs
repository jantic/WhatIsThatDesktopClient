using System;
using System.Diagnostics.CodeAnalysis;

namespace WhatIsThat.WhatIsThatServiceClient.Response.Dto
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [Serializable]
    public class WhatIsThatSpeciesCandidateDto
    {
        public String CommonName { get; set; }
        public String ScientificName { get; set; }
        public String Confidence { get; set; }
    }
}