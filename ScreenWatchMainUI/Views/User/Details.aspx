<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">Username</div>
        <div class="display-field"><%: Model.userName %></div>
        
        <div class="display-label">E-Mail Address</div>
        <div class="display-field"><%: Model.email %></div>
        
        <div class="display-label">Is The User Monitored?</div>
        <div class="display-field"><%: Model.isMonitored %></div>
        
        <div class="display-label">Is the User An Administrator?</div>
        <div class="display-field"><%: Model.isAdmin %></div>
        
    </fieldset>
    <p>

        <%: Html.ActionLink("Edit", "Edit", new { id=Model.userName }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

