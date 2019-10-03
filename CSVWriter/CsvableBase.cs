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
namespace CSVWriter
{
    /// <summary>
    /// Abstract class to save Model data as csv file
    /// </summary>
    public abstract class CsvableBase
    {
        /// <summary>
        ///  Write data with ,
        /// </summary>
        /// <returns></returns>^^
        public virtual string ToCsv()
        {
            var output = "";

            var properties = GetType().GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                output += properties[i].GetValue(this).ToString();

                if (i != properties.Length - 1)
                {
                    output += ",";
                }
            }

            return output;
        }
    }
}