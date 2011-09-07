using System.Web;
using System.Web.Routing;
using FluentScheduler;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using SmartTrack.Web.Configuration;
using SmartTrack.Web.Http.Behaviors.Transactions;
using SmartTrack.Web.Migrations;
using Spark.Web.Mvc;
using StructureMap;

namespace SmartTrack.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{   
            new MigrateDatabase().Execute();

            ObjectFactory.Initialize(x => x.AddRegistry<StructureMapRegistry>());
            
            TaskManager.Initialize(new SchedulerRegistry());

		    FubuApplication.For<FubuMvcRegistry>()
                .StructureMapObjectFactory()
		        .Bootstrap(RouteTable.Routes);
            
            SparkEngineStarter.RegisterViewEngine(new SparkConfiguration());
		}
	}
}