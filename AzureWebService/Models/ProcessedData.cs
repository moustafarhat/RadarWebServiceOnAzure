using System;
using AzureWebService.Core;

namespace AzureWebService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessedData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        public ProcessedData(string latitude)
        {
            Latitude = latitude;
        }

        /// <summary>
        /// 
        /// </summary>
        public Prefix Prefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Timestamp { get; set; }

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
        public int Speed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Direction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Hight { get; set; }
    }
}
