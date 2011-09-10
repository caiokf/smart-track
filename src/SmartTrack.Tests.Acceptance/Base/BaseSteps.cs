using TechTalk.SpecFlow;

namespace SmartTrack.Tests.Acceptance
{
    public class BaseSteps
    {
        public WebBrowser Browser
        {
            get { return ScenarioContext.Current.Browser(); }
        }

        public T Page<T>() where T : BasePage
        {
            return BasePage.Get<T>(Browser);
        }
    }
}