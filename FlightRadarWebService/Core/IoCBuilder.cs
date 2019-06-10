using Autofac;
using FlightRadarWebService.Core.Services.DataBaseOperations;
using FlightRadarWebService.Core.Services.DataProcessingProtocol;
using FlightRadarWebService.Core.Services.DataTransmissionProtocol;
using FlightRadarWebService.Core.Services.Interfaces;

namespace FlightRadarWebService.Core
{
    /// <summary>
    ///  Main Container
    /// </summary>
    public class IoCBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            RegisterTypes(builder);
            return builder.Build();
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<DataBaseOperations>().As<IDataBaseOperations>();
            builder.RegisterType<DataTransmissionOperations>().As<IDataTransmissionOperations>();
            builder.RegisterType<DataProcessingOperations>().As<IDataProcessingOperations>();
       }
    }
}