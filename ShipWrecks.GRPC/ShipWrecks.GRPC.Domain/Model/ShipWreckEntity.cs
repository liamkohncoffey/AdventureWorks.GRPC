using System.Collections.Generic;

namespace ShipWrecks.GRPC.Domain.Model
{
    public class ShipWreckEntity : Entity
    {

        public ShipWreckEntity(): base() { }

        public ShipWreckEntity(string record,
            string vesselTerms,
            string featureType,
            string chart,
            double latDec,
            double lonDec,
            string gpQuality,
            string depth,
            string soundingType,
            string history,
            string quasou,
            string watlev,
            List<decimal> coordinates):  base()
        {
            Record = record;
            VesselTerms = vesselTerms;
            FeatureType = featureType;
            Chart = chart;
            LatDec = latDec;
            LonDec = lonDec;
            GpQuality = gpQuality;
            Depth = depth;
            SoundingType = soundingType;
            History = history;
            Quasou = quasou;
            WatLev = watlev;
            Coordinates = coordinates;
        }
        
        public string Record { get; set; }
        public string VesselTerms { get; set; }
        public string FeatureType { get; set; }
        public string Chart { get; set; }
        public double LatDec { get; set; }
        public double LonDec { get; set; }
        public string GpQuality { get; set; }
        public string Depth { get; set; }
        public string SoundingType { get; set; }
        public string History { get; set; }
        public string Quasou { get; set; }
        public string WatLev { get; set; }
        public List<decimal> Coordinates { get; set; } 
    }
}