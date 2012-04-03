<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ScreenWatchUI.Models.ToneTrigger>>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Tone Triggers</h2>

    <table>
        <tr>
            <th>
                #
            </th>
            <th>
                User Name
            </th>
            <th>
                Lower Color Bound
            </th>
            <th>
                Upper Color Bound
            </th>
            <th>
                Sensitivity
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
                <%: Model.ElementAt(i).lowerColorBound%>
            </td>
            <td>
                <%: Model.ElementAt(i).upperColorBound%>
            </td>
            <td>
                <%: Model.ElementAt(i).sensitivity%>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id = Model.ElementAt(i).id })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id = Model.ElementAt(i).id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Add Tone Trigger", "Create") %>
    </p>

</asp:Content>

