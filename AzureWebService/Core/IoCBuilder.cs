using Autofac;
using AzureWebService.Core.Protocols.DataProcessingProtocol;
using AzureWebService.Core.Protocols.DataTransmissionProtocol;
using AzureWebService.Core.Protocols.Interfaces;

namespace AzureWebService.Core
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