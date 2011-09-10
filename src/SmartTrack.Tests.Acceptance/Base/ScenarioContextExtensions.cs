using TechTalk.SpecFlow;
using WatiN.Core;

namespace SmartTrack.Tests.Acceptance
{
    public static class ScenarioContextExtensions
    {
         public static WebBrowser Browser (this ScenarioContext context)
         {
             return context.Get<WebBrowser>("browser");
         }
    }
}