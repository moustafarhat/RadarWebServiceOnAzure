using FlightRadarWebService.Core.Services.DataBaseOperations;
using FlightRadarWebService.Models.TransmissionModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit

namespace FlightRadarWebService.Controllers
{
    /// <summary>
    /// Get Flight Data from Database 
    /// </summary>
    [Route("api/[controller]")]
    public class SelectFlightController : Controller
    {
        /// <summary>
        /// TODO:Get Flight Data from Database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<DataTransmissionModel> GetFlightData(string flight)
        {
            return DataBaseOperations.GetDataByFlightId(flight);
        }

        /// <summary>
        ///TODO: Get Flight Data from Database
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFlightData")]
        public JsonResult GetFlightRecord(string flight)
        {
            return Json(DataBaseOperations.GetDataByFlightId(flight));
        }
    }
}