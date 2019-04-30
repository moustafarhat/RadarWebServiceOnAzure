////////////////////////////////////////////////////////////////////
//FileName: IDataTransmissionOperations.cs
//FileType: Visual C# Source file
//Size : 0
//Author : Moustafa Farhat
//Created On : 0
//Last Modified On : 0
//Copy Rights : Flight Radar API
//Description : Interface contains all Data Transmission operations
////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using AzureWebService.Models;

namespace AzureWebService.Core.Protocols.Interfaces
{
    /// <summary>
    /// Interface contains all Data Transmission operations
    /// </summary>
    public interface IDataTransmissionOperations
    {
        /// <summary>
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        IList<DataTransmissionModel> GetFlightDataByDate(DateTime timestamp);
    }
}