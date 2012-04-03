<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Error
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Error</h2>

    <div class="display-label">Things went horribly wrong</div>

    <div>
        <p><%: Html.ActionLink("Back to Tone Triggers", "Index") %></p>
    </div>

</asp:Content>
