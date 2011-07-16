using System.Linq;
using Raven.Client;
using SmartTrack.Model;

namespace SmartTrack.Web.Controllers
{
    public class LoginController
    {
        private readonly IDocumentSession session;

        public LoginController(IDocumentSession session)
        {
            this.session = session;
        }

        public LoginViewModel Login(LoginInputModel input)
        {
            var users = session.Query<User>().Take(100).ToArray();

            return new LoginViewModel();
        }
    }

    public class LoginInputModel
    {
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
    }
}