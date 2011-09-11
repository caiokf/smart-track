using FluentValidation;
using FubuValidation;
using SmartTrack.Model.Repositories;
using SmartTrack.Web.Controllers.Login;

namespace SmartTrack.Web.Validation
{
    public class LoginJoinValidator : AbstractValidator<JoinInput>
    {
        public LoginJoinValidator(UserRepository userRepository)
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Username).Must(x => userRepository.Users.Named(x).Count() == 0).WithMessage("Sorry, the selected Username is already being used");

            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Email).Must(x => userRepository.Users.WithEmail(x).Count() == 0).WithMessage("Sorry, the selected Email is already being used");

            RuleFor(x => x.ConfirmEmail).NotNull().NotEmpty().WithMessage("Email Confirmation is required");
            RuleFor(x => x.ConfirmEmail).Must((input, x) => x == input.Email).WithMessage("Email Confirmation should match Email");
            RuleFor(x => x.ConfirmEmail).EmailAddress();

            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required");

            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().WithMessage("Password Confirmation is required");
            RuleFor(x => x.ConfirmPassword).Must((input, x) => x == input.Password).WithMessage("Password Confirmation should match Password");
        }
    }
}