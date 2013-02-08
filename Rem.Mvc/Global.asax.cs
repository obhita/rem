using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using Pillar.Common.InversionOfControl;
using Rem.Infrastructure.Mvc.Bootstrapper;
using Rem.Infrastructure.Mvc.UserContext;
using Rem.Ria.Infrastructure.Context;
using StructureMap;

namespace Rem.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new CustomDependencyResolver(IoC.CurrentContainer));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Debug.WriteLine("Session Start -IsNewSession {0}", Context.Session.IsNewSession);
            var currentUserContextService = IoC.CurrentContainer.Resolve<ICurrentUserContextService>() as CurrentUserService;
            currentUserContextService.InitializeUserSession ();
        }
    }
}