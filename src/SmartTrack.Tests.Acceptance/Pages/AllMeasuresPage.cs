using System.Linq;
using Fizzler;
using WatiN.Core;
using WatiN.CssSelectorExtensions;

namespace SmartTrack.Tests.Acceptance.Pages
{
    public class AllMeasuresPage : BasePage
    {
        public AllMeasuresPage(WebBrowser browser) : base(browser) { }

        public override string Url { get { return this.ServerAdress; } }
        
        public Element AddNewMeasureLink
        {
            get { return browser.CssSelect("#add-another-measure"); }
        }

        public bool IsMeasureVisible(string measure)
        {
            return browser.CssSelectAll("td.measure-table-col-name").Select(x => x.Text == measure).Count() > 0;
        }
        
        public int NumberOfMeasuresInList()
        {
            return browser.CssSelectAll("td.measure-table-col-name").Count();
        }

        public void DeleteAllMeasures()
        {
            browser.CssSelectAll("td.measure-table-col-name").ToList().ForEach(x => DeleteMeasureNamed(x.Text));
        }

        public void DeleteMeasureNamed(string name)
        {
            browser.CssSelectAll("td.measure-table-col-name").ToList().ForEach(x =>
            {
                if (x.Text != name) return;
                x.Parent.CssSelect("td.measure-table-col-delete a").Click();
            });
        }
    }
}