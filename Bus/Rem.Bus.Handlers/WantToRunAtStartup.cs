using NServiceBus;

namespace Rem.Bus.Handlers
{
    // This class deals with an issue in NServiceBus-NServiceBus-0116ed8\src\impersonation\NServiceBus.Impersonation\ImpersonationManager.cs.
    // http://tech.groups.yahoo.com/group/nservicebus/message/13141
    // In the method Transport_TransportMessageReceived, 
    // it check ConfigureImpersonation.Impersonate (it is actually ImpersonateSender) which is always true for a server endpoint. 
    // Here we force it to be false. 
    internal class WantToRunAtStartup : IWantToRunAtStartup
    {
        public void Run()
        {
            Configure.With().UnicastBus().ImpersonateSender(false);
        }

        public void Stop()
        {
        }
    }
}
