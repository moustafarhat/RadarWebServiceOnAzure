using System.Collections.Generic;
using AzureWebService.Core.Protocols.Interfaces;

namespace AzureWebService.Core.Protocols.DataProcessingProtocol
{
    /// <summary>
    /// Flights Data Processing Protocols
    /// </summary>
    public class DataProcessingOperations:IDataProcessingOperations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public double MeanCalculator(List<double> numbers)
        {
            double sum=0;

            foreach (var variable in numbers)
            {
                sum += variable;
            }

            var mean = sum / numbers.Count + 1;

            return mean;
        }
    }
}
