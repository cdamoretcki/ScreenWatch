<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.TextTrigger>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Trigger Details</h2>

    <fieldset>
        
        <div class="display-label">User Name</div>
        <div class="display-field"><%: Model.userName %></div>
        
        <div class="display-label">Trigger Text</div>
        <div class="display-field"><%: Model.triggerString %></div>
        
    </fieldset>
    <p>

        <%: Html.ActionLink("Edit", "Edit", new { id=Model.id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

