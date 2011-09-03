using System.Collections.Generic;
using FubuLocalization;
using FubuValidation;
using SmartTrack.Model.Repositories;
using SmartTrack.Web.Behaviors.Validation;
using SmartTrack.Web.Controllers.Login;
using SmartTrack.Web.Utils.Extensions;
using SmartTrack.Web.Validation.Rules;

namespace SmartTrack.Web.Validation
{
    public class LoginJoinKeys : StringToken
    {
        protected LoginJoinKeys(string key, string defaultValue) : base(key, defaultValue) { }

        public static readonly StringToken FieldRequired = new LoginJoinKeys("LoginJoinRequired", "{0} is required");
        public static readonly StringToken FieldShouldMatch = new LoginJoinKeys("LoginJoinShouldMatch", "{0} should match {1}");
        public static readonly StringToken AlreadyTaken = new LoginJoinKeys("LoginJoinShouldMatch", "Sorry, {0} ({1}) was already taken");
        public static readonly StringToken IsValidEmailAddress = new LoginJoinKeys("LoginJoinEmailAddress", "Email not valid");
    }

    public class LoginJoinRequiredFields : IValidate<JoinInput>
    {
        public IEnumerable<NotificationMessage> Validate(JoinInput input)
        {
            var errors = new NotificationMessageErrors<JoinInput>();

            if (input.Username.IsEmpty()) 
                errors.Add(x => x.Username, LoginJoinKeys.FieldRequired).AddSubstitution("0", "Username");

            if (input.Email.IsEmpty())
                errors.Add(x => x.Email, LoginJoinKeys.FieldRequired).AddSubstitution("0", "Email");

            if (input.Password.IsEmpty())
                errors.Add(x => x.Password, LoginJoinKeys.FieldRequired).AddSubstitution("0", "Password");

            if (input.ConfirmEmail.IsEmpty())
                errors.Add(x => x.ConfirmEmail, LoginJoinKeys.FieldRequired).AddSubstitution("0", "Email Confirmation");

            if (input.ConfirmPassword.IsEmpty())
                errors.Add(x => x.ConfirmPassword, LoginJoinKeys.FieldRequired).AddSubstitution("0", "Password Confirmation");

            return errors.ToList();
        }
    }

    public class LoginJoinPasswordShouldMatchConfirmation : IValidate<JoinInput>
    {
        public IEnumerable<NotificationMessage> Validate(JoinInput input)
        {
            var errors = new NotificationMessageErrors<JoinInput>();

            if ((!input.Password.IsEmpty() && !input.ConfirmPassword.IsEmpty()) && input.Password != input.ConfirmPassword)
                errors.Add(x => x.ConfirmPassword, LoginJoinKeys.FieldShouldMatch)
                    .AddSubstitution("0", "Password Confirmation")
                    .AddSubstitution("1", "Password");

            return errors.ToList();
        }
    }

    public class LoginJoinEmailShouldMatchConfirmation : IValidate<JoinInput>
    {
        public IEnumerable<NotificationMessage> Validate(JoinInput input)
        {
            var errors = new NotificationMessageErrors<JoinInput>();

            if ((!input.Email.IsEmpty() && !input.ConfirmEmail.IsEmpty()) && input.Email != input.ConfirmEmail)
                errors.Add(x => x.ConfirmEmail, LoginJoinKeys.FieldShouldMatch)
                    .AddSubstitution("0", "Email Confirmation")
                    .AddSubstitution("1", "Email");

            return errors.ToList();
        }
    }

    public class LoginJoinUsernameShouldBeUnique : IValidate<JoinInput>
    {
        private readonly UserRepository userRepository;

        public LoginJoinUsernameShouldBeUnique(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<NotificationMessage> Validate(JoinInput input)
        {
            var errors = new NotificationMessageErrors<JoinInput>();

            if (!input.Username.IsEmpty())
            {
                if (userRepository.Users.Named(input.Username).Count() > 0)
                {
                    errors.Add(x => x.Username, LoginJoinKeys.AlreadyTaken)
                        .AddSubstitution("0", "Username")
                        .AddSubstitution("1", input.Username);
                }
            }

            return errors.ToList();
        }
    }

    public class LoginJoinEmailShouldBeUnique : IValidate<JoinInput>
    {
        private readonly UserRepository userRepository;

        public LoginJoinEmailShouldBeUnique(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<NotificationMessage> Validate(JoinInput input)
        {
            var errors = new NotificationMessageErrors<JoinInput>();

            if (!input.Email.IsEmpty())
            {
                if (userRepository.Users.WithEmail(input.Email).Count() > 0)
                {
                    errors.Add(x => x.Email, LoginJoinKeys.AlreadyTaken)
                        .AddSubstitution("0", "Email")
                        .AddSubstitution("1", input.Email);
                }
            }

            return errors.ToList();
        }
    }
    
    public class LoginJoinEmailShouldBeInCorrectFormat : IValidate<JoinInput>
    {
        public IEnumerable<NotificationMessage> Validate(JoinInput input)
        {
            var errors = new NotificationMessageErrors<JoinInput>();

            if (!input.Email.IsEmpty() && !input.Email.IsValidEmailAddress())
            {
                errors.Add(x => x.Email, LoginJoinKeys.IsValidEmailAddress);
            }

            return errors.ToList();
        }
    }
}