using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IDictionary<string, ReceivedData> ReceivedData;
        private static DataRegistration _dataRegistration;

        private DataRegistration()
        {
            ReceivedData = new Dictionary<string, ReceivedData>();
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
        public void AddDataToDic(ReceivedData receivedData)
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
        /// <returns></returns>
        public IDictionary<string, ReceivedData> GetAllData()
        {
            return ReceivedData;
        }
    }
}
