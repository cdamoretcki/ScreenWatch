<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.TextTrigger>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete Trigger</h2>

    <h3>Are you sure you want to delete this text trigger?</h3>
    <fieldset>
        
        <div class="display-label">User Name</div>
        <div class="display-field"><%: Model.userName %></div>
        
        <div class="display-label">Trigger String</div>
        <div class="display-field"><%: Model.triggerString %></div>
        
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
		    <input type="submit" value="Delete" /> |
		    <%: Html.ActionLink("Back to ScreenWatch Text Triggers", "Index") %>
        </p>
    <% } %>

</asp:Content>

