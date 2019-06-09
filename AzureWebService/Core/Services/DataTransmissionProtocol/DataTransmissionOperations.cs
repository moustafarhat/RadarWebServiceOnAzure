using System;
using System.Collections.Generic;
using FlightRadarWebService.Core.Services.Interfaces;
using FlightRadarWebService.Models;

namespace FlightRadarWebService.Core.Services.DataTransmissionProtocol
{
    /// <summary>
    /// Flights Data Transmission Protocols
    /// </summary>
    public class DataTransmissionOperations:IDataTransmissionOperations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<DataTransmissionModel> GetFlightDataByDate(DateTime timestamp)
        {
            throw new NotImplementedException();
        }
    }
}
