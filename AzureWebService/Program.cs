using System;
using AzureWebService;
using FlightRadarWebService.Core.Protocols.DataBaseOperations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FlightRadarWebService
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            if (!DataBaseOperations.TestDataBaseConnection())
            {
                throw new Exception("Database connection error");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
