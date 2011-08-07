using System.Web;
using System.Web.Routing;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using SmartTrack.Web.Configuration;
using Spark.Web.Mvc;
using StructureMap;

namespace SmartTrack.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
            ObjectFactory.Initialize(x => x.AddRegistry(new StructureMapRegistry()));

		    FubuApplication.For<FubuMvcRegistry>()
                .StructureMapObjectFactory()
		        .Bootstrap(RouteTable.Routes);
            
            SparkEngineStarter.RegisterViewEngine(new SparkConfiguration());
		}
	}
}