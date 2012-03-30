<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.User>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add New User</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.userName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.userName) %>
                <%: Html.ValidationMessageFor(model => model.userName) %>
            </div>
            
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
                <input type="submit" value="Add" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

