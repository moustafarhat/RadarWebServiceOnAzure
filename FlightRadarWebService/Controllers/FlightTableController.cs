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
using CSVWriter;
using FlightRadarWebService.Core;
using FlightRadarWebService.Models.TransmissionModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FlightRadarWebService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FlightTableController : ControllerBase
    {
        // POST: api/<controller>
        /// <summary>
        /// Register Data
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
                    //Add Data To registration Class (Data Dictionary)
                    //DataRegistration.GetInstance().AddDataToDic(recievedModel);

                    DataRegistration.GetInstance().AddDataToDic(recievedModel);

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
