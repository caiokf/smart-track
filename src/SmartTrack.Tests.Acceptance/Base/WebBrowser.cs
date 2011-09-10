using System;
using WatiN.Core;
using WatiN.Core.Native;

namespace SmartTrack.Tests.Acceptance
{
    public class WebBrowser : Browser
    {
        private readonly Browser browser;

        public WebBrowser(Browser browser)
        {
            this.browser = browser;
        }

        public BasePage Goto<T>() where T : BasePage
        {
            var page = BasePage.Get<T>(this);
            
            if (this.Url != page.Url)
                page.Visit();
            
            return page;
        }

        #region Browser

        public override void WaitForComplete(int waitForCompleteTimeOut)
        {
            browser.WaitForComplete(waitForCompleteTimeOut);
        }

        public override void Close()
        {
            browser.Close();
        }

        public override INativeBrowser NativeBrowser
        {
            get { return browser.NativeBrowser; }
        }
        
        #endregion
    }
}