using FlightRadarWebService.Models.ProcessingModels;
using FlightRadarWebService.Models.TransmissionModels;
using System.Collections.Generic;
using System.Linq;

namespace FlightRadarWebService.Core.Services.DataProcessingProtocol
{
    public class DataProcessingOperations
    {
        private static DataProcessingOperations _dataProcessingOperations;

        /// <summary>
        /// Private Constructor
        /// </summary>
        private DataProcessingOperations()
        { }

        /// <summary>
        ///  Static Instance
        /// </summary>
        /// <returns></returns>
        public static DataProcessingOperations GetInstance()
        {
            if (_dataProcessingOperations == null)
            {
                _dataProcessingOperations = new DataProcessingOperations();
                return _dataProcessingOperations;
            }

            return _dataProcessingOperations;
        }
        /// <summary>
        /// Model Transformation Data TransmissionTo Data Processing with Kalman object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataProcessingModel DataTransmissionModelToDataProcessingModelKalman(DataTransmissionModel model)
        {
            var dataProcessingModel = new DataProcessingModel()
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
                UTC_Predicted=model.UTC,
                KalmanRunner = new KalmanRunner()
            };
            return dataProcessingModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldDataProcessingModel"></param>
        /// <param name="dataTransmissionModel"></param>
        /// <returns></returns>
        public static DataProcessingModel DataTransmissionModelToDataProcessingModel(DataProcessingModel oldDataProcessingModel, DataTransmissionModel dataTransmissionModel)
        {
            var dataProcessingModel = new DataProcessingModel()
            {
                Altitude = dataTransmissionModel.Altitude==0? oldDataProcessingModel.Altitude:dataTransmissionModel.Altitude,
                SenderId = dataTransmissionModel.SenderId,
                Flight = dataTransmissionModel.Flight,
                Latitude = dataTransmissionModel.Latitude==0? oldDataProcessingModel.Latitude:dataTransmissionModel.Latitude,
                Longitude = dataTransmissionModel.Longitude==0? oldDataProcessingModel.Longitude:dataTransmissionModel.Longitude,
                IsPredicted = dataTransmissionModel.IsPredicted,
                Groundspeed = dataTransmissionModel.Groundspeed,
                Timestamp = dataTransmissionModel.Timestamp,
                Track = dataTransmissionModel.Track,
                AltTimestamp = dataTransmissionModel.AltTimestamp,
                AltitudeUnit = dataTransmissionModel.AltitudeUnit,
                DeviationAlt = dataTransmissionModel.DeviationAlt,
                DeviationLat = dataTransmissionModel.DeviationLat,
                DeviationLong = dataTransmissionModel.DeviationLong,
                Flarm = dataTransmissionModel.Flarm,
                GroundSpeedUnit = dataTransmissionModel.GroundSpeedUnit,
                LatTimestamp = dataTransmissionModel.LatTimestamp,
                Longimestamp = dataTransmissionModel.LatTimestamp,
                Prefix = dataTransmissionModel.Prefix,
                UTC = dataTransmissionModel.UTC,
                UTC_Predicted=dataTransmissionModel.UTC,
                KalmanRunner =oldDataProcessingModel.KalmanRunner
            };
            
            return dataProcessingModel;
        }
        /// <summary>
        /// Model Transformation Data Transmission List To Data Processing 
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public IList<DataProcessingModel> DataTransmissionModelListToDataProcessingModel(IEnumerable<DataTransmissionModel> models)
        {

            return models.Select(model => new DataProcessingModel()
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
                UTC_Predicted=model.UTC,
                KalmanRunner = new KalmanRunner()
            }).ToList();
            
        }
    }
}
