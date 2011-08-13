<%@ Page Title="SmartTrack" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="SmartTrack.Web.Controllers.Measures.CreateMeasure" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Measures" %>
<%@ Import Namespace="SmartTrack.Web.HtmlTags" %>

<asp:Content ID="PageContent" runat="server" ContentPlaceHolderID="MainContent" >
 
    <h2>Create Measures</h2>
    	
    <%= this.FormFor<MeasuresController>(x => x.CreateMeasurePost(null)) %>
    <div>
        Name: <%= this.TextBoxFor(x => x.Name).Id("measure-name") %>
        Unit: <%= this.TextBoxFor(x => x.Unit).Id("measure-unit") %>
       
    </div>
    <%= this.SubmitButton().Value("Save") %>
    <%= this.CancelButton() %>
    <%= this.EndForm() %>
	
</asp:Content>