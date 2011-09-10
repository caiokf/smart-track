using System;
using System.Collections.Generic;
using System.Configuration;
using SharpTestsEx;
using SmartTrack.Tests.Acceptance.Pages;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace SmartTrack.Tests.Acceptance
{
    [Binding]
    public static class StepsSetup
    {
        [BeforeScenario] 
        public static void BeforeWebScenario()
        {
            var browsers = new Dictionary<string, Func<WebBrowser>>
            {
                {"firefox", () => new WebBrowser(new FireFox()) },
                {"ie",      () => new WebBrowser(new IE()) }
            };
            
            var browserSetting = ConfigurationManager.AppSettings["Browser"];
            
            var browser = browsers[browserSetting]();
            
            browser.Should().Not.Be.Null();

            ScenarioContext.Current.Set(browser, "browser");

            ScenarioContext.Current.Browser().Goto<AllMeasuresPage>();
        } 

        [AfterScenario] 
        public static void AfterWebScenario() 
        { 
           ScenarioContext.Current.Browser().Close();
        }
    }
}