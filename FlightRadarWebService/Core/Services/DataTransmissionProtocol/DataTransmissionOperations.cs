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

using System;
using FlightRadarWebService.CoordinateSystemConverter3D;
using FlightRadarWebService.Core.Services.DataProcessingProtocol;
using FlightRadarWebService.Models.ProcessingModels;
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


        private bool IsPositionNullOrZero(double? value)
        {
            if (value == null || value <= 0)
                return true;
            return false;


        }
        /// <summary>
        /// Add data to Dictionary
        /// </summary>
        /// <param name="receivedData"></param>
        public void RegisterData(DataTransmissionModel receivedData)
        {

            DataProcessingModel dataProcessingModel = null;

            if (DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.ContainsKey(receivedData.Flight))
            {
                dataProcessingModel = DataContainers.GetInstance().DATA_PROCESSING_CONTAINER[receivedData.Flight];

                DataContainers.GetInstance().DATA_PROCESSING_CONTAINER[receivedData.Flight] = DataProcessingOperations
                    .GetInstance().DataTransmissionModelToDataProcessingModel(dataProcessingModel.KalmanRunner,receivedData);
                

                DataContainers.GetInstance().DATA_RECEIVED_CONTAINER.Clear();
            }
            else
            {
                //to do: add a new record in the processing container


                DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Add(receivedData.Flight, DataProcessingOperations.GetInstance().DataTransmissionModelToDataProcessingModelKalman(receivedData));
                dataProcessingModel = DataContainers.GetInstance().DATA_PROCESSING_CONTAINER[receivedData.Flight];

            }

            CartesianCoordinates3D cartesianCoordinates;

            if (!IsPositionNullOrZero(receivedData.Altitude) && !IsPositionNullOrZero(receivedData.Latitude) && !IsPositionNullOrZero(receivedData.Longitude))
            {
                cartesianCoordinates = new CartesianCoordinates3D(
                    receivedData.Altitude.Value,
                    receivedData.Longitude.Value,
                    receivedData.Latitude.Value
                );

                cartesianCoordinates = dataProcessingModel.KalmanRunner.Update(cartesianCoordinates);

                dataProcessingModel.Altitude = cartesianCoordinates.Altitude;
                dataProcessingModel.Longitude = cartesianCoordinates.Longitude;
                dataProcessingModel.Latitude = cartesianCoordinates.Latitude;
                dataProcessingModel.UTC = DateTime.UtcNow;

            }

        }
    }
}