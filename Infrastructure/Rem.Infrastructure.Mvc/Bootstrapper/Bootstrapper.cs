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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Agatha.Common;
using Agatha.Common.WCF;
using Pillar.Common.Bootstrapper;
using Pillar.Common.InversionOfControl;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Bootstrapper;
using Rem.Infrastructure.Mvc.Service;
using Rem.Infrastructure.Mvc.UserContext;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.Web.Service;
using StructureMap;
using StructureMap.Pipeline;
using Container = Pillar.IoC.StructureMap.Container;
using IContainer = StructureMap.IContainer;

namespace Rem.Infrastructure.Mvc.Bootstrapper
{
    /// <summary>
    /// Class for bootstrapping.
    /// </summary>
    public class Bootstrapper
    {
        #region Public Methods

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public virtual void Run ()
        {
            var appContainer = CreateAndConfigureApplicationDiContainer ();

            ConfigurePillarIoC ( appContainer );

            ConfigureAgatha ( appContainer );

            ConfigureSecurity ( appContainer );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Configures agatha.
        /// </summary>
        /// <param name="appContainer">The app container.</param>
        protected virtual void ConfigureAgatha ( IContainer appContainer )
        {
            var structureMapContainer = new Agatha.StructureMap.Container ( appContainer );
            Agatha.Common.InversionOfControl.IoC.Container = structureMapContainer;

            var assemblyLocator = appContainer.GetInstance<IAssemblyLocator> ();
            var infrastructureAssemblies = assemblyLocator.LocateInfrastructureAssemblies ();

            var genericsToApply = new Dictionary<Type, Type> ();
            foreach ( var infrastructureAssembly in infrastructureAssemblies )
            {
                var genericsToApplyInAssebmly = KnownTypeHelper.GetGenerics ( infrastructureAssembly );

                foreach ( var keyValuePair in genericsToApplyInAssebmly )
                {
                    genericsToApply.Add ( keyValuePair.Key, keyValuePair.Value );
                }
            }

            var serviceAssemblies = assemblyLocator.LocateWebServiceAssemblies ();

            foreach ( var assembly in serviceAssemblies )
            {
                KnownTypeHelper.RegisterNonGenericRequestsAndResponses ( assembly );

                KnownTypeProvider.RegisterDerivedTypesOf<AbstractDataTransferObject> ( assembly.GetTypes ().Where ( t => !t.IsGenericType ) );

                KnownTypeHelper.RegisterGenerics ( genericsToApply, assembly );
            }

            var clientConfiguration = new ClientConfiguration ( structureMapContainer )
                {
                    AsyncRequestDispatcherImplementation = typeof( AsyncRequestDispatcher )
                };

            clientConfiguration.AddRequestAndResponseAssembly ( typeof( GetLookupValuesRequest ).Assembly );

            clientConfiguration.RequestProcessorImplementation = typeof( CustomSyncRequestProcessorProxy );

            clientConfiguration.Initialize ();
        }

        /// <summary>
        /// Configures the pillar IoC.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void ConfigurePillarIoC ( IContainer container )
        {
            IoC.SetContainerProvider ( () => new Container ( container ) );
            IoC.Bootstrap ();
        }

        /// <summary>
        /// Configures the security.
        /// </summary>
        /// <param name="appContainer">The app container.</param>
        protected virtual void ConfigureSecurity ( IContainer appContainer )
        {
            appContainer.Configure (
                c => c.For<ICurrentUserContextService> ().LifecycleIs ( new HttpSessionLifecycle () ).Use<CurrentUserService> () );
            var accessCtrlMgr = appContainer.GetInstance<IAccessControlManager> ();
            var permissionDescriptors = appContainer.GetAllInstances<IPermissionDescriptor> ();
            accessCtrlMgr.RegisterPermissionDescriptor ( permissionDescriptors.ToArray () );
        }

        /// <summary>
        /// Creates the and configure application di container.
        /// </summary>
        /// <returns>A StructureMap Container.</returns>
        protected virtual IContainer CreateAndConfigureApplicationDiContainer ()
        {
            ObjectFactory.Configure (
                x => x.Scan (
                    scanner =>
                        {
                            // In the scan operation, include all needed dlls as defined in the helper class.
                            // be cautious in the future - this could still pick up unwanted assemblies,
                            // such as the stray test project that mistakenly ends up in the bin folder.
                            // so consider those possibilities if errors pop up, and you're led here.
                            // scanner.AssembliesFromApplicationBaseDirectory(AutoRegistrationAssemblyHelper.ShouldStructureMapIncludeAssembly);
                            scanner.AssembliesFromApplicationBaseDirectory ( AutoRegistrationAssemblyHelper.ShouldStructureMapIncludeAssembly );

                            scanner.LookForRegistries ();

                            // Register all boostrapper tasks
                            scanner.AddAllTypesOf<IBootstrapperTask> ();

                            // Register All Permission Descriptors
                            scanner.AddAllTypesOf<IPermissionDescriptor> ();

                            scanner.AddAllTypesOf<IAsyncRequestProcessor> ();

                            scanner.WithDefaultConventions ();
                        } ) );

            return ObjectFactory.Container;
        }

        #endregion
    }
}
