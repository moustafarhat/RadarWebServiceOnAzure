using System;
using CSVWriter;

namespace AzureWebService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DataProcessingModel : CsvableBase
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Timestamp { get; set; }

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
        public double? Longitude { get; set; }

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
    }
}
