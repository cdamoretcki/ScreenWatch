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
            <th colspan="2" align="center">
                Lower Color Bound
            </th>
            <th colspan="2" align="center">
                Upper Color Bound
            </th>
            <th>
                Threshold
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
            <td name="colorBoundText" style="width: 90px">
                <%: Model.ElementAt(i).lowerColorBound%>
            </td>
            <td name="colorBoundColor" style="width: 30px"></td>
            <td name="colorBoundText" style="width: 90px">
                <%: Model.ElementAt(i).upperColorBound%>
            </td>
            <td name="colorBoundColor" style="width: 30px"></td>
            <td>
                <%: Model.ElementAt(i).sensitivity%>%
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

    <script type="text/javascript">
        var textFields = document.getElementsByName("colorBoundText");
        var colorsFields = document.getElementsByName("colorBoundColor");
        for (var i = 0; i < colorsFields.length; i++) {
            colorsFields[i].style.backgroundColor = textFields[i].innerHTML;
        }
    </script>

</asp:Content>

