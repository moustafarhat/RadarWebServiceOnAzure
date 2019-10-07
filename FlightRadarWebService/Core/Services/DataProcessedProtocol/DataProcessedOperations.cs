using FlightRadarWebService.Models.ProcessedModels;
using FlightRadarWebService.Models.TransmissionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using CSVWriter;
using FlightRadarWebService.CoordinateSystemConverter3D;
using FlightRadarWebService.Models.ProcessingModels;

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
            // delete old data and predict the new values 
            UpdateAndDeleteOldPositions();


            // transform the data from data-processing-Container to data-processed-Container
            DataContainers.GetInstance().DATA_PROCESSED_CONTAINER.Clear();

            //Create CSV Model
    

            foreach (var model in DataContainers.GetInstance().DATA_PROCESSING_CONTAINER)
            {
                
                var cw = new CsvWriter<DataProcessedModel>();
                //Write Model into Csv File
                cw.WriteModelToCsvFile(DataProcessingModelToDataProcessedModel(model.Value), Constants.DATA_PROCESSED_FILE_PATH);

                DataContainers.GetInstance().DATA_PROCESSED_CONTAINER.Add(model.Value.Flight,DataProcessingModelToDataProcessedModel(model.Value));
            }
            
            

            return DataContainers.GetInstance().DATA_PROCESSED_CONTAINER;
        }
        /// <summary>
        /// Transfer Model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataProcessedModel DataProcessingModelToDataProcessedModel(DataProcessingModel model)
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

        /// <summary>
        /// 
        /// </summary>
        private void UpdateAndDeleteOldPositions()
        {
            foreach (var data in DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Values)
            {
                var currentTime = DateTime.UtcNow;

                double diff = currentTime.Subtract(data.UTC.Value).Seconds;

                if (diff > 200)
                {
                    DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Remove(data.Flight);
                }
                else
                {
                    CartesianCoordinates3D cartesianCoordinates = data.KalmanRunner.Predict(diff);
                      
                    data.Altitude = cartesianCoordinates.Altitude;
                    data.Longitude = cartesianCoordinates.Longitude;
                    data.Latitude = cartesianCoordinates.Latitude;
                    data.UTC = DateTime.UtcNow;
                }
            }
        }
    }
}
