using System.Collections.Generic;
using AzureWebService.Core;
using AzureWebService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit 

namespace AzureWebService.Controllers
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
