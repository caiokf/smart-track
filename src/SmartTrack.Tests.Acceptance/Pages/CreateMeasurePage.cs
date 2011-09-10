using WatiN.Core;
using WatiN.CssSelectorExtensions;

namespace SmartTrack.Tests.Acceptance.Pages
{
    public class CreateMeasurePage : BasePage
    {
        public CreateMeasurePage(WebBrowser browser) : base(browser) { }
        
        public override string Url { get { throw new System.NotImplementedException(); } }

        public TextField MeasureName
        {
            get { return browser.CssSelect("#measure-name") as TextField; }
        }

        public TextField MeasureUnit
        {
            get { return browser.CssSelect("#measure-unit") as TextField; }
        }

        public Element Save
        {
            get { return browser.CssSelect("#save-button"); }
        }

        public Element Cancel
        {
            get { return browser.CssSelect("#cancel-button"); }
        }
    }
}