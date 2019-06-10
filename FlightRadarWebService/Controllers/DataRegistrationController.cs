using System;
using System.IO;
using CSVWriter;
using FlightRadarWebService.Core;
using FlightRadarWebService.Core.Services.DataBaseOperations;
using FlightRadarWebService.Models;
using Microsoft.AspNetCore.Mvc;

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
                    DeviationLong = received.DeviationLong
                };

                //Create CSV Model
                var cw = new CsvWriter<DataTransmissionModel>();

                //Write Model into Csv File
                cw.WriteModelToCsvFile(newReceivedData, Constants.FilePath);

                //Insert data into Database
                DataBaseOperations.InsertFlugData(newReceivedData);

                //Add Data To registration Class (Data Dictionary)
                DataRegistration.GetInstance().AddDataToDic(newReceivedData);

            }
            catch (Exception e)
            {
                //Exceptions are typically logged at the ERROR level
                Constants.Logger.Error(e);
                throw;
            }
        }

        //// POST api/<controller>
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="received"></param>
        ///// <returns></returns>
        //[HttpPost("InsertData")]
        //public IActionResult InsertData(DataTransmissionModel received)
        //{
        //    var newReceivedData = new DataTransmissionModel
        //    {
        //        Flight = received.Flight,
        //        Track = received.Track,
        //        Altitude = received.Altitude,
        //        Latitude = received.Latitude,
        //        Longitude = received.Longitude,
        //        Prefix = received.Prefix,
        //        SenderId = received.SenderId,
        //        Groundspeed = received.Groundspeed,
        //        Timestamp = received.Timestamp
        //    };

        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\DataFiles", "Data.csv");
        //    var cw = new CsvWriter<DataTransmissionModel>();
        //    cw.WriteModelToCsvFile(newReceivedData, filePath);

        //    DataRegistration.GetInstance().AddDataToDic(newReceivedData);

        //    return Ok(DataRegistration.GetInstance().ReceivedData);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="received"></param>
        ///// <returns></returns>
        //[Route("Data/")]
        //[HttpPost("AddData")]
        //public JsonResult AddData(DataTransmissionModel received)
        //{
        //    var newReceivedData = new DataTransmissionModel
        //    {
        //        Flight = received.Flight,
        //        Track = received.Track,
        //        Altitude = received.Altitude,
        //        Latitude = received.Latitude,
        //        Longitude = received.Longitude,
        //        Prefix = received.Prefix,
        //        SenderId = received.SenderId,
        //        Groundspeed = received.Groundspeed,
        //        Timestamp = received.Timestamp
        //    };

        //    DataRegistration.GetInstance().AddDataToDic(newReceivedData);

        //    return Json(DataRegistration.GetInstance().ReceivedData);
        //}
    }
}
