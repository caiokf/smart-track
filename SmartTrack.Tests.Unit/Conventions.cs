using System.Linq;
using System.Reflection;
using FubuMVC.Core;
using NUnit.Framework;
using SharpTestsEx;
using SmartTrack.Web.Http;
using SmartTrack.Web.Utils;

namespace SmartTrack.Tests.Unit
{
    [TestFixture]
    public class Conventions
    {
        [Test]
        public void every_request_model_should_specify_how_values_are_routed()
        {
            var requestModels = Assembly.GetAssembly(typeof (IHttpSession)).GetTypes()
                .Where(x => x.IsClass && x.FullName.EndsWith("Request") && x.FullName.Contains("Controllers"));

            var propertiesWithoutRouteInfo = requestModels.SelectMany(x => x.GetProperties())
                .Where(x =>
                {
                    var customAttr = x.GetCustomAttributes(false).Select(a => a.GetType());
                    var hasQueryString = customAttr.Contains(typeof(QueryStringAttribute));
                    var hasRouteInput = customAttr.Contains(typeof(RouteInputAttribute));
                    var hasDefaultRoute = customAttr.Contains(typeof(DefaultRouteAttribute));
                    return !(hasQueryString || hasRouteInput || hasDefaultRoute);
                });

            propertiesWithoutRouteInfo.Count().Should().Be(0);
        }

    }
}