using System;
using System.Collections.Generic;
using AzureWebService.Core;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        [HttpGet]
        public IList<DataTransmissionModel> GetFlightData(string flight, DateTime time)
        {
            return DataBaseConnection.GetDataByFlightIdAndTimeStamp(flight, time);
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
            return Json(DataBaseConnection.GetDataByFlightIdAndTimeStamp(flight, time));
        }
    }
}
