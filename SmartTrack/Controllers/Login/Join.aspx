<%@ Page Language="C#" Inherits="SmartTrack.Web.Controllers.Login.Join" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Login" %>
<%@ Import Namespace="SmartTrack.Web.HtmlTags" %>
<%@ Import Namespace="SquishIt.Framework" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>SmartTrack - Join</title>
        <%= Bundle.Css()
			.Add("~/Content/styles/colors.less")
			.Add("~/Content/styles/site.less")
			.Add("~/Content/styles/site.css")
		    .Render("~/Content/styles/login_join_#.css")      
		%>
        <%= Bundle.JavaScript()
			.Add("~/Content/scripts/jquery/jquery-1.6.2.min.js")
			.Add("~/Content/scripts/less-1.1.3.min.js")
                .Add("~/Scripts/watermark.js")
            .Render("~/Content/scripts/login_join_#.js")
		%>
    </head>
    <body>
        
        <h2>Join</h2>

	    <div style="text-align: left;">
            <%= this.FormFor<LoginController>(x => x.JoinPost(null)) %>
            
            <div>           
               <%= this.Textbox().Name("Username").Id("join-username").Watermark("Username") %> Username
            </div>
            <div>       
                <%= this.Textbox().Name("Email").Id("join-email").Watermark("Email") %> Email  
            </div>
            <div>           
                <%= this.Textbox().Name("ConfirmEmail").Id("join-confirm-email").Watermark("Confirm your Email") %> Confirm your Email
            </div>
            <div>           
                <%= this.Password().Name("Password").Id("join-password").Watermark("Password") %> Password
            </div>
            <div>           
            <%= this.Password().Name("ConfirmPassword").Id("join-confirm-password").Watermark("Confirm your password") %> Confirm your Password
            </div>

            <%= this.SubmitButton().Value("Join") %>

            <%= this.EndForm() %>
	    </div>

    </body>
</html>
