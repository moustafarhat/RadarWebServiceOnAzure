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
            //todo: return all flights data, which are stored in recievedData
            List<string> removedList=new List<string>();
            foreach (var data in ReceivedData.Values)
            {
                if (data.KalmanRunner == null)
                {
                    data.KalmanRunner = new KalmanRunner();
                }
                DateTime currentTime = DateTime.UtcNow;
                double diff = currentTime.Subtract(data.UTC.Value).Seconds;

                if (diff > 2000)
                {
                   removedList.Add(data.Flight);
                }

                else
                {

                    for (int i = 0; i < diff; i++)
                    {

                        data.KalmanRunner.Predict();
                    }
                    if (diff != 0)
                    {
                        double[] result = data.KalmanRunner.getState();
                        data.Longitude = result[0];
                        data.Latitude = result[1];
                        data.Altitude = (int?)result[2];
                        data.UTC = DateTime.UtcNow;
                    }

                }


               
            }

            foreach (var str in removedList)
            {
                ReceivedData.Remove(str);
            }
            return ReceivedData;


        }
    }

}


