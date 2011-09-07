using System.Linq;
using Moq;
using NUnit.Framework;
using SharpTestsEx;
using SmartTrack.Model.Measures;
using SmartTrack.Model.Repositories;
using SmartTrack.Web.Controllers.Login;
using SmartTrack.Web.Validation;

namespace SmartTrack.Tests.Unit.Validations
{
    [TestFixture]
    public class LoginJoinInputValidationTests
    {
        private Mock<UserRepository> repository;
        private LoginJoinValidator validator;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<UserRepository>(null);
            validator = new LoginJoinValidator(repository.Object);
        }

        [Test]
        public void email_is_required()
        {
            var errors = validator.Validate(new JoinInput()).Errors;

            errors.Where(x => x.PropertyName == "Email")
                .Where(x => x.ErrorMessage.Contains("required"))
                .Count().Should().Be(1);
        }

        [Test]
        public void username_is_required()
        {
            var errors = validator.Validate(new JoinInput()).Errors;

            errors.Where(x => x.PropertyName == "Username")
                .Where(x => x.ErrorMessage.Contains("required"))
                .Count().Should().Be(1);
        }

        [Test]
        public void password_is_required()
        {
            var errors = validator.Validate(new JoinInput()).Errors;

            errors.Where(x => x.PropertyName == "Password")
                .Where(x => x.ErrorMessage.Contains("required"))
                .Count().Should().Be(1);
        }

        [Test]
        public void email_confirmation_should_match_email()
        {
            var input = new JoinInput {Email = "any", ConfirmEmail = "any other"};

            var errors = validator.Validate(input).Errors;

            errors.Where(x => x.PropertyName == "ConfirmEmail")
                .Where(x => x.ErrorMessage.Contains("should match Email"))
                .Count().Should().Be(1);
        }

        [Test]
        public void password_confirmation_should_match_password()
        {
            var input = new JoinInput { Password = "any", ConfirmPassword = "any other" };

            var errors = validator.Validate(input).Errors;

            errors.Where(x => x.PropertyName == "ConfirmPassword")
                .Where(x => x.ErrorMessage.Contains("should match Password"))
                .Count().Should().Be(1);
        }

        [Test]
        [TestCase("user@domain.com", true)]
        [TestCase("user.domain", false)]
        [TestCase("user@domain", false)]
        [TestCase("user@.com", false)]
        [TestCase("@domain.com", false)]
        public void email_should_be_valid(string email, bool shouldBeValid)
        {
            var input = new JoinInput { Email = email };

            validator.Validate(input).Errors
                .Where(x => x.PropertyName == "Email")
                .Where(x => x.ErrorMessage.Contains("not a valid"))
                .Count().Should().Be(shouldBeValid ? 0 : 1);
        }

        [Test]
        public void email_should_be_unique()
        {
            repository.SetupGet(x => x.Users).Returns(new[] { new User("", "", "email") }.AsQueryable);

            var unavailableEmail = new JoinInput { Email = "email" };
            validator.Validate(unavailableEmail).Errors
                .Where(x => x.PropertyName == "Email")
                .Where(x => x.ErrorMessage.Contains("already"))
                .Count().Should().Be(1);

            var availableEmail = new JoinInput { Email = "otherEmail" };
            validator.Validate(availableEmail).Errors
                .Where(x => x.PropertyName == "Email")
                .Where(x => x.ErrorMessage.Contains("already"))
                .Count().Should().Be(0);
        }

        [Test]
        public void username_should_be_unique()
        {
            repository.SetupGet(x => x.Users).Returns(new[] {new User("user", "", "")}.AsQueryable);

            var unavailableUsername = new JoinInput { Username = "user" };
            validator.Validate(unavailableUsername).Errors
                .Where(x => x.PropertyName == "Username")
                .Where(x => x.ErrorMessage.Contains("already"))
                .Count().Should().Be(1);

            var availableUsername = new JoinInput {Username = "otherUser"};
            validator.Validate(availableUsername).Errors
                .Where(x => x.PropertyName == "Username")
                .Where(x => x.ErrorMessage.Contains("already"))
                .Count().Should().Be(0);
        }
    }
}