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
using FlightRadarWebService.Core;
using FlightRadarWebService.Models.TransmissionModels;
using Microsoft.AspNetCore.Mvc;
using System;
using FlightRadarWebService.Core.Services.DataTransmissionProtocol;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace FlightRadarWebService.Controllers
{
    /// <summary>
    /// Send new Data to the Service and store it
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataRegistrationController : Controller
    {

        // POST: api/<controller>
        /// <summary>
        /// Register Data
        /// </summary>
        /// <param name="received"></param>
        [HttpPost]
        public void RegisterData(DataTransmissionModel received)
        {
            try
            {
                var newReceivedData = new DataTransmissionModel
                {
                    Flight = received.Flight,
                    Track = received.Track,
                    Altitude = received.Altitude,
                    Latitude = received.Latitude,
                    Longitude = received.Longitude,
                    Prefix = received.Prefix,
                    SenderId = received.SenderId,
                    Groundspeed = received.Groundspeed,
                    Timestamp = received.Timestamp,
                    UTC = received.UTC,
                    DeviationAlt = received.DeviationAlt,
                    DeviationLat = received.DeviationLat,
                    DeviationLong = received.DeviationLong,
                    AltTimestamp = received.AltTimestamp,
                    LatTimestamp = received.LatTimestamp,
                    Longimestamp = received.Longimestamp,
                    Flarm = received.Flarm,
                };

                //Create CSV Model
                var cw = new CsvWriter<DataTransmissionModel>();

                //Write Model into Csv File
                cw.WriteModelToCsvFile(newReceivedData, Constants.DATA_RECEIVED_FILE_PATH);

                //Add Data To registration Class (Data Dictionary)
                DataTransmissionOperations.GetInstance().RegisterData(newReceivedData);
            }
            catch (Exception e)
            {
                //Exceptions are typically logged at the ERROR level
                Constants.LOGGER.Error(e);
                throw;
            }
        }
    }
}
