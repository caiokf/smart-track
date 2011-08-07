using FubuMVC.Core.Continuations;
using SmartTrack.Web.Http;

namespace SmartTrack.Web.Controllers.Login
{
    public class LoginController
    {
        private readonly IHttpSession httpSession;

        public LoginController(IHttpSession httpSession)
        {
            this.httpSession = httpSession;
        }

        public LoginViewModel Index()
        {
            return new LoginViewModel();  
        }
        
        public FubuContinuation Login(LoginRequestModel model)
        {
            httpSession[CurrentLoginStatus.Key] = new CurrentLoginStatus { UserName = "Cookie Monster" };
            return FubuContinuation.RedirectTo(new HomeDashboardViewModel());
        }

        public FubuContinuation Logoff(LogoffRequestModel model)
        {
            httpSession.Clear();
            return FubuContinuation.RedirectTo(new HomeDashboardViewModel());
        }
    }

    public class LogoffRequestModel { }

    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
    }
}