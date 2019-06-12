using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVWriter
{
    public class CsvWriter<T> where T : CsvableBase
    {
        /// <summary>
        ///  Save List of Models to csv file
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="destination"></param>
        public void WriteModelsListToCsvFile(IEnumerable<T> objects, string destination)
        {
            var objs = objects as IList<T> ?? objects.ToList();

            if (objs.Any())
            {
                using (var sw = new StreamWriter(destination,true))
                {
                    foreach (var obj in objs)
                    {
                        sw.WriteLine(obj.ToCsv());
                    }
                }
            }
        }

        /// <summary>
        /// Save model to csv file
        /// </summary>
        /// <param name="model"></param>
        /// <param name="destination"></param>
        public void WriteModelToCsvFile(T model, string destination)
        {
           using (var sw = new StreamWriter(destination, true))
           {
               sw.WriteLine(model.ToCsv());
           }
        }
    }
}