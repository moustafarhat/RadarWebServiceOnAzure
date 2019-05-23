using System;
using System.Collections.Generic;
using AzureWebService.Core;
using AzureWebService.Core.Protocols.DataProcessingProtocol;
using AzureWebService.Core.Protocols.Interfaces;
using AzureWebService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureWebService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SelectFlightByIdAndTimeStampController : Controller
    {
        private IDataProcessingOperations processingOperations = new DataProcessingOperations();

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
            return processingOperations.ProcessDataByMean(flight,time);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet("GetFlightData")]
        public JsonResult GetFlightRecord(string flight,DateTime time)
        {
            return Json(processingOperations.ProcessDataByMean(flight, time));
        }
    }
}
