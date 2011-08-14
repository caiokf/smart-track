<%@ Page Language="C#" Inherits="SmartTrack.Web.Controllers.Login.LoggedInStatus" %>
<%@ Import Namespace="SmartTrack.Web.Configuration" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Login" %>

<div id="login-status">
    <% if (Model.IsLoggedIn) { %>
    
            Welcome <b><%= Model.UserName %></b>! [ <%= this.LinkTo<LogoffRequestModel>().Text("Log Out") %> ]
            [ <a href="<%= this.Urls.UrlForActionless<Join>() %>" >Join</a> ]
    <% } else { %> 

        <form method="post" action="<%= Urls.UrlFor(new LoginRequestModel()) %>">		
            <input type="submit" value="Log On!" />
        </form>

    <% } %>
 </div>