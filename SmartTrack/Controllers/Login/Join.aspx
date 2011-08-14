<%@ Page Language="C#" Inherits="SmartTrack.Web.Controllers.Login.Join" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>SmartTrack - Join</title>
    </head>
    <body>
        
        <h2>Join</h2>

	    <div>
            <%= this.FormFor<LoginController>(x => x.JoinPost(null)) %>
            
            <div>           
               Username: <input type="text" name="Username" id="join-username" /> 
            </div>
            <div>           
               Email: <input type="text" name="Email" id="join-email" /> 
            </div>
            <div>           
               Confirm Email: <input type="text" name="ConfirmEmail" id="join-confirm-email" /> 
            </div>
            <div>           
               Password: <input type="password" name="Password" id="join-password" /> 
            </div>
            <div>           
               Username: <input type="password" name="ConfirmPassword" id="join-confirm-password" /> 
            </div>

            <input type="submit" value="Join" />

            <%= this.EndForm() %>
	    </div>

    </body>
</html>
