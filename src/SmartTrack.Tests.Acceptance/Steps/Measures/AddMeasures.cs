using SharpTestsEx;
using SmartTrack.Tests.Acceptance.Pages;
using TechTalk.SpecFlow;

namespace SmartTrack.Tests.Acceptance.Steps.Measures
{
    [Binding]
    public class AddMeasures : BaseSteps
    {
        [Given("I am logged in user")]
        public void IAmLoggedInUser()
        {
            Browser.Goto<AllMeasuresPage>();
        }

        [Given("I have no existing measures")]
        public void IHaveNoExistingMeasures()
        {
            var page = Page<AllMeasuresPage>();
            page.DeleteAllMeasures();
        }

        [When("I add a new measure called \"(.*)\" with unit in \"(.*)\" and save")]
        public void IAddANewMeasureCalledAndSave(string measure, string unit)
        {
            IAddANewMeasureCalled(measure, unit);

            var createMeasurePage = Page<CreateMeasurePage>();
            createMeasurePage.Save.Click();
        }

        [When("I add a new measure called \"(.*)\" with unit in \"(.*)\" and cancel")]
        public void IAddANewMeasureCalledAndCancel(string measure, string unit)
        {
            IAddANewMeasureCalled(measure, unit);

            var createMeasurePage = Page<CreateMeasurePage>();
            createMeasurePage.Cancel.Click();
        }

        [When("I add a new measure called \"(.*)\" with unit in \"(.*)\"")]
        public void IAddANewMeasureCalled(string measure, string unit)
        {
            var allMeasuresPage = Page<AllMeasuresPage>();
            allMeasuresPage.AddNewMeasureLink.Click();

            var createMeasurePage = Page<CreateMeasurePage>();
            createMeasurePage.MeasureName.TypeText(measure);
            createMeasurePage.MeasureUnit.TypeText(unit);
        }

        [Then("I (can|cannot) see \"(.*)\" measure in my measures page")]
        public void ICanSeeAMeasureInMyMeasuresPage(string canCannot, string measure)
        {
            var can = canCannot == "can";
            var page = Page<AllMeasuresPage>();
            page.IsMeasureVisible(measure).Should().Be(can);
        }

        [Then("I have exactly (.*) measure\\(s\\) in my measures page")]
        public void IOnlyHaveNMeasures(int measures)
        {
            var page = Page<AllMeasuresPage>();
            page.NumberOfMeasuresInList().Should().Be(measures);
        }
    }
}
