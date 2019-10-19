using FlightRadarWebService.Core.Services.DataBaseOperations;
using NUnit.Framework;

namespace FlightRadarWebService.Test.DataBase.Test
{
    /// <summary>
    /// Test DataBase Connection 
    /// </summary>
    public class DataBaseOperationsTest
    {
        [SetUp]
        public void Setup()
        { }

        /// <summary>
        /// Test DataBase Connection Method
        /// </summary>
        [Test]
        public void TestDataBaseConnection()
        {
            var dataBaseOperations = new DataBaseOperations();
            Assert.IsTrue(dataBaseOperations.TestDataBaseConnection());
        }
    }
}