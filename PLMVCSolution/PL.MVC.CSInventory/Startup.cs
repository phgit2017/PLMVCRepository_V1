using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Optimization;
using PL.MVC.CSInventory.Infrastructure;
using Autofac;
using System.Reflection;

[assembly: OwinStartup(typeof(PL.MVC.CSInventory.Startup))]

namespace PL.MVC.CSInventory
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            DependencyInjection resolver = new DependencyInjection();
            ContainerBuilder builder = new ContainerBuilder();
            IContainer myContainer = null;
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            builder = resolver.OnConfigure(builder);

            if (myContainer == null)
            {
                myContainer = builder.Build();
            }
            else
            {
                builder.Update(myContainer);
            }

            DependencyResolver.SetResolver(new AutofacDependencyResolver(myContainer));
        }
    }
}
