using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace SmartTrack.Tests.Acceptance.Steps.Measures
{
    [Binding]
    public class AddMeasures
    {
        [Given("I am logged in user")]
        public void GivenIHaveEnteredSomethingIntoTheCalculator()
        {
            var driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["ServerAddress"]);
            Thread.Sleep(5000);
            driver.Close();
        }

        [Given("I have no existing measures")]
        public void WhenIPressAdd()
        {
            //ScenarioContext.Current.Pending();
        }

        [When("I add a new measure called \"(.*)\"")]
        public void ThenTheResultShouldBe(string measure)
        {
            //ScenarioContext.Current.Pending();
        }

        [Then("I can see \"(.*)\" measure in my home page")]
        public void a(string measure)
        {

            //ScenarioContext.Current.Pending();
        }

        [Then("I only have (.*) measure\\(s\\)")]
        public void b(int measures)
        {

            //ScenarioContext.Current.Pending();
        }
    }
}
