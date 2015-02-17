using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using PhotoTournament.Data;
using PhotoTournament.Models;

namespace PhotoTournament
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(
                new MediaTypeHeaderValue("text/html"));


            var builder = new ContainerBuilder();
            builder.RegisterType<PhotoTournamentDbContext>().SingleInstance();
            builder.RegisterType<ApplicationDbContext>().SingleInstance();
            builder.RegisterType<RepositoryFactories>().SingleInstance();
            builder.RegisterType<RepositoryProvider>().As<IRepositoryProvider>();
            builder.RegisterType<PhotoTournamentUow>().As<IPhotoTournamentUow>();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
