<%@ Page Title="SmartTrack" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="SmartTrack.Web.Controllers.Measures.AllMeasures" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Measures" %>

<asp:Content ID="PageContent" runat="server" ContentPlaceHolderID="MainContent" >
 
    <h2>All Measures</h2>

	<div>
		<%  foreach (var measure in Model.Measures) { %>
        <div>
		    <%: measure %> <input class="measure-value-input" type="text" />
        </div>
        <% } %>
        <div>
            Another Measure? 
            <input id="another-measure-name" class="measure-name-input" type="text" /> 
            <input id="another-measure-value" class="measure-value-input" type="text" /> 
            <input type="button" value="Add" /> 
            <%= this.Urls.UrlFor<MeasuresController>(x => x.AddSingleMeasure(new AddMeasureInputModel())) %>
        </div>
        <input type="submit" value="Save Measures"/>
	</div>

</asp:Content>