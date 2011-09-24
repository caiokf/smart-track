using System.Web;
using System.Web.Routing;
using FluentScheduler;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using SmartTrack.Web.Configuration;
using SmartTrack.Web.Migrations;
using StructureMap;

namespace SmartTrack.Web
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{   
            new MigrateDatabase().Execute();

            ObjectFactory.Initialize(x => x.AddRegistry<StructureMapRegistry>());
            
		    FubuApplication.For<FubuMvcRegistry>()
                .StructureMapObjectFactory()
		        .Bootstrap(RouteTable.Routes);
            
            TaskManager.Initialize(new SchedulerRegistry());
		}
	}
}