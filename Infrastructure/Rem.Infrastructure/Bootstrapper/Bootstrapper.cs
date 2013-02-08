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
using System.Text.RegularExpressions;
using Agatha.Common.InversionOfControl;
using Agatha.Common.WCF;
using Agatha.ServiceLayer;
using AutoMapper;
using AutoMapper.Mappers;
using C32Gen;
using NHibernate.Event;
using NLog;
using Pillar.Common.Bootstrapper;
using Pillar.Common.Configuration;
using Pillar.Common.Cryptography;
using Pillar.Domain;
using Pillar.Domain.Event;
using Pillar.Domain.NHibernate;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Storage.Managed.Backup;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Domain.EventListener;
using Rem.Infrastructure.Metadata;
using Rem.Infrastructure.Security;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Infrastructure.Service.Interceptor;
using StructureMap;
using TerminologyService.WebService;
using IConfigurationProvider = uNhAddIns.SessionEasier.IConfigurationProvider;
using IContainer = StructureMap.IContainer;
using PillarIoC = Pillar.Common.InversionOfControl.IoC;

namespace Rem.Infrastructure.Bootstrapper
{
    /// <summary>
    /// Defines the bootstrapper process needs to run.
    /// </summary>
    public abstract class Bootstrapper
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        /// <summary>
        /// Initializes the specified app state.
        /// </summary>
        public virtual void Run ()
        {
            Logger.Info("Initializing StructureMap IoC Container");
            var appContainer = CreateAndConfigureApplicationDiContainer ();

            //Need to configure AutoMapper before it is used so do it right away.
            ConfigureAutoMapper ( appContainer );

            Logger.Info("Initializing Pillar IoC");
            ConfigurePillarIoC(appContainer);

            Logger.Info("Initializing Agatha");
            ConfigureAgatha ( appContainer );

            ConfigureRavenDb ( appContainer );
            ConfigureSecurity ( appContainer );

            // Begin initialize and along the way do further DI Container configuration if required

            Logger.Info("Initializing NHibernate");
            InitializeNHibernate ( appContainer );

            Logger.Info("Running Service Registries");
            CallServiceRegistries ( appContainer );

            Logger.Info("Running Bootstrapper Tasks");
            RunBootstrapperTasks ( appContainer );

            Logger.Debug ( appContainer.WhatDoIHave () );
        }

        /// <summary>
        /// Configures the auto mapper.
        /// </summary>
        /// <param name="appContainer">The app container.</param>
        protected virtual void ConfigureAutoMapper ( IContainer appContainer )
        {
            var originalMapperFunction = MapperRegistry.AllMappers;
            MapperRegistry.AllMappers = () =>
                                            {
                                                var mappers = ( originalMapperFunction.Invoke () as IObjectMapper[] ).ToList ();
                                                var objectMappers = appContainer.GetAllInstances<IObjectMapper> ();
                                                mappers.AddRange ( objectMappers );
                                                return mappers.ToArray ();
                                            };
        }

        /// <summary>
        /// Creates the and configure application di container.
        /// </summary>
        /// <returns>A StructureMap Container.</returns>
        protected virtual IContainer CreateAndConfigureApplicationDiContainer()
        {
            ObjectFactory.Configure(c => c.For<IAssemblyLocator>().Singleton().Use<AssemblyLocator>());

            ObjectFactory.Configure(c => c.For<IConfigurationPropertiesProvider>().Singleton().Use<AppSettingsConfiguration>());
            ObjectFactory.Configure(c => c.For<IPersistenceConfigurerProvider>().Singleton().Use<MsSql2008PersistenceConfigurerProvider>());
            ObjectFactory.Configure(c => c.For<IConfigurationProvider>().Singleton().Use<FluentConfigurationProvider>());

            ObjectFactory.Configure(c => c.For<IWorkingMemoryGetStrategy>().Use<NhibernateWorkingMemoryGetStrategy>());

            // AES Encryption Utility
            ObjectFactory.Configure(c => c.For<IEncryptionUtility>().Use<AesEncryptionUtility>());

            // C32 Generator
            ObjectFactory.Configure(c => c.For<IC32DtoSerializer>().Use<C32DtoSerializer>());
            ObjectFactory.Configure(c => c.For<IGreenCdaToC32Transformer>().Use<GreenCdaToC32Transformer>());
            ObjectFactory.Configure(c => c.For<IC32ToGreenCdaTransformer>().Use<C32ToGreenCdaTransformer>());
            ObjectFactory.Configure(c => c.For<IC32Validator>().Use<C32Validator>());
            ObjectFactory.Configure(c => c.For<IC32Builder>().Use<C32Builder>());

            //TerminologyService
            ObjectFactory.Configure(c => c.For<ITerminologyService>().Use<TerminologyService.WebService.TerminologyService>());

            // Domain Event Service - per transaction
            RegisterDomainEventService(ObjectFactory.Container);

            ObjectFactory.Configure(x => x.Scan(scanner =>
            {
                // In the scan operation, include all needed dlls as defined in the helper class.
                // be cautious in the future - this could still pick up unwanted assemblies,
                // such as the stray test project that mistakenly ends up in the bin folder.
                // so consider those possibilities if errors pop up, and you're led here.
                // scanner.AssembliesFromApplicationBaseDirectory(AutoRegistrationAssemblyHelper.ShouldStructureMapIncludeAssembly);
                scanner.AssembliesFromApplicationBaseDirectory( AutoRegistrationAssemblyHelper.ShouldStructureMapIncludeAssembly );

                scanner.LookForRegistries();

                // Register all boostrapper tasks
                scanner.AddAllTypesOf<IBootstrapperTask>();

                // Register All Permission Descriptors
                scanner.AddAllTypesOf<IPermissionDescriptor>();

                //Register all KnowTypeProviders
                scanner.AddAllTypesOf<IKnownTypeProvider>();

                //Register all ObjectMappers
                scanner.AddAllTypesOf<IObjectMapper>();

                scanner.ConnectImplementationsToTypesClosing(typeof(IKeyedDtoFactory<>));

                scanner.ConnectImplementationsToTypesClosing(typeof(IDomainEventHandler<>));

                scanner.ConnectImplementationsToTypesClosing(typeof(IRuleCollection<>));

                scanner.ConnectImplementationsToTypesClosing(typeof(IRuleCollectionCustomizer<,>));

                scanner.WithDefaultConventions();
            }));

            return ObjectFactory.Container;
        }

        /// <summary>
        /// Registers the domain event service.
        /// </summary>
        /// <param name="container">The container.</param>
        protected abstract void RegisterDomainEventService ( IContainer container );

        /// <summary>
        /// Configures the pillar IoC.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void ConfigurePillarIoC(IContainer container)
        {
            PillarIoC.SetContainerProvider(() => new Pillar.IoC.StructureMap.Container(container));
            PillarIoC.Bootstrap();
        }

        /// <summary>
        /// Configures the agatha.
        /// </summary>
        /// <param name="appContainer">The app container.</param>
        protected virtual void ConfigureAgatha(IContainer appContainer)
        {
            var structureMapContainer = new Agatha.StructureMap.Container ( appContainer );
            IoC.Container = structureMapContainer;

            var assemblyLocator = appContainer.GetInstance<IAssemblyLocator> ();
            var infrastructureAssemblies = assemblyLocator.LocateInfrastructureAssemblies();

            var genericsToApply = new Dictionary<Type, Type> ();
            foreach (var infrastructureAssembly in infrastructureAssemblies)
            {
                var genericsToApplyInAssebmly = KnownTypeHelper.GetGenerics ( infrastructureAssembly );
                
                foreach ( KeyValuePair<Type, Type> keyValuePair in genericsToApplyInAssebmly )
                {
                    genericsToApply.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            var serviceLayerConfiguration = new ServiceLayerConfiguration ( structureMapContainer );

            var serviceAssemblies = assemblyLocator.LocateWebServiceAssemblies();

            foreach ( var assembly in serviceAssemblies )
            {
                var assemblyRef = assembly;
                Logger.Debug ( "Registering Requests, Responses, Handlers, and Dtos for Assembly: {0}", assemblyRef );

                KnownTypeHelper.RegisterNonGenericRequestsAndResponses ( assembly );
                serviceLayerConfiguration.AddRequestHandlerAssembly ( assembly );

                KnownTypeProvider.RegisterDerivedTypesOf<AbstractDataTransferObject> ( assembly.GetTypes ().Where ( t => !t.IsGenericType ) );

                KnownTypeHelper.RegisterGenerics ( genericsToApply, assembly );
            }

            KnownTypeProvider.Register<TerminologyConcept> ();
            KnownTypeProvider.Register<TerminologyProperty> ();
            KnownTypeProvider.Register<TerminologyPropertyType> ();
            KnownTypeProvider.Register<TerminologyVocabulary> ();

            // register the agatha interceptors
            serviceLayerConfiguration.RegisterRequestHandlerInterceptor<SecurityInterceptor>();
            serviceLayerConfiguration.RegisterRequestHandlerInterceptor<RuleViolationEventInterceptor> ();

            serviceLayerConfiguration.RequestProcessorImplementation = typeof ( Service.PerformanceLoggingRequestProcessor );
            serviceLayerConfiguration.Initialize ();

            RegisterIRequestHandlerOfGetRequestDtoByKey ( appContainer );
            CallKnownTypeProviders ( appContainer );
        }

        private void RegisterIRequestHandlerOfGetRequestDtoByKey(IContainer container)
        {
            var assemblyLocator = container.GetInstance<IAssemblyLocator>();
            var assemblies = assemblyLocator.LocateWebServiceAssemblies();

            foreach (var assembly in assemblies)
            {
                Logger.Debug("Registering IRequestHandler<GetDtoRequestByKey<TDto>> instance in DI container: {0}.", assembly);

                var dtoTypes = from dtoType in assembly.GetTypes()
                               where dtoType.IsSubclassOf(typeof(KeyedDataTransferObject)) && !dtoType.IsAbstract
                               select dtoType;
                foreach (var dtoType in dtoTypes)
                {
                    var openGetDtoRequestType = typeof(GetDtoRequest<>);
                    var closedGetDtoRequestType = openGetDtoRequestType.MakeGenericType(dtoType);

                    var openGetDtoByKeyRequestHandlerType = typeof(GetDtoByKeyRequestHandler<>);
                    var closedGetDtoByKeyRequestHandlerType =
                        openGetDtoByKeyRequestHandlerType.MakeGenericType(dtoType);

                    var openRequestHandlerInterfaceType = typeof(IRequestHandler<>);
                    var closedGetDtoByKeyRequestHandlerInterfaceType =
                        openRequestHandlerInterfaceType.MakeGenericType(closedGetDtoRequestType);

                    if (!container.Model.HasDefaultImplementationFor(closedGetDtoByKeyRequestHandlerInterfaceType))
                    {
                        container.Configure(
                            c =>
                            c.For(closedGetDtoByKeyRequestHandlerInterfaceType).Use(
                                p => PillarIoC.CurrentContainer.Resolve(closedGetDtoByKeyRequestHandlerType)));
                    }
                }
            }
        }

        private void CallKnownTypeProviders(IContainer container)
        {
            Logger.Info("Running Known Type Providers");
            var knownTypeProviders = container.GetAllInstances<IKnownTypeProvider>();
            foreach (var knownTypeProvider in knownTypeProviders)
            {
                var knownTypeProviderName = knownTypeProvider.GetType().ToString();
                Logger.Debug("KnownTypeProviders: {0}", knownTypeProviderName);
                knownTypeProvider.RegisterTypes();
            }
        }

        /// <summary>
        /// Configures the raven db.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void ConfigureRavenDb(IContainer container)
        {
            ForceLoadingRavenDbAssemblies ();
            var appsettingsProvider = container.GetInstance<IConfigurationPropertiesProvider> ();
            var ravenDbUrl = appsettingsProvider.GetProperty ( "RavenDBUrl" );
            var runInMemory = appsettingsProvider.GetProperty ( "RavenDBRunInMemory" );

            IDocumentStore store = runInMemory.ToLower () == "true" ?
                                                                        new EmbeddableDocumentStore { RunInMemory = true } : new DocumentStore { Url = ravenDbUrl };

            store.Initialize ();
            container.Configure ( c => c.For<IDocumentStore> ().Singleton ().Use ( store ) );

            RegisterDocumentSessionProvider ( container );
            container.Configure ( c => c.For<IDocumentSessionProvider> ().HttpContextScoped ().Use<DocumentSessionProvider> () );
        }

        /// <summary>
        /// Registers the document session provider.
        /// </summary>
        /// <param name="container">The container.</param>
        protected abstract void RegisterDocumentSessionProvider ( IContainer container );

        private static void ForceLoadingRavenDbAssemblies()
        {
            // Just make sure that the Raven.Storage.Managed assembly is loaded
            var restoreOperation = new RestoreOperation(string.Empty, string.Empty);
        }

        /// <summary>
        /// Configures the security.
        /// </summary>
        /// <param name="appContainer">The app container.</param>
        protected virtual void ConfigureSecurity ( IContainer appContainer )
        {
            appContainer.Configure ( c => c.For<ICurrentUserPermissionService>().Use<CurrentUserPermissionService>() );
            var accessCtrlMgr = appContainer.GetInstance<IAccessControlManager> ();
            var permissionDescriptors = appContainer.GetAllInstances<IPermissionDescriptor> ();
            accessCtrlMgr.RegisterPermissionDescriptor ( permissionDescriptors.ToArray () );
        }

        /// <summary>
        /// Initializes the Nhibernate.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void InitializeNHibernate(IContainer container)
        {
            var configurationProvider = container.GetInstance<IConfigurationProvider>();
            var configuration = configurationProvider.Configure().GetEnumerator().Current;

            configuration.SetListener(ListenerType.Flush, new PostFlushFixEventListener());
            configuration.SetListener(ListenerType.Autoflush, new AutoFlushFixEventListener());
            configuration.SetListener(ListenerType.PostUpdate, new PatientAccessAuditablePostUpdateListener());
            configuration.SetListener(ListenerType.PostInsert, new PatientAccessAuditablePostInsertListener());
            configuration.SetListener(ListenerType.PostDelete, new PatientAccessAuditablePostDeleteListener());

            ConfigureNHibernateSession(configurationProvider, container);
        }

        /// <summary>
        /// Configures the NHibernate session.
        /// </summary>
        /// <param name="configurationProvider">The configuration provider.</param>
        /// <param name="container">The container.</param>
        protected abstract void ConfigureNHibernateSession(IConfigurationProvider configurationProvider, IContainer container);

        /// <summary>
        /// Calls the service registries.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void CallServiceRegistries(IContainer container)
        {
            var serviceRegistries = container.GetAllInstances<IServiceRegistry> ();
            foreach ( var serviceRegistry in serviceRegistries )
            {
                var serviceRegistryName = serviceRegistry.GetType ().ToString ();
                Logger.Debug ( "ServiceRegistries: {0}", serviceRegistryName );
                serviceRegistry.RegisterServices ();
            }
        }

        /// <summary>
        /// Runs the bootstrapper tasks.
        /// </summary>
        /// <param name="container">The container.</param>
        protected virtual void RunBootstrapperTasks(IContainer container)
        {
            var tasks = container.GetAllInstances<IBootstrapperTask> ();
            foreach ( var bootstrapperTask in tasks )
            {
                bootstrapperTask.Execute ();
            }
        }
    }
}
