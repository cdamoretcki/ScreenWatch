<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.ToneTrigger>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add New Tone Trigger</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>

            <%: Html.HiddenFor(model => model.id) %>
            <%: Html.HiddenFor(model => model.userEmail) %>

            <div class="editor-label">
                <%: Html.LabelFor(model => model.userName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.userName) %>
                <%: Html.ValidationMessageFor(model => model.userName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.lowerColorBound) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.lowerColorBound)%>
                <%: Html.ValidationMessageFor(model => model.lowerColorBound)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.upperColorBound) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.upperColorBound)%>
                <%: Html.ValidationMessageFor(model => model.upperColorBound)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.sensitivity) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.sensitivity)%>
                <%: Html.ValidationMessageFor(model => model.sensitivity)%>
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

