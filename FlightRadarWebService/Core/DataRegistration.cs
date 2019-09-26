using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FlightRadarWebService.Models;
using UnscentedKalmanFilter;

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
        public static IDictionary<string, DataTransmissionModel> ReceivedData;

        private static DataRegistration _dataRegistration;

        private static int _deleteCounter;

        private DataRegistration()
        {
            
            ReceivedData = new Dictionary<string, DataTransmissionModel>();

        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static DataProcessingModel ModelTransformation(DataTransmissionModel model)
        //{
        //    //TODO Model data
        //    DataProcessingModel dataProcessingModel = new DataProcessingModel()
        //    {
        //        Altitude = model.Altitude,
        //        SenderId = model.SenderId,
        //        Flight = model.Flight,
        //        Latitude = model.Latitude,
        //        Longitude = model.Longitude,
        //        IsPredicted = model.IsPredicted,
        //        Groundspeed = model.Groundspeed,
        //        Timestamp = model.Timestamp,
        //        Track = model.Track,

        //    };

        //    return dataProcessingModel;

        //}


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

            while (_deleteCounter==5)
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
