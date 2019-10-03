////////////////////////////////////////////////////////////////////
//FileName: DataTransmissionProtocol.cs
//FileType: Visual C# Source file
//Size : 0
//Author : Moustafa Farhat
//Created On : 0
//Last Modified On : 0
//Copy Rights : Flight Radar API
//Description : Class which contains all Data Transmission operations
////////////////////////////////////////////////////////////////////
using FlightRadarWebService.Models.TransmissionModels;

namespace FlightRadarWebService.Core.Services.DataTransmissionProtocol
{
    /// <summary>
    /// Flights Data Transmission Protocols
    /// </summary>
    public class DataTransmissionOperations
    {
        private static DataTransmissionOperations _dataTransmissionOperations;

        private DataTransmissionOperations()
        { }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static DataTransmissionOperations GetInstance()
        {
            if (_dataTransmissionOperations == null)
            {
                _dataTransmissionOperations = new DataTransmissionOperations();
                return _dataTransmissionOperations;
            }

            return _dataTransmissionOperations;
        }

        /// <summary>
        /// Add data to Dictionary
        /// </summary>
        /// <param name="receivedData"></param>
        public void RegisterData(DataTransmissionModel receivedData)
        {
            if (DataContainers.GetInstance().DATA_RECEIVED_CONTAINER.ContainsKey(receivedData.Flight))
            {
                var currentKalman = DataContainers.GetInstance().DATA_RECEIVED_CONTAINER[receivedData.Flight].KalmanRunner;

                DataContainers.GetInstance().DATA_RECEIVED_CONTAINER[receivedData.Flight] = receivedData;

                //var result = currentKalman.Update(receivedData.Longitude, receivedData.Latitude,receivedData.Altitude);

                //ReceivedData[receivedData.Flight].Longitude = result[0];
                //ReceivedData[receivedData.Flight].Latitude = result[1];
                //ReceivedData[receivedData.Flight].Altitude =(int?)result[2];
                //ReceivedData[receivedData.Flight].KalmanRunner = currentKalman;
                DataContainers.GetInstance().DATA_RECEIVED_CONTAINER.Clear();
            }
            else
            {
                DataContainers.GetInstance().DATA_RECEIVED_CONTAINER.Add(receivedData.Flight, receivedData);
            }
        }
    }
}