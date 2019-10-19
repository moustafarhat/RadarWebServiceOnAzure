using CSVWriter;
using FlightRadarWebService.Core;
using FlightRadarWebService.Models.TransmissionModels;
using Microsoft.AspNetCore.Mvc;
using System;
using FlightRadarWebService.Core.Services.DataTransmissionProtocol;

namespace FlightRadarWebService.Controllers
{
    /// <summary>
    /// Receive a List of Data
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FlightTableController : ControllerBase
    {
        // POST: api/<controller>
        /// <summary>
        /// Register Data List
        /// </summary>
        /// <param name="receivedTable"></param>
        [HttpPost]
        public void RegisterDataTable(DataTransmissionList receivedTable)
        {
            var dataList = receivedTable.DataTransmissionModels;

            //Create CSV Model
            var cw = new CsvWriter<DataTransmissionModel>();

            try
            {
                foreach (var recievedModel in dataList)
                {
                    DataTransmissionOperations.GetInstance().RegisterData(recievedModel);

                    //Insert data into Database
                    //DataBaseOperations.InsertFlugData(recievedModel);
                }

                //Write Model into Csv File
                cw.WriteModelsListToCsvFile(dataList, Constants.DATA_RECEIVED_FILE_PATH);
            }
            catch (Exception e)
            {
                //Exceptions are typically logged at the ERROR level
                Constants.LOGGER.Error(e);
                throw;
            }
        }
    }
}