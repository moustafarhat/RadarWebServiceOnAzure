using System;
using AzureWebService.Core;
using AzureWebService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
/// <summary>
/// hello world
/// </summary>
namespace AzureWebService.Controllers
{
    /// <summary>
    /// Send new Data to the Service and store it
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DataRegistrationController : Controller
    {
        // POST: api/<controller>
        /// <summary>
        /// Register Data
        /// </summary>
        /// <param name="received"></param>
        [HttpPost]
        public void RegisterData(FlugData received)
        {
            try
            {
                var newReceivedData = new FlugData
                {
                    Flight = received.Flight,
                    Track = received.Track,
                    Altitude = received.Altitude,
                    Latitude = received.Latitude,
                    Long = received.Long,
                    Prefix = received.Prefix,
                    SenderId = received.SenderId,
                    Groundspeed = received.Groundspeed,
                    Timestamp = received.Timestamp
                };

                DataRegistration.GetInstance().AddDataToDic(newReceivedData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // POST api/<controller>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="received"></param>
        /// <returns></returns>
        [HttpPost("InsertData")]
        public IActionResult InsertData(FlugData received)
        {
            var newReceivedData = new FlugData
            {
                Flight = received.Flight,
                Track = received.Track,
                Altitude = received.Altitude,
                Latitude = received.Latitude,
                Long = received.Long,
                Prefix = received.Prefix,
                SenderId = received.SenderId,
                Groundspeed = received.Groundspeed,
                Timestamp = received.Timestamp
            };
            DataRegistration.GetInstance().AddDataToDic(newReceivedData);

            return Ok(DataRegistration.GetInstance().ReceivedData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="received"></param>
        /// <returns></returns>
        [Route("Data/")]
        [HttpPost("AddData")]
        public JsonResult AddData(FlugData received)
        {
            var newReceivedData = new FlugData
            {
                Flight = received.Flight,
                Track = received.Track,
                Altitude = received.Altitude,
                Latitude = received.Latitude,
                Long = received.Long,
                Prefix = received.Prefix,
                SenderId = received.SenderId,
                Groundspeed = received.Groundspeed,
                Timestamp = received.Timestamp
            };
            DataRegistration.GetInstance().AddDataToDic(newReceivedData);

            return Json(DataRegistration.GetInstance().ReceivedData);
        }
    }
}
