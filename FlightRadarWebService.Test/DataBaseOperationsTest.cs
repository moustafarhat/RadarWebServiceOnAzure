using NUnit.Framework;
using Autofac;
using FlightRadarWebService.Core;
using FlightRadarWebService.Core.Services.Interfaces;

namespace FlightRadarWebService.Test
{
    public class DataBaseOperationsTest
    {
        [SetUp]
        public void Setup()
        {}

        [Test]
        public void TestDataBaseConnection()
        {
            var container = IoCBuilder.Build();
            var dataBaseOperations = container.Resolve<IDataBaseOperations>();
            Assert.IsTrue(dataBaseOperations.TestDataBaseConnection());
        }
    }
}