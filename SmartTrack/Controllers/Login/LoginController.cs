using FubuMVC.Core.Continuations;
using FubuMVC.WebForms;
using SmartTrack.Web.Controllers.Measures;

namespace SmartTrack.Web.Controllers.Login
{
    public class Index : FubuPage<LoginViewModel> { }

    public class LoginController
    {
        public LoginViewModel Index()
        {
            return new LoginViewModel();  
        }
        
        public FubuContinuation Login(LoginRequestModel model)
        {
            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
        }

        public FubuContinuation Logoff(LogoffRequestModel model)
        {
            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
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