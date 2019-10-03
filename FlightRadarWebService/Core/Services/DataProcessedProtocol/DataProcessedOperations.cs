using FlightRadarWebService.Models.ProcessedModels;
using FlightRadarWebService.Models.TransmissionModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightRadarWebService.Core.Services.DataProcessedProtocol
{
    public class DataProcessedOperations
    {
        private static DataProcessedOperations _dataProcessedOperations;

        private DataProcessedOperations()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataProcessedOperations GetInstance()
        {
            if (_dataProcessedOperations == null)
            {
                _dataProcessedOperations = new DataProcessedOperations();
                return _dataProcessedOperations;
            }

            return _dataProcessedOperations;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, DataProcessedModel> GetAllData()
        {
            //todo: return all flights data, which are stored in recievedData
            var removedList = new List<string>();

            foreach (var data in DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Values)
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

                DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Add(data.Flight, data);
                
            }

            foreach (var str in removedList)
            {
                DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Remove(str);
            }

            return DataContainers.GetInstance().DATA_PROCESSED_CONTAINER;
        }
        /// <summary>
        /// Transfer Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataProcessedModel DataTransmissionModelToDataProcessedModel(DataTransmissionModel model)
        {
            var dataProcessingModel = new DataProcessedModel
            {
                Altitude = model.Altitude,
                SenderId = model.SenderId,
                Flight = model.Flight,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                IsPredicted = model.IsPredicted,
                Groundspeed = model.Groundspeed,
                Timestamp = model.Timestamp,
                Track = model.Track,
                AltTimestamp = model.AltTimestamp,
                AltitudeUnit = model.AltitudeUnit,
                DeviationAlt = model.DeviationAlt,
                DeviationLat = model.DeviationLat,
                DeviationLong = model.DeviationLong,
                Flarm = model.Flarm,
                GroundSpeedUnit = model.GroundSpeedUnit,
                LatTimestamp = model.LatTimestamp,
                Longimestamp = model.LatTimestamp,
                Prefix = model.Prefix,
                UTC = model.UTC,
            };
            return dataProcessingModel;
        }


        /// <summary>
        /// Transfer Model
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public IList<DataProcessedModel> DataTransmissionModelListToDataProcessedModel(IList<DataTransmissionModel> models)
        {
            return models.Select(model => new DataProcessedModel
            {
                Altitude = model.Altitude,
                SenderId = model.SenderId,
                Flight = model.Flight,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                IsPredicted = model.IsPredicted,
                Groundspeed = model.Groundspeed,
                Timestamp = model.Timestamp,
                Track = model.Track,
                AltTimestamp = model.AltTimestamp,
                AltitudeUnit = model.AltitudeUnit,
                DeviationAlt = model.DeviationAlt,
                DeviationLat = model.DeviationLat,
                DeviationLong = model.DeviationLong,
                Flarm = model.Flarm,
                GroundSpeedUnit = model.GroundSpeedUnit,
                LatTimestamp = model.LatTimestamp,
                Longimestamp = model.LatTimestamp,
                Prefix = model.Prefix,
                UTC = model.UTC
            }).ToList();
        }

        private void DeleteOldDataFromDictionary()
        {
            foreach (var data in DataContainers.GetInstance().DATA_PROCESSED_CONTAINER.Values)
            {
                var currentTime = DateTime.UtcNow;

                double diff = currentTime.Subtract(data.UTC.Value).Seconds;

                if (diff > 2000)
                {
                    DataContainers.GetInstance().DATA_PROCESSED_CONTAINER.Remove(data.Flight);
                }
            }
        }
    }
}
