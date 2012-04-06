<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.ToneTrigger>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<link type="text/css" rel="stylesheet" href="../Scripts/miniColors/jquery.miniColors.css" />
	<script type="text/javascript" src="../Scripts/jquery/js/jquery-1.7.1.min.js"></script>
	<script type="text/javascript" src="../Scripts/miniColors/jquery.miniColors.js"></script>		
    <script type="text/javascript">
        $(document).ready(function () {
            $(".color-picker").miniColors({ letterCase: 'uppercase' });
        });
    </script>

    <h2>Trigger Details</h2>

    <fieldset>
        
        <div class="display-label">User Name</div>
        <div class="display-field"><%: Model.userName %></div>
        
        <div class="display-label">Lower Color Bound</div>
        <div class="display-field">
            <table style="border: none;">
                <tr>
                    <td name="colorBoundText" style="width: 90px; border: none;">
                        <%: Model.lowerColorBound %>
                    </td>
                    <td name="colorBoundColor" style="width: 30px; border: 1px solid #E8EEF4;"></td>
                </tr>
            </table>
        </div>
        <div class="display-label">Upper Color Bound</div>
        <div class="display-field">
            <table style="border: none;">
                <tr>
                    <td name="colorBoundText" style="width: 90px; border: none;">
                        <%: Model.upperColorBound %>
                    </td>
                    <td name="colorBoundColor" style="width: 30px; border: 1px solid #E8EEF4;"></td>
                </tr>
            </table>
        </div>
            
        
        <div class="display-label">Percentage of Screen</div>
        <div class="display-field"><%: Model.sensitivity %>%</div>
        
    </fieldset>
    <p>
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

    <script type="text/javascript">
        var textFields = document.getElementsByName("colorBoundText");
        var colorsFields = document.getElementsByName("colorBoundColor");
        for (var i = 0; i < colorsFields.length; i++) {
            colorsFields[i].style.backgroundColor = textFields[i].innerHTML;
        }
    </script>

</asp:Content>

