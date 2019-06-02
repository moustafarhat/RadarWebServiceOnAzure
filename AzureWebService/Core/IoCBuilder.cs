using Autofac;
using FlightRadarWebService.Core.Protocols.DataBaseOperations;
using FlightRadarWebService.Core.Protocols.DataProcessingProtocol;
using FlightRadarWebService.Core.Protocols.DataTransmissionProtocol;
using FlightRadarWebService.Core.Protocols.Interfaces;

namespace FlightRadarWebService.Core
{
    /// <summary>
    ///  Main Container
    /// </summary>
    public class IoCBuilder
    {
        internal static IContainer Build()
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