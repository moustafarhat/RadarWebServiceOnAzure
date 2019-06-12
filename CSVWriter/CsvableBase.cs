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