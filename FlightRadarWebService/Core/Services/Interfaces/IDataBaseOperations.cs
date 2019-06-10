using System;
using System.Collections.Generic;
using FlightRadarWebService.Models;

namespace FlightRadarWebService.Core.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataBaseOperations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool TestDataBaseConnection();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flugData"></param>
        void InsertFlugData(DataTransmissionModel flugData);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        List<DataTransmissionModel> GetDataByFlightIdAndTimeStamp(string flight, DateTime time);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<DataTransmissionModel> GetDataAllDataFromDataBase();



    }
}
