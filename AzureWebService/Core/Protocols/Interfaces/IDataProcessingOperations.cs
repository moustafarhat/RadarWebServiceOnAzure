////////////////////////////////////////////////////////////////////
//FileName: IDataProcessingOperations.cs
//FileType: Visual C# Source file
//Size : 0
//Author : Moustafa Farhat
//Created On : 0
//Last Modified On : 0
//Copy Rights : Flight Radar API
//Description : Interface contains all Data Processing operations
////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using AzureWebService.Models;

namespace AzureWebService.Core.Protocols.Interfaces
{
    /// <summary>
    /// Interface contains all Data Processing operations
    /// </summary>
    public interface IDataProcessingOperations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        DataProcessingModel DataCorrection(List<DataTransmissionModel> x);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        DataProcessingModel ProcessDataByMean(string flight, DateTime timeStamp);

    }
}
