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
using FlightRadarWebService.Models.ProcessedModels;
using FlightRadarWebService.Models.ProcessingModels;
using FlightRadarWebService.Models.TransmissionModels;
using System;
using System.Collections.Generic;

namespace FlightRadarWebService.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class DataRegistration
    {
        /// <summary>
        /// Dictionary which contains Received Data
        /// </summary>
        public static IDictionary<string, DataTransmissionModel> ReceivedData;

        /// <summary>
        /// Dictionary for Data Processing
        /// </summary>
        public static IDictionary<string, DataProcessingModel> DataProcessing;

        /// <summary>
        /// Dictionary which contains Processed Data
        /// </summary>
        public static IDictionary<string, DataProcessedModel> ProcessedData;

        private static DataRegistration _dataRegistration;

        private DataRegistration()
        {
            ReceivedData = new Dictionary<string, DataTransmissionModel>();

            DataProcessing = new Dictionary<string, DataProcessingModel>();

            ProcessedData = new Dictionary<string, DataProcessedModel>();
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
                var currentKalman = ReceivedData[receivedData.Flight].KalmanRunner;

                ReceivedData[receivedData.Flight] = receivedData;

                //var result = currentKalman.Update(receivedData.Longitude, receivedData.Latitude,receivedData.Altitude);

                //ReceivedData[receivedData.Flight].Longitude = result[0];
                //ReceivedData[receivedData.Flight].Latitude = result[1];
                //ReceivedData[receivedData.Flight].Altitude =(int?)result[2];
                //ReceivedData[receivedData.Flight].KalmanRunner = currentKalman;

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
        /// <returns></returns>
        public IDictionary<string, DataProcessedModel> GetAllData()
        {
            //todo: return all flights data, which are stored in recievedData
            var removedList = new List<string>();
            foreach (var data in ReceivedData.Values)
            {
                if (data.KalmanRunner == null)
                {
                    data.KalmanRunner = new KalmanRunner();
                }

                var currentTime = DateTime.UtcNow;

                double diff = currentTime.Subtract(data.UTC.Value).Seconds;

                if (diff > 2000)
                {
                    removedList.Add(data.Flight);
                }

                else
                {

                    for (var i = 0; i < diff; i++)
                    {

                        data.KalmanRunner.Predict();
                    }

                    if (diff != 0)
                    {
                        var result = data.KalmanRunner.GetState();
                        data.Longitude = result[0];
                        data.Latitude = result[1];
                        data.Altitude = (int?)result[2];
                        data.UTC = DateTime.UtcNow;
                    }
                }

                //ProcessedData.Add(data.Flight, data);
            }

            foreach (var str in removedList)
            {
                ReceivedData.Remove(str);
            }

            return ProcessedData;
        }

        private static void DeleteOldDataFromDictionary()
        {
            foreach (var data in ReceivedData.Values)
            {
                var currentTime = DateTime.UtcNow;

                double diff = currentTime.Subtract(data.UTC.Value).Seconds;

                if (diff > 2000)
                {
                    ReceivedData.Remove(data.Flight);
                }
            }
        }
    }
}


