using System;
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
    [ApiController]
    public class DataRegistrationController : Controller
    {
        // POST: api/<controller>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="received"></param>
        [HttpPost]
        public void RegisterData(ReceivedData received)
        {
            try
            {
                var newReceivedData = new ReceivedData
                {
                    Flight = received.Flight,
                    Direction = received.Direction,
                    Hight = received.Hight,
                    Latitude = received.Latitude,
                    Long = received.Long,
                    Prefix = received.Prefix,
                    SenderId = received.SenderId,
                    Speed = received.Speed,
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
        public IActionResult InsertData(ReceivedData received)
        {
            var newReceivedData = new ReceivedData
            {
                Flight = received.Flight,
                Direction = received.Direction,
                Hight = received.Hight,
                Latitude = received.Latitude,
                Long = received.Long,
                Prefix = received.Prefix,
                SenderId = received.SenderId,
                Speed = received.Speed,
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
        public JsonResult AddData(ReceivedData received)
        {
            var newReceivedData = new ReceivedData
            {
                Flight = received.Flight,
                Direction = received.Direction,
                Hight = received.Hight,
                Latitude = received.Latitude,
                Long = received.Long,
                Prefix = received.Prefix,
                SenderId = received.SenderId,
                Speed = received.Speed,
                Timestamp = received.Timestamp
            };
            DataRegistration.GetInstance().AddDataToDic(newReceivedData);

            return Json(DataRegistration.GetInstance().ReceivedData);
        }
    }
}
