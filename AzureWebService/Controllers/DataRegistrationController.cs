using System;
using System.Collections.Generic;
using System.IO;
using AzureWebService.Core;
using AzureWebService.Models;
using Microsoft.AspNetCore.Mvc;
using CSVWriter;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace AzureWebService.Controllers
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
                    DeviationLong = received.DeviationLong
                };

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DataFiles\\", "Data.csv");

                var cw = new CsvWriter<DataTransmissionModel>();

                cw.WriteModelToCsvFile(newReceivedData, filePath);

                DataBaseConnection.InsertFlugData(newReceivedData);

                DataRegistration.GetInstance().AddDataToDic(newReceivedData);

                DataBaseConnection.SaveDataToTxtFile(newReceivedData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // POST api/<controller>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="received"></param>
        /// <returns></returns>
        [HttpPost("InsertData")]
        public IActionResult InsertData(DataTransmissionModel received)
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
                Timestamp = received.Timestamp
            };

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DataFiles", "Data.csv");
            var cw = new CsvWriter<DataTransmissionModel>();
            cw.WriteModelToCsvFile(newReceivedData, filePath);

            DataRegistration.GetInstance().AddDataToDic(newReceivedData);
            DataBaseConnection.SaveDataToTxtFile(newReceivedData);

            return Ok(DataRegistration.GetInstance().ReceivedData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="received"></param>
        /// <returns></returns>
        [Route("Data/")]
        [HttpPost("AddData")]
        public JsonResult AddData(DataTransmissionModel received)
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
                Timestamp = received.Timestamp
            };

            DataRegistration.GetInstance().AddDataToDic(newReceivedData);

            return Json(DataRegistration.GetInstance().ReceivedData);
        }
    }
}
