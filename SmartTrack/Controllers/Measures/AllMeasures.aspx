<%@ Page Title="SmartTrack" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="SmartTrack.Web.Controllers.Measures.AllMeasures" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Measures" %>

<asp:Content ID="PageContent" runat="server" ContentPlaceHolderID="MainContent" >
 
    <h2>All Measures</h2>

	<div>
        <table>
		    <%  foreach (var measure in Model.Measures) { %>
            <tr>
                <td class="measure-table-cell">
                    <%= this.LinkTo(new EditThisMeasureInputModel { OriginalName = measure.Name }).Text("Edit") %>
                </td>
                <td class="measure-table-cell">
                    <%: measure.Name %> 
                </td>
                <td class="measure-table-cell">
                    <input class="measure-value" type="text" />
                </td>
                <td class="measure-table-cell">    
                    <%: measure.Unit %> 
                </td>
            </tr>
            <% } %>
        </table>

        <div>           
            Another Measure? 
            <input id="another-measure-name" class="measure-name-input" type="text" /> 
            <input id="another-measure-value" class="measure-value" type="text" /> 
            <input type="button" value="Add" /> 
            <%= this.Urls.UrlFor<MeasuresController>(x => x.AddSingleMeasure(new AddMeasureInputModel())) %>
        </div>
        
        

        <input type="submit" value="Save Measures"/>
	</div>

</asp:Content>