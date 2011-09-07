using FubuMVC.Core;
using FubuMVC.WebForms;
using SmartTrack.Model.Measures;
using SmartTrack.Web.Utils;

namespace SmartTrack.Web.Controllers.Login
{
    public class LoggedInStatus : FubuPage<LoggedInStatusViewModel> { }

    public class LoggedInStatusController
    {
        private readonly User user;

        public LoggedInStatusController(User user)
        {
            this.user = user;
        }

        [FubuPartial]
        public LoggedInStatusViewModel Status(LoggedInStatusRequest request)
        {
            return new LoggedInStatusViewModel
            {
                IsLoggedIn = user != null,
                UserName = user != null ? user.Name : ""
            };
        }
    }
    
    public class LoggedInStatusRequest
    {
        // Will come in from request headers (i.e. "User-Agent")
        [DefaultRoute] public string UserAgent { get; set; }

        // Will come in from request property (i.e. HttpContext.Current.Request.IsLocal)
        [DefaultRoute] public bool IsLocal { get; set; }
    }

    public class LoggedInStatusViewModel
    {
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
    }
}