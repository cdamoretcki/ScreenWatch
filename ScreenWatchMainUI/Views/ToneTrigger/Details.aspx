<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.ToneTrigger>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Trigger Details</h2>

    <fieldset>
        
        <div class="display-label">User Name</div>
        <div class="display-field"><%: Model.userName %></div>
        
        <div class="display-label">Lower Color Bound</div>
        <div class="display-field"><%: Model.lowerColorBound %></div>
        
        <div class="display-label">Upper Color Bound</div>
        <div class="display-field"><%: Model.upperColorBound %></div>
        
        <div class="display-label">Sensitivity</div>
        <div class="display-field"><%: Model.sensitivity %></div>
        
    </fieldset>
    <p>
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

