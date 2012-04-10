<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.TextTrigger>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Trigger</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Settings for this trigger</legend>
            
            <%: Html.HiddenFor(model => model.id) %>
            <%: Html.HiddenFor(model => model.userEmail) %>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.userName) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownListFor(model => model.userName, Model.userList) %>
                <%: Html.ValidationMessageFor(model => model.userName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.triggerString) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.triggerString)%>
                <%: Html.ValidationMessageFor(model => model.triggerString)%>
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

