﻿<%@ Page Title="SmartTrack" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="SmartTrack.Web.Controllers.Measures.Measures.AllMeasures" %>
<%@ Import Namespace="SmartTrack.Web.HtmlTags" %>
<%@ Import Namespace="SquishIt.Framework" %>
<%@ Import Namespace="SmartTrack.Web.Controllers.Measures.Measures" %>
<%@ Import Namespace="SmartTrack.Web.Utils.Extensions" %>

<asp:Content ID="PageScripts" runat="server" ContentPlaceHolderID="ScriptsContent" >
    <script type="text/javascript">
        var saveMeasuresUrl = '<%= Urls.UrlFor<MeasuresController>(x => x.SaveTodaysMeasurements(null)) %>';
        var model = <%= Model.ToJson() %>;
    </script>
    
    <%= Bundle.JavaScript()
		.Add("~/Scripts/Measures/allmeasures.js")
        .Render("~/Content/scripts/allmeasures_#.js")
	%>
</asp:Content>
<asp:Content ID="PageContent" runat="server" ContentPlaceHolderID="MainContent" >
 
    <h2>All Measures</h2>

	<div>
        <table id="measures-table">
		    <%  foreach (var measure in Model.Measures) { %>
            <tr>
                <td class="measure-table-cell measure-table-col-delete">
                    <%= this.LinkWithConfirmationTo(new DeleteMeasureInput { Measure = measure.Name }, "Mijo").Text("Delete")%>
                </td>
                <td class="measure-table-cell measure-table-col-edit">
                    <%= this.LinkTo(new EditMeasureRequest { OriginalName = measure.Name }).Text("Edit") %>
                </td>
                <td class="measure-table-cell measure-table-col-name">
                    <%= measure.Name %>
                </td>
                <td class="measure-table-cell measure-table-col-value">
                    <%= new HtmlTag("input").Attr("type","text").AddClass("measure-value") %>
                </td>
                <td class="measure-table-cell measure-table-col-unit">    
                    <%: measure.Unit %>
                </td>
            </tr>
            <% } %>
        </table>

        <div>           
            <%= this.LinkTo<MeasuresController>(x => x.CreateMeasure())
                .Id("add-another-measure")
                .Text("Another Measure?") %>
        </div>
        
        <%= this.Button().Value("Save").Id("save-all-measures") %>
	</div>

</asp:Content>