using System.Collections.Generic;
using FlightRadarWebService.Core;
using FlightRadarWebService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlightRadarWebService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class DataRetrieveController : Controller
    {
        
        // GET: api/<controller>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IDictionary<string, DataTransmissionModel> GetAllData()
        {
            return DataRegistration.GetInstance().GetAllData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllData")]
        public JsonResult GetAllStudentRecords()
        {
            return Json(DataRegistration.GetInstance().GetAllData());
        }
    }
}
