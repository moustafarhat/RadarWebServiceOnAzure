using FlightRadarWebService.CoordinateSystemConverter3D;
using FlightRadarWebService.Core.Services.DataProcessingProtocol;
using FlightRadarWebService.Models.ProcessingModels;
using FlightRadarWebService.Models.TransmissionModels;
using System;

namespace FlightRadarWebService.Core.Services.DataTransmissionProtocol
{
    /// <summary>
    /// Flights Data Transmission Functions
    /// </summary>
    public class DataTransmissionOperations
    {
        private static DataTransmissionOperations _dataTransmissionOperations;

        /// <summary>
        /// Private Constructor
        /// </summary>
        private DataTransmissionOperations()
        { }

        /// <summary>
        /// Static Instance
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
        /// Check double value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsPositionNullOrZero(double? value)
        {
            return value == null || value <= 0;
        }

        /// <summary>
        /// Add data to Dictionary
        /// </summary>
        /// <param name="receivedData"></param>
        public void RegisterData(DataTransmissionModel receivedData)
        {
            DataProcessingModel dataProcessingModel;

            var updateTimes = 1;

            if (DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.ContainsKey(receivedData.Flight))
            {
                dataProcessingModel = DataContainers.GetInstance().DATA_PROCESSING_CONTAINER[receivedData.Flight];

                dataProcessingModel = DataProcessingOperations.DataTransmissionModelToDataProcessingModel(dataProcessingModel, receivedData);

                DataContainers.GetInstance().DATA_RECEIVED_CONTAINER.Clear();
            }
            else
            {
                DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Add(receivedData.Flight, DataProcessingOperations.GetInstance().DataTransmissionModelToDataProcessingModelKalman(receivedData));
                dataProcessingModel = DataContainers.GetInstance().DATA_PROCESSING_CONTAINER[receivedData.Flight];
                updateTimes = 10;
            }

            if (!IsPositionNullOrZero(receivedData.Altitude) && !IsPositionNullOrZero(receivedData.Latitude) && !IsPositionNullOrZero(receivedData.Longitude))
            {
                var cartesianCoordinates = new CartesianCoordinates3D(
                    receivedData.Altitude.Value,
                    receivedData.Longitude.Value,
                    receivedData.Latitude.Value
                );

                for (var i = 0; i < updateTimes; i++)
                {
                    cartesianCoordinates = dataProcessingModel.KalmanRunner.Update(cartesianCoordinates);
                }

                dataProcessingModel.Altitude = cartesianCoordinates.Altitude;
                dataProcessingModel.Longitude = cartesianCoordinates.Longitude;
                dataProcessingModel.Latitude = cartesianCoordinates.Latitude;
                dataProcessingModel.UTC = DateTime.UtcNow;
            }
        }
    }
}