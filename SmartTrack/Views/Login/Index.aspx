<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" CodeBehind="Index.aspx.cs" Inherits="SmartTrack.Web.Views.Login.Index" %>

<asp:Content ID="PageContent" runat="server" ContentPlaceHolderID="MainContent" >
 
    <h2>LOGIN</h2>
	<div id="login-container">
		<label for="Username">Username</label>
		<input id="Username" type="text" />

		<label for="Password">Username</label>
		<input id="Password" type="password" />

		<input type="submit" value="Log On!" />
	</div>

</asp:Content>