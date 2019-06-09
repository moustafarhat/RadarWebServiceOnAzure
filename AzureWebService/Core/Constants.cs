using System.IO;

namespace FlightRadarWebService.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// /NLog recommends using a static variable for the logger object
        /// </summary>
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// CSV data file default path
        /// </summary>
        public static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DataFiles\\", "Data.csv");

    }
}
