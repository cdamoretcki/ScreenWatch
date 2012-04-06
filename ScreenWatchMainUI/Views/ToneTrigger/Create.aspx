<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ScreenWatchUI.Models.ToneTrigger>" %>

<asp:Content ID="TitleContent" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <link type="text/css" rel="Stylesheet" href="../Scripts/jquery/css/start/jquery-ui-1.8.18.custom.css"/>
    <link type="text/css" rel="stylesheet" href="../Scripts/miniColors/jquery.miniColors.css" />
    <script type="text/javascript" src="../Scripts/jquery/js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery/js/jquery-ui-1.8.18.custom.min.js"></script>
	<script type="text/javascript" src="../Scripts/miniColors/jquery.miniColors.js"></script>		 
    <script type="text/javascript">

        $(document).ready(function () {
            $(".color-picker").miniColors({ letterCase: 'uppercase' });
        });

        $(function () {
            $("#slider").slider({
                range: "min",
                value: 50,
                min: 1,
                max: 100,
                slide: function (event, ui) {
                    $("#sensitivity").val(ui.value);
                }
            });
            $("#sensitivity").val($("#slider").slider("value"));
        });
    </script>
            
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
            <%: Html.TextBoxFor(model => model.userName )%>

                <%: Html.ValidationMessageFor(model => model.userName) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.lowerColorBound) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.lowerColorBound, new { @class="color-picker", @id="lowerColorBound", @size="6"})%>
                <%: Html.ValidationMessageFor(model => model.lowerColorBound)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.upperColorBound) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.upperColorBound, new { @class="color-picker", @id="upperColorBound", @size="6"})%>
                <%: Html.ValidationMessageFor(model => model.upperColorBound)%>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.sensitivity) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.sensitivity, new { @id = "sensitivity", @style = "width: 30px;" })%>
                <div id="slider" style="width: 200px; display: inline-block"></div>
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

