using System;
using CSVWriter;
using FlightRadarWebService.Core.Enum;

namespace FlightRadarWebService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTransmissionModel : CsvableBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Prefix Prefix { get; set; } = Prefix.Live;

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? UTC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SenderId { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public int? Groundspeed { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double? DeviationLat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double? DeviationLong { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Flight { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public int? Track { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public int? Altitude { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public double? DeviationAlt { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public AltitudeUnits AltitudeUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GroundSpeedUnits GroundSpeedUnit { get; set; }
    }
}
