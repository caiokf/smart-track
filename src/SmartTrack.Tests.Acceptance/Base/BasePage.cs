using System;
using System.Configuration;
using WatiN.Core;

namespace SmartTrack.Tests.Acceptance
{
    public abstract class BasePage : Page
    {
        protected readonly WebBrowser browser;

        protected BasePage(WebBrowser browser)
        {
            this.browser = browser;
        }

        protected string ServerAdress { get { return ConfigurationManager.AppSettings["ServerAddress"]; } }
        
        public abstract string Url { get; }

        public void Visit()
        {
            browser.GoTo(this.Url);
        }

        public static T Get<T>(WebBrowser webBrowser) where T : BasePage
        {
            return (T)Activator.CreateInstance(typeof(T), new[] { webBrowser }, new object[] { });
        }
    }
}