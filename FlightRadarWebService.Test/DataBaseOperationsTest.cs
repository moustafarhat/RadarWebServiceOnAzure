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
using FlightRadarWebService.Core.Services.DataBaseOperations;
using NUnit.Framework;

namespace FlightRadarWebService.Test
{
    public class DataBaseOperationsTest
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void TestDataBaseConnection()
        {
            var dataBaseOperations = new DataBaseOperations();
            Assert.IsTrue(dataBaseOperations.TestDataBaseConnection());
        }
    }
}