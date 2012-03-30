<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete User</h2>

    <h3>Are you sure you want to delete <%: Model.userName %>?</h3>
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
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to ScreenWatch Users", "Index") %>
        </p>
    <% } %>

</asp:Content>

