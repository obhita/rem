#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using NHibernate;
using NHibernate.Cfg;
using Pillar.Domain.Event;
using Rem.Infrastructure.Metadata;
using StructureMap;
using uNhAddIns.SessionEasier;

namespace Rem.Infrastructure.Server
{
    /// <summary>
    /// This Bootstrapper is for NServiceBus based application server application.
    /// </summary>
    public class Bootstrapper : Infrastructure.Bootstrapper.Bootstrapper
    {
        #region Methods

        /// <summary>
        /// Configures the NHibernate session.
        /// </summary>
        /// <param name="configurationProvider">The configuration provider.</param>
        /// <param name="container">The container.</param>
        protected override void ConfigureNHibernateSession ( IConfigurationProvider configurationProvider, IContainer container )
        {
            NHibernate.Cfg.Configuration configuration = configurationProvider.Configure ().GetEnumerator ().Current;
            ISessionFactory sessionFactory = configuration.BuildSessionFactory ();

            container.Configure ( x => x.For<ISessionFactory> ().Singleton ().Use ( sessionFactory ) );
            container.Configure (
                x => x.For<ISession> ().LifecycleIs ( new NServiceBusThreadLocalStorageLifestyle () ).Use ( sessionFactory.OpenSession ) );
        }

        /// <summary>
        /// Registers the document session provider.
        /// </summary>
        /// <param name="container">The container.</param>
        protected override void RegisterDocumentSessionProvider ( IContainer container )
        {
            container.Configure (
                c => c.For<IDocumentSessionProvider> ().LifecycleIs ( new NServiceBusThreadLocalStorageLifestyle () ).Use<DocumentSessionProvider> () );
        }

        /// <summary>
        /// Registers the domain event service.
        /// </summary>
        /// <param name="container">The container.</param>
        protected override void RegisterDomainEventService ( IContainer container )
        {
            // Domain Event Service - per transport message scope
            container.Configure (
                c => c.For<IDomainEventService> ().LifecycleIs ( new NServiceBusThreadLocalStorageLifestyle () ).Use<DomainEventService> () );
        }

        #endregion
    }
}
