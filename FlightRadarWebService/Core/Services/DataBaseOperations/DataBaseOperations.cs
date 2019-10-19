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
using FlightRadarWebService.Models.TransmissionModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace FlightRadarWebService.Core.Services.DataBaseOperations
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBaseOperations
    {
        /// <summary>
        /// Test Data Base connection and return true or false
        /// </summary>
        /// <returns></returns>
        public bool TestDataBaseConnection()
        {
            using (var connection = new SQLiteConnection(Constants.DATA_BASE_CONNECTION_STRING))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SQLiteException e)
                {
                    //Exceptions are typically logged at the ERROR level
                    Constants.LOGGER.Error(e);
                    return false;
                }
            }
        }

        /// <summary>
        /// Insert operation for Flug Data 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void InsertFlugData(DataTransmissionModel flugData)
        {
            var con = new SQLiteConnection(Constants.DATA_BASE_CONNECTION_STRING);
            try
            {
                con.Open();
                var cmd = new SQLiteCommand("INSERT INTO Flights (Timestamp,SenderId,Groundspeed,Latitude,Longitude,Flight,Track,Altitude,UTC,DeviationLat,DeviationLong,DeviationAlt,AltTimestamp,LatTimestamp,LongTimestamp,Covariance,IsPredicted)VALUES(@Timestamp,@SenderId,@Groundspeed,@Latitude,@Longitude,@Flight,@Track,@Altitude,@UTC,@DeviationLat,@DeviationLong,@DeviationAlt,@AltTimestamp,@LatTimestamp,@LongTimestamp,@Covariance,@IsPredicted)", con);


                cmd.Parameters.AddWithValue("@Timestamp", flugData.Timestamp ?? DateTime.Now);
                cmd.Parameters.AddWithValue("@SenderId", flugData.SenderId);
                cmd.Parameters.AddWithValue("@Groundspeed", flugData.Groundspeed ?? 0);
                cmd.Parameters.AddWithValue("@Latitude", flugData.Latitude ?? 0);
                cmd.Parameters.AddWithValue("@Longitude", flugData.Longitude ?? 0);
                cmd.Parameters.AddWithValue("@Flight", flugData.Flight ?? "Null");
                cmd.Parameters.AddWithValue("@Track", flugData.Track ?? 0);
                cmd.Parameters.AddWithValue("@Altitude", flugData.Altitude ?? 0);

                cmd.Parameters.AddWithValue("@UTC", flugData.UTC ?? DateTime.Now);
                cmd.Parameters.AddWithValue("@DeviationLat", flugData.DeviationLat ?? 0);
                cmd.Parameters.AddWithValue("@DeviationLong", flugData.DeviationLong ?? 0);
                cmd.Parameters.AddWithValue("@DeviationAlt", flugData.DeviationAlt ?? 0);

                cmd.Parameters.AddWithValue("@AltTimestamp", flugData.AltTimestamp ?? DateTime.Now);
                cmd.Parameters.AddWithValue("@LatTimestamp", flugData.LatTimestamp ?? DateTime.Now);
                cmd.Parameters.AddWithValue("@Longimestamp", flugData.LongTimestamp ?? DateTime.Now);

                cmd.Parameters.AddWithValue("@IsPredicted", flugData.IsPredicted);

                cmd.Parameters.AddWithValue("@Covariance", flugData.Covariance ?? "Null");

                int count = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Exceptions are typically logged at the ERROR level
                Constants.LOGGER.Error(ex);
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public static List<DataTransmissionModel> GetDataByFlightId(string flight)
        {
            var flugDataLst = new List<DataTransmissionModel>();

            using (var connection = new SQLiteConnection(Constants.DATA_BASE_CONNECTION_STRING))
            {
                using (var command = new SQLiteCommand(
                    "Select * from Flights where Flight = @Flight", connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Flight", flight));

                    connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var flugData = new DataTransmissionModel
                        {
                            Altitude = Convert.ToInt32(reader["Altitude"]),
                            Flight = reader["Flight"].ToString(),
                            Groundspeed = Convert.ToInt32(reader["Groundspeed"]),
                            Track = Convert.ToInt32(reader["Track"]),
                            Latitude = Convert.ToInt32(reader["Latitude"]),
                            Longitude = Convert.ToInt32(reader["Longitude"]),
                            Timestamp = Convert.ToDateTime(reader["Timestamp"].ToString()),
                            SenderId = reader["SenderId"].ToString()
                        };

                        flugDataLst.Add(flugData);
                    }
                }
            }

            return flugDataLst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static List<DataTransmissionModel> GetDataByFlightIdAndTimeStamp(string flight, DateTime time)
        {
            var flugDataLst = new List<DataTransmissionModel>();

            using (var connection = new SQLiteConnection(Constants.DATA_BASE_CONNECTION_STRING))
            {
                using (var command = new SQLiteCommand(
                    "Select * from Flights where Flight = @Flight and TimeStamp=@TimeStamp", connection))
                {
                    command.Parameters.Add(new SQLiteParameter("@Flight", flight));
                    command.Parameters.Add(new SQLiteParameter("@TimeStamp", time));

                    connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var flugData = new DataTransmissionModel
                        {
                            Altitude = Convert.ToInt32(reader["Altitude"]),
                            Flight = reader["Flight"].ToString(),
                            Groundspeed = Convert.ToInt32(reader["Groundspeed"]),
                            Track = Convert.ToInt32(reader["Track"]),
                            Latitude = Convert.ToDouble(reader["Latitude"]),
                            Longitude = Convert.ToDouble(reader["Longitude"]),
                            Timestamp = Convert.ToDateTime(reader["Timestamp"]),
                            SenderId = reader["SenderId"].ToString()
                        };

                        flugDataLst.Add(flugData);
                    }
                }
            }

            var result = from dataTransmissionModel in flugDataLst
                         group dataTransmissionModel by dataTransmissionModel.Altitude into flightGroup
                         select new
                         {
                             AverageAltitude = flightGroup.Average(x => x.Altitude),
                             AverageLatitude = flightGroup.Average(x => x.Latitude),
                             AverageLongitude = flightGroup.Average(x => x.Longitude),
                         };


            return flugDataLst;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<DataTransmissionModel> GetDataAllDataFromDataBase()
        {
            var flugDataLst = new List<DataTransmissionModel>();

            using (var connection = new SQLiteConnection(Constants.DATA_BASE_CONNECTION_STRING))
            {
                using (var command = new SQLiteCommand(
                    "Select * from Flights", connection))
                {

                    connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var flugData = new DataTransmissionModel
                        {
                            Altitude = ParseStringValueToInt(reader["Altitude"]),
                            Flight = reader["Flight"].ToString(),
                            Groundspeed = ParseStringValueToInt(reader["Groundspeed"]),
                            Track = ParseStringValueToInt(reader["Track"]),
                            Latitude = Convert.ToDouble(reader["Latitude"]),
                            Longitude = Convert.ToDouble(reader["Longitude"]),
                            Timestamp = Convert.ToDateTime(reader["Timestamp"].ToString() ?? null),
                            SenderId = reader["SenderId"].ToString()
                        };

                        flugDataLst.Add(flugData);
                    }
                }
            }

            return flugDataLst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CheckNull<T>(object obj)
        {
            return (obj == DBNull.Value ? default(T) : (T)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ParseStringValueToInt(object value)
        {
            try
            {
                if (value == null)
                {
                    return 0;
                }
                var result = Convert.ToInt32(value);
                return result;

            }
            catch (FormatException)
            {
                return 0;
            }
        }
    }
}
