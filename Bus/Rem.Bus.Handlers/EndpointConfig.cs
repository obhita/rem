using Rem.Infrastructure.Server;
using log4net.Appender;
using log4net.Core;
using NServiceBus;

namespace Rem.Bus.Handlers
{
    // If you implement IWantCustomInitialization on the same class as the one which implements IConfigureThisEndpoint, 
    // that is the one that will be called first. 
    // Others which implements IWantCustomInitialization then will be called without particular order.
    [EndpointName("RemServerInputQueue")]
    internal class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()

                //.Log4Net<DebugAppender>(a => a.Threshold = Level.Debug)
                .StructureMapBuilder()
                .XmlSerializer()
                .UnicastBus();

            // The Bootstrapper must run here in the IWantCustomInitialization implementation not in IWantToRunAtStartup implementation.
            // Otherwise, you will get two different Structuremap container instances.
            new Bootstrapper().Run();
        }
    }
}
