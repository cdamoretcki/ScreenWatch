<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ScreenWatchUI.Models.User>>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ScreenWatch Users</h2>

    <table>
        <tr>
            <th>
                User Name
            </th>
            <th>
                E-Mail Address
            </th>
            <th>
                Is This User Monitored?
            </th>
            <th>
                Is This User An Administrator?
            </th>
            <th>
                Actions
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.userName %>
            </td>
            <td>
                <%: item.email %>
            </td>
            <td align="center">
                <%: item.isMonitored == true ? "Yes" : "No" %>
            </td>
            <td align="center">
                <%: item.isAdmin == true ? "Yes" : "No"%>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.userName }) %> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.userName })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Add User", "Create") %>
    </p>

</asp:Content>

