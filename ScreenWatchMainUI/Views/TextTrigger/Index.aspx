<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ScreenWatchUI.Models.TextTrigger>>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Text Triggers</h2>

    <table>
        <tr>
            <th>
                #
            </th>
            <th>
                User Name
            </th>
            <th>
                Trigger Text
            </th>
            <th>
                Actions
            </th>
        </tr>

    <% for (int i = 0; i < Model.Count(); i++ )
       { %>
    
        <tr>
            <td>
                <%: i+1 %>
            </td>
            <td>
                <%: Model.ElementAt(i).userName%>
            </td>
            <td>
                <%: Model.ElementAt(i).triggerString%>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id = Model.ElementAt(i).id })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id = Model.ElementAt(i).id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Add Text Trigger", "Create") %>
    </p>

</asp:Content>

