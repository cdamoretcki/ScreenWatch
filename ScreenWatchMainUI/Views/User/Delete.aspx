<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete User</h2>

    <h3>Are you sure you want to delete this user?</h3>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">userName</div>
        <div class="display-field"><%: Model.userName %></div>
        
        <div class="display-label">email</div>
        <div class="display-field"><%: Model.email %></div>
        
        <div class="display-label">isMonitored</div>
        <div class="display-field"><%: Model.isMonitored %></div>
        
        <div class="display-label">isAdmin</div>
        <div class="display-field"><%: Model.isAdmin %></div>
        
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>

</asp:Content>

