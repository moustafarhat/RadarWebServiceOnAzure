namespace FlightRadarWebService.Core.Services.DataProcessedProtocol
{
    using CSVWriter;
    using Models.ProcessedModels;
    using Models.ProcessingModels;
    using Models.TransmissionModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DataProcessedOperations
    {
        /// <summary>
        /// Class which contains all processed Data and other Transformations
        /// </summary>
        private static DataProcessedOperations _dataProcessedOperations;

        /// <summary>
        /// Private Constructor
        /// </summary>

        private DataProcessedOperations()
        { }

        /// <summary>
        /// Static Instance
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
        /// Function which retrieves all processed Data
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
                if (model.Value.Altitude < 0 || model.Value.Longitude < 0 || model.Value.Latitude < 0)
                {
                    continue;

                }

                var cw = new CsvWriter<DataProcessedModel>();

                //Write Model into Csv File
                cw.WriteModelToCsvFile(DataProcessingModelToDataProcessedModel(model.Value), Constants.DATA_PROCESSED_FILE_PATH);

                //Add value to Dictionary

                DataContainers.GetInstance().DATA_PROCESSED_CONTAINER.Add(model.Value.Flight, DataProcessingModelToDataProcessedModel(model.Value));
            }

            return DataContainers.GetInstance().DATA_PROCESSED_CONTAINER;
        }

        /// <summary>
        /// Model Transformation (Data Processing To Data Processed)
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
                UTC = model.UTC_Predicted,
            };
            return dataProcessingModel;
        }

        /// <summary>
        /// Model Transformation (Data Transmission List To Data Processed)
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
        /// Delete Old Data in Dictionary after a period of time
        /// </summary>
        private static void UpdateAndDeleteOldPositions()
        {
            var oldFlightsList = new List<string>();

            foreach (var data in DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Values)
            {
                var currentTime = DateTime.UtcNow;

                double diff = currentTime.Subtract(data.UTC.Value).Seconds;

                if (diff > 60)
                {
                    //DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Remove(data.Flight);
                    oldFlightsList.Add(data.Flight);
                }
                else
                {
                    double diff2 = currentTime.Subtract(data.UTC_Predicted.Value).Seconds;

                    var cartesianCoordinates = data.KalmanRunner.Predict(diff2);

                    if (cartesianCoordinates == null) return;

                    data.Altitude = cartesianCoordinates.Altitude;
                    data.Longitude = cartesianCoordinates.Longitude;
                    data.Latitude = cartesianCoordinates.Latitude;
                    data.UTC_Predicted = DateTime.UtcNow;

                    
                }
            }

            foreach (var str in oldFlightsList)
            {
                DataContainers.GetInstance().DATA_PROCESSING_CONTAINER.Remove(str);
            }
        }
    }
}