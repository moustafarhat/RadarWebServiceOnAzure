using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSVWriter;

namespace FlightRadarWebService.Models
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
