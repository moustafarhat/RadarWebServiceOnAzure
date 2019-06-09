using System;
using System.Collections.Generic;
using FlightRadarWebService.Models;

namespace FlightRadarWebService.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class DataRegistration
    {
        /// <summary>
        /// Dictionary which contains all Data
        /// </summary>
        public IDictionary<string, DataTransmissionModel> ReceivedData;

        private static DataRegistration _dataRegistration;

        private static int _deleteCounter;

        private DataRegistration()
        {
            ReceivedData = new Dictionary<string, DataTransmissionModel>();
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataRegistration GetInstance()
        {
            if (_dataRegistration == null)
            {
                _dataRegistration = new DataRegistration();
                return _dataRegistration;
            }

            return _dataRegistration;
        }

        /// <summary>
        /// Add data to Dictionary
        /// </summary>
        /// <param name="receivedData"></param>
        public void AddDataToDic(DataTransmissionModel receivedData)
        {
            if (ReceivedData.ContainsKey(receivedData.Flight))
            {
                ReceivedData[receivedData.Flight] = receivedData;
            }
            else
            {
                ReceivedData.Add(receivedData.Flight, receivedData);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public DataTransmissionModel GetFlightData(string flight)
        {
            if (ReceivedData.ContainsKey(flight))
            {
                return ReceivedData[flight];
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public IList<DataTransmissionModel> GetFlightDataByDate(DateTime timestamp)
        {
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, DataTransmissionModel> GetAllData()
        {
            _deleteCounter += 1;

            while (_deleteCounter==2)
            {
                _deleteCounter = 0;
                DeleteOldData();
            }

            return ReceivedData;
        }

        /// <summary>
        /// Filter Dictionary & Delete old data
        /// </summary>
        /// <returns></returns>
        public void DeleteOldData()
        {
            ReceivedData.Clear();
            //TODO: Delete old Data automatically that are older than X time
        }
    }
}
