using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using WhatIsThat.WhatIsThatServiceClient.Response.Dto;

namespace WhatIsThat.WhatIsThatServiceClient.Response
{
    public class WhatIsThatSpeciesCandidatesCollection
    {
        private readonly ImmutableList<SpeciesCandidate> _speciesCandidates;

        public WhatIsThatSpeciesCandidatesCollection(List<WhatIsThatSpeciesCandidateDto> dtos)
        {
            _speciesCandidates = ImmutableList.Create(ReadAllSpeciesCandidates(dtos).ToArray());
        }

        public List<SpeciesCandidate> SpeciesCandidates
        {
            get { return _speciesCandidates.ToList(); }
        }

        private List<SpeciesCandidate> ReadAllSpeciesCandidates(List<WhatIsThatSpeciesCandidateDto> dtos)
        {
            var speciesCandidates = new List<SpeciesCandidate>();

            if (dtos == null)
            {
                return speciesCandidates;
            }

            speciesCandidates.AddRange(from dto in dtos 
                let commonName = dto.CommonName 
                let confidence = String.IsNullOrEmpty(dto.Confidence) ? 0 : Double.Parse(dto.Confidence) 
                let scientificName = dto.ScientificName 
                select new SpeciesCandidate(commonName, scientificName, confidence));

            return speciesCandidates;
        }
    }
}