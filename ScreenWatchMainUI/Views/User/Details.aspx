<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>User Settings For <%: Model.userName %></h2>

    <fieldset>
        
        <div class="display-label">User Name</div>
        <div class="display-field"><%: Model.userName %></div>
        
        <div class="display-label">E-Mail Address</div>
        <div class="display-field"><%: Model.email %></div>
        
        <div class="display-label">Is This User Monitored?</div>
        <div class="display-field"><%: Model.isMonitored == true ? "Yes" : "No"%> </div>
        
        <div class="display-label">Is This User An Administrator?</div>
        <div class="display-field"><%: Model.isAdmin == true ? "Yes": "No"%> </div>
        
    </fieldset>
    <p>

        <%: Html.ActionLink("Edit", "Edit", new { id=Model.userName }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

