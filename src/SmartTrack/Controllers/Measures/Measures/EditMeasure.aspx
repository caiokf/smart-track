<%@ Page Title="SmartTrack" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="SmartTrack.Web.Controllers.Measures.Measures.EditMeasure" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Measures" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Measures.Measures" %>

<asp:Content ID="PageContent" runat="server" ContentPlaceHolderID="MainContent" >
 
    <h2>Edit Measure: <%: Model.Name %></h2>

    <%= this.FormFor<MeasuresController>(x => x.EditMeasurePost(null)) %>

        <div>
            <%= new HiddenTag().Name("OriginalName").Value(Model.Name) %>
            Name: <%= this.TextBoxFor(x => x.Name).Id("measure-name") %>
            Unit: <%= this.TextBoxFor(x => x.Unit).Id("measure-unit") %>                     
        </div>
        <input type="submit" value="Save"/>

    <%= this.EndForm() %>

</asp:Content>