using FlightRadarWebService.Models.ProcessedModels;
using FlightRadarWebService.Models.ProcessingModels;
using FlightRadarWebService.Models.TransmissionModels;
using System.Collections.Generic;

namespace FlightRadarWebService.Core
{
    public class DataContainers
    {
        /// <summary>
        /// Dictionary which contains Received Data
        /// </summary>
        public IDictionary<string, DataTransmissionModel> DATA_RECEIVED_CONTAINER;

        /// <summary>
        /// Dictionary for Data Processing
        /// </summary>
        public IDictionary<string, DataProcessingModel> DATA_PROCESSING_CONTAINER;

        /// <summary>
        /// Dictionary which contains Processed Data
        /// </summary>
        public IDictionary<string, DataProcessedModel> DATA_PROCESSED_CONTAINER;

        /// <summary>
        /// Class Instance
        /// </summary>
        private static DataContainers _dataContainer;

        private DataContainers()
        {
            DATA_RECEIVED_CONTAINER = new Dictionary<string, DataTransmissionModel>();

            DATA_PROCESSING_CONTAINER = new Dictionary<string, DataProcessingModel>();

            DATA_PROCESSED_CONTAINER = new Dictionary<string, DataProcessedModel>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataContainers GetInstance()
        {
            if (_dataContainer == null)
            {
                _dataContainer = new DataContainers();
                return _dataContainer;
            }
            return _dataContainer;
        }
    }
}
