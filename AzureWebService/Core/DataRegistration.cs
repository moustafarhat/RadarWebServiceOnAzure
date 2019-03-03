using System.Collections.Generic;
using AzureWebService.Models;

namespace AzureWebService.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class DataRegistration
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, FlugData> ReceivedData;
        private static DataRegistration _dataRegistration;

        private DataRegistration()
        {
            ReceivedData = new Dictionary<string, FlugData>();
        }

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
        /// 
        /// </summary>
        /// <param name="receivedData"></param>
        public void AddDataToDic(FlugData receivedData)
        {
            if (ReceivedData.ContainsKey(receivedData.Flight))
            {
                ReceivedData[receivedData.Flight] = receivedData;
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
        public FlugData GetFlightData(string flight)
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
        /// <returns></returns>
        public IDictionary<string, FlugData> GetAllData()
        {
            return ReceivedData;
        }
    }
}
