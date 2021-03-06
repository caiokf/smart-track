﻿<%@ Page Language="C#" Inherits="SmartTrack.Web.Controllers.Login.Join" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Login" %>
<%@ Import Namespace="SmartTrack.Web.HtmlTags" %>
<%@ Import Namespace="SquishIt.Framework" %>
<%@ Import Namespace="SquishIt.Framework.Css.Compressors" %>
<%@ Import Namespace="SquishIt.Framework.JavaScript.Minifiers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>SmartTrack - Join</title>
        <%= Bundle.Css()
            .Add("~/Content/themes/base/jquery.ui.all.css")
            .Add("~/Content/styles/colors.less")
			.Add("~/Content/styles/site.less")
			.Add("~/Content/styles/site.css")
            .WithCompressor(CssCompressors.YuiCompressor)
		    .Render("~/Content/styles/login_join_#.css")      
		%>
        <%= Bundle.JavaScript()
            .Add_JQuery()
            .Add_JQueryUi()
            .Add_LinqJs()
			.Add_LessHandler()
            .Add_Underscore()
			.Add("~/Content/scripts/less-1.1.3.min.js")
            .WithMinifier(JavaScriptMinifiers.Yui)
            .Render("~/Content/scripts/login_join_#.js")
		%>

    </head>
    <body>
        
        <h2>Join</h2>

	    <div id="content" style="text-align: left;">
            <%= this.FormFor<LoginController>(x => x.JoinPost(null))
                    .WithAjaxValidation()
                    .Id("join-form")
            %>
                        
            <%= this.ErrorSummary() %>

            <div>           
               <%= this.Textbox().Name("Username").Id("join-username").Watermark("Username") %> Username
               <%= this.ValidationFor("join-username") %>
            </div>
            <div>       
                <%= this.Textbox().Name("Email").Id("join-email").Watermark("Email") %> Email  
                <%= this.ValidationFor("join-email")%>
            </div>
            <div>           
                <%= this.Textbox().Name("ConfirmEmail").Id("join-confirm-email").Watermark("Confirm your Email") %> Confirm your Email
                <%= this.ValidationFor("join-confirm-email")%>
            </div>
            <div>           
                <%= this.Password().Name("Password").Id("join-password").Watermark("Password") %> Password
                <%= this.ValidationFor("join-password")%>
            </div>
            <div>           
                <%= this.Password().Name("ConfirmPassword").Id("join-confirm-password").Watermark("Confirm your password") %> Confirm your Password
                <%= this.ValidationFor("join-confirm-password")%>
            </div>

            <%= this.SubmitButton().Value("Join") %>

            <%= this.EndForm() %>
	    </div>

    </body>
</html>
