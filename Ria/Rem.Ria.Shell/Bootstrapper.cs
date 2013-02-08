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
using System.Windows;
using Agatha.Common;
using Agatha.Common.InversionOfControl;
using Agatha.Common.WCF;
using Agatha.Unity;
using Castle.DynamicProxy;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using NLog;
using NLog.Config;
using Pillar.Common.Metadata.Dtos;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Bootstrapper;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure;
using Rem.Ria.Infrastructure.Common;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.LogOutWarning;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Security;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.View.Behavior;
using Rem.Ria.Infrastructure.View.Configuration;
using Rem.Ria.Infrastructure.View.CustomControls;
using Rem.Ria.Infrastructure.Web.Service;
using Telerik.Windows.Controls;
using AsyncRequestDispatcher = Rem.Ria.Infrastructure.Service.AsyncRequestDispatcher;
using Category = Microsoft.Practices.Prism.Logging.Category;

namespace Rem.Ria.Shell
{
    /// <summary>
    /// The Bootstrapper in the base class (UnityBootstrapper) loads in the following sequence:
    /// CreateLogger();
    /// CreateModuleCatalog();
    /// ConfigureModuleCatalog();
    /// CreateContainer();
    /// ConfigureContainer();
    /// ConfigureServiceLocator();
    /// ConfigureRegionAdapterMappings();
    /// ConfigureDefaultRegionBehaviors();
    /// RegisterFrameworkExceptionTypes();
    /// CreateShell();
    /// InitializeShell();
    /// InitializeModules();
    /// </summary>
    public class Bootstrapper : UnityBootstrapper
    {
        #region Constants and Fields

        /// <summary>
        /// This is the region where all the Workspaces (Tabs) are inserted. e.g.:Patient Workspace
        /// </summary>
        public static readonly string WorkspacesRegion = "WorkspacesRegion";

        #endregion

        #region Methods

        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Unity.IUnityContainer"/>. May be overwritten in a derived class to add specific
        /// type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer ()
        {
            base.ConfigureContainer ();
            try
            {
                RegisterServices ();
                RegisterScreens ();
            }
            catch ( Exception ex )
            {
                Logger.Log ( ex.Message, Category.Exception, Priority.High );
                throw;
            }
        }

        /// <summary>
        /// Configures the <see cref="ServiceLocator" />,
        /// and configures <see cref="Pillar.Common.InversionOfControl.IoC" />,
        /// and bootstraps Pillar libraries.
        /// </summary>
        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();

            var unityPillarContainer = new Pillar.IoC.Unity.Container(Container);
            Pillar.Common.InversionOfControl.IoC.SetContainerProvider(() => unityPillarContainer);

            Container.RegisterInstance<Pillar.Common.InversionOfControl.IContainer>(Pillar.Common.InversionOfControl.IoC.CurrentContainer);

            Pillar.Common.InversionOfControl.IoC.Bootstrap();
        }

        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog"/> used by Prism.
        /// </summary>
        protected override void ConfigureModuleCatalog ()
        {
            RegisterInfrastructureModule ();
        }

        /// <summary>
        /// Configures the default region adapter mappings to use in the application, in order
        /// to adapt UI controls defined in XAML to use a region and register it automatically.
        /// May be overwritten in a derived class to add specific mappings required by the application.
        /// </summary>
        /// <returns>The <see cref="T:Microsoft.Practices.Prism.Regions.RegionAdapterMappings"/> instance containing all the mappings.</returns>
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings ()
        {
            var mappings = base.ConfigureRegionAdapterMappings ();
            var radTileViewRegionAdapter = Container.Resolve<RadTileViewRegionAdapter> ();
            var radTabControlRegionAdapter = Container.Resolve<RadTabControlRegionAdapter>();
            var collapsingPanelRegionAdapter = Container.Resolve<CollapsingPanelRegionAdapter>();
            mappings.RegisterMapping ( typeof( RadTileView ), radTileViewRegionAdapter );
            mappings.RegisterMapping ( typeof( RadTabControl ), radTabControlRegionAdapter );
            mappings.RegisterMapping ( typeof(CollapsingPanel), collapsingPanelRegionAdapter );

            return mappings;
        }

        /// <summary>
        /// Create the <see cref="T:Microsoft.Practices.Prism.Logging.ILoggerFacade"/> used by the bootstrapper.
        /// </summary>
        /// <returns>A <see cref="Microsoft.Practices.Prism.Logging.ILoggerFacade"/></returns>
        protected override ILoggerFacade CreateLogger ()
        {
            //// Step 1. Create configuration object 
            var config = new LoggingConfiguration ();

            // Step 2. Create targets and add them to the configuration 
            var target = new NLogLoggerTarget ();
            config.AddTarget ( "NLogLoggerTarget", target );

            // Step 3. Set target properties 
            target.Layout = "${longdate} - ${logger} - ${message}";

            // Step 4. Define rules
            var rule = new LoggingRule ( "*", LogLevel.Debug, target );
            config.LoggingRules.Add ( rule );

            // Step 5. Activate the configuration
            LogManager.Configuration = config;

            return new PrismToNLogLoggerBridge ( GetType () );
        }

        /// <summary>
        /// Creates the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog"/> used by Prism.
        /// </summary>
        /// <returns>A <see cref="Microsoft.Practices.Prism.Modularity.IModuleCatalog"/></returns>
        protected override IModuleCatalog CreateModuleCatalog ()
        {
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml (
                new Uri ( "/Rem.Ria.Shell;component/ModulesCatalog.xaml", UriKind.Relative ) );
        }

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        protected override DependencyObject CreateShell ()
        {
            Logger.Log ( "Launching ShellLoader", Category.Debug, Priority.None );
            var loader = Container.Resolve<ShellLoader>();
            return loader.InitiateLoading ();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell ()
        {
            base.InitializeShell ();
            var accessControlManager = Container.Resolve<IAccessControlManager> ();
            accessControlManager.RegisterPermissionDescriptor ( new ClientPermissionDescriptor () );

            var regionManager = Container.Resolve<IRegionManager> ();
            regionManager.Regions[WorkspacesRegion].Behaviors.Add (
                "CreateScopedRegionBehavior",
                new CreateScopedRegionBehavior () );
        }

        private void ConfigureAgatha ()
        {
            var agathaContainer = new Container ( Container );
            IoC.Container = agathaContainer;
            Container.RegisterInstance<IContainer> ( agathaContainer );
            var clientConfiguration = new ClientConfiguration ( agathaContainer )
                {
                    AsyncRequestDispatcherImplementation = typeof( AsyncRequestDispatcher )
                };

            KnownTypeProvider.Register ( typeof( LookupValueDto ) );

            KnownTypeHelper.RegisterNonGenericRequestsAndResponses ( typeof( InfrastructureModule ).Assembly );

            clientConfiguration.AddRequestAndResponseAssembly ( typeof( GetLookupValuesRequest ).Assembly );

            var genericsToApply = KnownTypeHelper.GetGenerics ( typeof( Const ).Assembly );
            KnownTypeHelper.RegisterGenerics ( genericsToApply, typeof( InfrastructureModule ).Assembly );

            KnownTypeHelper.RegisterKnownTypesFromIKnownTypeProviders ( typeof( IMetadataItemDto ).Assembly ); // metadata

            clientConfiguration.RequestProcessorImplementation = typeof( CustomAsyncRequestProcessorProxy );

            clientConfiguration.Initialize ();
        }

        private void ConfigureSercurity ()
        {
            var currentUserService = Container.Resolve<CurrentUserService> ();
            Container.RegisterInstance ( typeof( IEmergencyAccessService ), currentUserService, new ContainerControlledLifetimeManager () );
            Container.RegisterInstance ( typeof( ICurrentUserPermissionService ), currentUserService, new ContainerControlledLifetimeManager () );
            Container.RegisterInstance ( typeof( ICurrentUserContextService ), currentUserService, new ContainerControlledLifetimeManager () );
            Container.RegisterType<ISignOffService, SignOffService> ();
        }

        private void RegisterInfrastructureModule ()
        {
            var infrastructureModuleType = typeof( InfrastructureModule );
            var infrastructureModuleInfo = new ModuleInfo
                {
                    ModuleName = infrastructureModuleType.Name,
                    ModuleType = infrastructureModuleType.AssemblyQualifiedName,
                };

            ModuleCatalog.AddModule ( infrastructureModuleInfo );
        }

        private void RegisterScreens ()
        {
            Container.RegisterType<object, PopupView> ( "PopupView" );
            Container.RegisterType<object, HomePageView> ( "HomePageView" );
            Container.RegisterType<object, LogOutWarningView> ( "LogOutWarningView" );
        }

        private void RegisterServices ()
        {
            Container.RegisterType<IRegionNavigationContentLoader, SecureRegionNavigationContentLoader> ();
            Container.RegisterType<IDtoFactory, DtoFactory> ();
            Container.RegisterType<IUserDialogService, UserDialogService> ();
            Container.RegisterType<IPopupService, PopupService> ( new ContainerControlledLifetimeManager () );
            Container.RegisterType<IRedirectService, RedirectService> ();
            Container.RegisterType<INotificationService, NotificationService> ();
            Container.RegisterType<INavigationService, NavigationService> ();
            Container.RegisterType<ShellLoader, ShellLoader> ();

            var proxyBuilder = new DefaultProxyBuilder(new ModuleScope(false, true));
            Container.RegisterInstance ( typeof( ProxyGenerator ), new ProxyGenerator ( proxyBuilder ), new ContainerControlledLifetimeManager () );

            ConfigureAgatha ();
            ConfigureSercurity ();

            var metadataService = Container.Resolve<MetadataService> ();
            Container.RegisterInstance<IMetadataService> ( metadataService );
            Container.RegisterType<IMetadataItemApplicatorService, MetadataItemApplicatorService> ();
        }

        #endregion
    }
}
