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
               <%= this.Textbox().Watermark("Username").Name("Username").Id("join-username")%> Username
            </div>
            <div>       
                <%= this.Textbox().Watermark("Email").Name("Email").Id("join-email")%> Email  
            </div>
            <div>           
                <%= this.Textbox().Watermark("Confirm your Email").Name("ConfirmEmail").Id("join-confirm-email")%> Confirm your Email
            </div>
            <div>           
                <%= this.Password().Watermark("Password").Name("Password").Id("join-password")%> Password
            </div>
            <div>           
            <%= this.Password().Watermark("Confirm your password").Name("ConfirmPassword").Id("join-confirm-password")%> Confirm your Password
            </div>

            <%= this.SubmitButton().Value("Join") %>

            <%= this.EndForm() %>
	    </div>

    </body>
</html>
