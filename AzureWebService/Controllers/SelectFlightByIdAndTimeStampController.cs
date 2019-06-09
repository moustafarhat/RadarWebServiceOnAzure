using System;
using FlightRadarWebService.Core.Services.DataProcessingProtocol;
using FlightRadarWebService.Core.Services.Interfaces;
using FlightRadarWebService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightRadarWebService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SelectFlightByIdAndTimeStampController : Controller
    {
        private readonly IDataProcessingOperations _processingOperations = new DataProcessingOperations();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public DataProcessingModel GetFlightData(string flight, DateTime time)
        {
            return _processingOperations.ProcessDataByMean(flight,time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet("GetFlightDataFromDataBase")]
        public JsonResult GetFlightRecord(string flight,DateTime time)
        {
            return Json(_processingOperations.ProcessDataByMean(flight, time));
        }
    }
}
