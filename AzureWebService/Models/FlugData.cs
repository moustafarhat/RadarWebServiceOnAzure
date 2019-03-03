using System;
using AzureWebService.Core;

namespace AzureWebService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FlugData
    {

        /// <summary>
        /// 
        /// </summary>
        public Prefix? Prefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Groundspeed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Long { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Flight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Track { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Altitude { get; set; }
    }
}
