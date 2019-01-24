using System;
using AzureWebService.Core;

namespace AzureWebService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ReceivedData
    {

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
        public int SenderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Speed { get; set; }

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
        public int Direction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Hight { get; set; }
    }
}
