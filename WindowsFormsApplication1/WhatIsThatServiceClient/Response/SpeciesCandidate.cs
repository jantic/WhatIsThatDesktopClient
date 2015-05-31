using System;

namespace WhatIsThat.WhatIsThatServiceClient.Response
{
    public class SpeciesCandidate
    {
        private readonly String _commonName;
        private readonly String _scientificName;
        private readonly Double _confidence;

        public SpeciesCandidate(String commonName, String scientificName, Double confidence)
        {
            _commonName = commonName;
            _scientificName = scientificName;
            _confidence = confidence;
        }

        public String CommonName
        {
            get { return _commonName; }
        }

        public String ScientificName
        {
            get { return _scientificName; }
        }

        public Double Confidence
        {
            get { return _confidence; }
        }
    }
}