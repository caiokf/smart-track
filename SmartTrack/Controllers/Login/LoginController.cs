using FubuMVC.Core.Continuations;
using FubuMVC.WebForms;
using FubuValidation;
using SmartTrack.Model.Measures;
using SmartTrack.Model.Repositories;
using SmartTrack.Web.Controllers.Measures;
using SmartTrack.Web.Http.Behaviors.ActionlessViews;
using SmartTrack.Web.Http.Behaviors.Validation.Rules;

namespace SmartTrack.Web.Controllers.Login
{
    public class Index : FubuPage<LoginViewModel> { }
    public class Join : FubuPage, IAmActionless { }
    public class ForgotPassword : FubuPage, IAmActionless { }
    public class LoginWithOpenId: FubuPage { }

    public class LoginController
    {
        private readonly UserRepository userRepository;

        public LoginController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public LoginViewModel Index()
        {
            return new LoginViewModel();  
        }

        public FubuContinuation JoinPost(JoinInput input)
        {
            var user = new User(input.Username, input.Password, input.Email);
            userRepository.Save(user);

            return FubuContinuation.RedirectTo<MeasuresController>(x => x.AllMeasures());
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

    public class JoinInput
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [ConfirmEqualsTo("Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [ConfirmEqualsTo("Password")]
        public string ConfirmPassword { get; set; }
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