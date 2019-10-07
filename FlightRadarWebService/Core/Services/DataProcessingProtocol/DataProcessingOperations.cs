using FlightRadarWebService.Models.ProcessingModels;
using FlightRadarWebService.Models.TransmissionModels;
using System.Collections.Generic;
using System.Linq;

namespace FlightRadarWebService.Core.Services.DataProcessingProtocol
{
    public class DataProcessingOperations
    {
        private static DataProcessingOperations _dataProcessingOperations;

        private DataProcessingOperations()
        { }

        /// <summary>
        /// 
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
        /// Transfer Model
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
                KalmanRunner = new KalmanRunner()
            };
            return dataProcessingModel;
        }


        public DataProcessingModel DataTransmissionModelToDataProcessingModel(KalmanRunner runner, DataTransmissionModel model)
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
                KalmanRunner =runner
            };
            return dataProcessingModel;
        }
        /// <summary>
        /// Transfer Model
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public IList<DataProcessingModel> DataTransmissionModelListToDataProcessingModel(IList<DataTransmissionModel> models)
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
                KalmanRunner = new KalmanRunner()
            }).ToList();
        }
    }
}
