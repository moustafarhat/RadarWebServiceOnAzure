using FlightRadarWebService.Core.Services.DataProcessedProtocol;
using FlightRadarWebService.Models.ProcessedModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For mor
// e information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        ///Get all Processed Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IDictionary<string, DataProcessedModel> GetAllData()
        {
            return DataProcessedOperations.GetInstance().GetAllData();
        }
    }
}