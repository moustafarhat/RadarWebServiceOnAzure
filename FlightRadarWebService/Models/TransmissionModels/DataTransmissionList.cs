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