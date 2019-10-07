////////////////////////////////////////////////////////////////////
//FileName: IDataBaseOperations.cs
//FileType: Visual C# Source file
//Size : 0
//Author : Moustafa Farhat
//Created On : 0
//Last Modified On : 0
//Copy Rights : Flight Radar API
//Description : Interface contains all Data Transmission operations
////////////////////////////////////////////////////////////////////

using CSVWriter;
using FlightRadarWebService.Core.Enum;
using System;

namespace FlightRadarWebService.Models.ProcessedModels
{
    public class DataProcessedModel : CsvableBase
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
        public double? Altitude { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public DateTime? AltTimestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LatTimestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Longimestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double? DeviationAlt { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string Covariance { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public bool IsPredicted { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public AltitudeUnits AltitudeUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GroundSpeedUnits GroundSpeedUnit { get; set; }

        /// <summary>
        /// Flarm Signal
        /// </summary>
        public int Flarm { get; set; } = 0;
    }
}
