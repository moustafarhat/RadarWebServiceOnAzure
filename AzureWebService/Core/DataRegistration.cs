using System;
using System.Collections.Generic;
using System.Linq;
using AzureWebService.Models;
using OpenCvSharp;
namespace AzureWebService.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class DataRegistration
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, DataTransmissionModel> ReceivedData;
        private static DataRegistration _dataRegistration;

        private DataRegistration()
        {
            ReceivedData = new Dictionary<string, DataTransmissionModel>();
        //Kalman
            //var kalman = new KalmanFilter();
            //kalman.Predict();
            //kalman.Correct(new Mat());
           
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
        /// 
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

            DeleteOldData();

            return ReceivedData;
        }

        /// <summary>
        /// Filter Dictionary & Delete old data
        /// </summary>
        /// <returns></returns>
        private void DeleteOldData()
        {
            var filtered = (from kvp in ReceivedData
                           where  kvp.Value.Timestamp.Value.Second > DateTime.Now.Second -5
                            select kvp).ToDictionary(t => t.Key, t => t.Value); ;

            ReceivedData = filtered;
        }
    }
}
