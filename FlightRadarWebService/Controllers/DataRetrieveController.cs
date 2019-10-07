////////////////////////////////////////////////////////////////////
//FileName: IDataBaseOperations.cs
//FileType: Visual C# Source file
//Size : 0
//Author : Moustafa Farhat
//Created On : 0
//Last Modified On : 0
//Copy Rights : Flight Radar API
//Description : Interface contains all Data Transmission operations
////////////////////////////////////////////////////////////////////
using FlightRadarWebService.Core.Services.DataProcessedProtocol;
using FlightRadarWebService.Models.ProcessedModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CSVWriter;
using FlightRadarWebService.Core;
using FlightRadarWebService.Models.TransmissionModels;

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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IDictionary<string, DataProcessedModel> GetAllData()
        {

            return DataProcessedOperations.GetInstance().GetAllData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllData")]
        public JsonResult GetAllStudentRecords()
        {
            return Json(DataProcessedOperations.GetInstance().GetAllData());
        }
    }
}
