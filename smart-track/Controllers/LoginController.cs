using HtmlTags;

namespace SmartTrack.Web.Controllers
{
    public class LoginController
    {

        public HtmlDocument Login()
        {
            var document = new HtmlDocument
            {
                Title = "Saying hello to you"
            };

            document
                .Add("h1")
                .Text("Hello world!")
                .Style("color", "blue");

            return document;
        }
    }

    public class LoginInputModel
    {
    }

    public class LoginViewModel
    {
    }
}