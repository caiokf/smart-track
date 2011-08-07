<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SmartTrack.Web.Views.Login.Index" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Login" %>

<body>
asd
as
dasd
	<form method="post" action="<%= Urls.UrlFor(new LoginRequestModel()) %>">
        
        <h2>LOGIN</h2>
	    <div id="login-container">
		    <label for="Username">Username</label>
		    <input id="Username" type="text" />

		    <label for="Password">Username</label>
		    <input id="Password" type="password" />

		    <input type="submit" value="Log On!" />
	    </div>

	</form>
</body>
