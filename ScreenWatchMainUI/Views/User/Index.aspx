<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ScreenWatchUI.Models.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Saved Users</h2>

    <table>
        <tr>
            <th></th>
            <th>
                User Name
            </th>
            <th>
                E-Mail Address
            </th>
            <th>
                Monitored?
            </th>
            <th>
                Administrator?
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.userName }) %> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.userName })%>
            </td>
            <td>
                <%: item.userName %>
            </td>
            <td>
                <%: item.email %>
            </td>
            <td>
                <%: item.isMonitored %>
            </td>
            <td>
                <%: item.isAdmin %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

