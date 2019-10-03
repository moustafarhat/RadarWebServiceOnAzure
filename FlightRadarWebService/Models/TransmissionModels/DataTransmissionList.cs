////////////////////////////////////////////////////////////////////
//FileName: IDataBaseOperations.cs
//FileType: Visual C# Source file
//Size : 0
//Author : Moustafa Farhat
//Created On : 0
//Last Modified On : 0
//Copy Rights : Flight Radar API
//Description : Interface contains all Data Transmission operations
////////////////////////////////////////////////////////////////////

using CSVWriter;
using System.Collections.Generic;

namespace FlightRadarWebService.Models.TransmissionModels
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTransmissionList : CsvableBase
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<DataTransmissionModel> DataTransmissionModels;
    }
}
