using System.Collections.Generic;
using FlightRadarWebService.Core.Protocols.DataBaseOperations;
using FlightRadarWebService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit 

namespace FlightRadarWebService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class SelectFlightController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<DataTransmissionModel> GetFlightData(string flight)
        {
            return DataBaseOperations.GetDataByFlightId(flight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFlightData")]
        public JsonResult GetFlightRecord(string flight)
        {
            return Json(DataBaseOperations.GetDataByFlightId(flight));
        }
    }
}
