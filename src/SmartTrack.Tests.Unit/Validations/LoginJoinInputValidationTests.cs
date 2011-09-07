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
        [Test]
        public void email_is_required()
        {
            var input = new JoinInput();

            var errors = new LoginJoinRequiredFields().Validate(input);

            errors.SelectMany(x => x.ToValidationErrors())
                .Where(x => x.field == "Email")
                .Where(x => x.message.Contains("required"))
                .Count().Should().Be(1);
        }

        [Test]
        public void username_is_required()
        {
            var input = new JoinInput();

            var errors = new LoginJoinRequiredFields().Validate(input);

            errors.SelectMany(x => x.ToValidationErrors())
                .Where(x => x.field == "Username")
                .Where(x => x.message.Contains("required"))
                .Count().Should().Be(1);
        }

        [Test]
        public void password_is_required()
        {
            var errors = new LoginJoinRequiredFields().Validate(new JoinInput());

            errors.SelectMany(x => x.ToValidationErrors())
                .Where(x => x.field == "Password")
                .Where(x => x.message.Contains("required"))
                .Count().Should().Be(1);
        }

        [Test]
        public void email_confirmation_should_match_email()
        {
            var input = new JoinInput {Email = "any", ConfirmEmail = "any other"};

            var errors = new LoginJoinEmailShouldMatchConfirmation().Validate(input);

            errors.SelectMany(x => x.ToValidationErrors())
                .Where(x => x.field == "ConfirmEmail")
                .Where(x => x.message.Contains("should match Email"))
                .Count().Should().Be(1);
        }

        [Test]
        public void password_confirmation_should_match_password()
        {
            var input = new JoinInput { Password = "any", ConfirmPassword = "any other" };

            var errors = new LoginJoinPasswordShouldMatchConfirmation().Validate(input);

            errors.SelectMany(x => x.ToValidationErrors())
                .Where(x => x.field == "ConfirmPassword")
                .Where(x => x.message.Contains("should match Password"))
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

            var errors = new LoginJoinEmailShouldBeInCorrectFormat().Validate(input);

            if (shouldBeValid)
            {
                errors.Count().Should().Be(0);
                return;
            }

            errors.SelectMany(x => x.ToValidationErrors())
                .Where(x => x.field == "Email")
                .Where(x => x.message.Contains("not valid"))
                .Count().Should().Be(1);
        }

        [Test]
        public void email_should_be_unique()
        {
            var repository = new Mock<UserRepository>(null);
            repository.SetupGet(x => x.Users).Returns(new[] { new User("", "", "email") }.AsQueryable);

            var validator = new LoginJoinEmailShouldBeUnique(repository.Object);

            validator.Validate(new JoinInput { Email = "email" }).Count().Should().Be(1);
            validator.Validate(new JoinInput { Email = "otherEmail" }).Count().Should().Be(0);
        }

        [Test]
        public void username_should_be_unique()
        {
            var repository = new Mock<UserRepository>(null);
            repository.SetupGet(x => x.Users).Returns(new[] {new User("user", "", "")}.AsQueryable);

            var validator = new LoginJoinUsernameShouldBeUnique(repository.Object);

            validator.Validate(new JoinInput { Username = "user" }).Count().Should().Be(1);
            validator.Validate(new JoinInput { Username = "otherUser" }).Count().Should().Be(0);
        }
    }
}