using System;
using System.IO;

namespace FlightRadarWebService.Core
{
    /// <summary>
    /// Default Constants Values
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// /NLog recommends using a static variable for the logger object
        /// </summary>
        public static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// CSV Received data file default path
        /// </summary>
        public static string DATA_RECEIVED_FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DataFiles\\", "ReceivedData.csv");

        /// <summary>
        /// CSV Processed data file default path
        /// </summary>
        public static string DATA_PROCESSED_FILE_PATH = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DataFiles\\", "ProcessedData.csv");

        /// <summary>
        ///
        /// </summary>
        public static string DATA_BASE_CONNECTION_STRING = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\db\\Flight.db;Version=3;";

        /// <summary>
        /// CSV File Header
        /// </summary>
        public static string HEADER = "Code" + "," + "Description" + "," + "NDC" + "," + "Supplier Code"
        + "," + "Supplier Description" + "," + "Pack Size" + "," + "UOM";
    }
}