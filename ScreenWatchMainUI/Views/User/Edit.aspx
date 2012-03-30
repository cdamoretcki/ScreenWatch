<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit this user's settings</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Settings for <%: Html.DisplayTextFor(model => model.userName) %></legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.email) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.email) %>
                <%: Html.ValidationMessageFor(model => model.email) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.isMonitored) %>
            </div>
            <div class="editor-field">
                <%: Html.RadioButtonFor(model => model.isMonitored, "true", ViewData["isMonitored"] == "true")%> Yes
                <%: Html.RadioButtonFor(model => model.isMonitored, "false", ViewData["isMonitored"] == "false")%> No
                <%: Html.ValidationMessageFor(model => model.isMonitored) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.isAdmin) %>
            </div>
            <div class="editor-field">
                <%: Html.RadioButtonFor(model => model.isAdmin, "true", ViewData["isAdmin"] == "true") %> Yes
                <%: Html.RadioButtonFor(model => model.isAdmin, "false", ViewData["isAdmin"] == "false") %> No
                <%: Html.ValidationMessageFor(model => model.isAdmin) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

