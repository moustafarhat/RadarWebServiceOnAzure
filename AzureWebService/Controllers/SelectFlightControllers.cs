using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureWebService.Core;
using AzureWebService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureWebService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class SelectFlightControllers : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public FlugData GetFlightData(string flight)
        {
            return DataRegistration.GetInstance().GetFlightData(flight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetFlightData")]
        public JsonResult GetFlightRecord(string flight)
        {
            return Json(DataRegistration.GetInstance().GetFlightData(flight));
        }
    }
}
