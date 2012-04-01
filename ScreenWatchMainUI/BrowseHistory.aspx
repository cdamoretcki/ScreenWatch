<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseHistory.aspx.cs"
    Inherits="ScreenWatchUI.BrowseHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse Schreen Shot History</title>
    <link rel="stylesheet" type="text/css" href="Content/jquery.ad-gallery.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.ad-gallery.js"></script>
    <script type="text/javascript">
        $(function () {
            $('img.image1').data('ad-desc', 'Whoa! This description is set through elm.data("ad-desc") instead of using the longdesc attribute.<br>And it contains <strong>H</strong>ow <strong>T</strong>o <strong>M</strong>eet <strong>L</strong>adies... <em>What?</em> That aint what HTML stands for? Man...');
            $('img.image1').data('ad-title', 'Title through $.data');
            $('img.image4').data('ad-desc', 'This image is wider than the wrapper, so it has been scaled down');
            $('img.image5').data('ad-desc', 'This image is higher than the wrapper, so it has been scaled down');
            var galleries = $('.ad-gallery').adGallery();
            $('#switch-effect').change(
      function () {
          galleries[0].settings.effect = $(this).val();
          return false;
      }
    );
            $('#toggle-slideshow').click(
      function () {
          galleries[0].slideshow.toggle();
          return false;
      }
    );
            $('#toggle-description').click(
      function () {
          if (!galleries[0].settings.description_wrapper) {
              galleries[0].settings.description_wrapper = $('#descriptions');
          } else {
              galleries[0].settings.description_wrapper = false;
          }
          return false;
      }
    );
        });
  </script>
    <style type="text/css">
        *
        {
            font-family: "Lucida Grande" , "Lucida Sans Unicode" , "Lucida Sans" , Verdana, Arial, sans-serif;
            color: #333;
            line-height: 140%;
        }
        select, input, textarea
        {
            font-size: 1em;
        }
        body
        {
            font-size: 70%;
            width: 900px;
        }
        h2
        {
            margin-top: 1.2em;
            margin-bottom: 0;
            padding: 0;
            border-bottom: 1px dotted #dedede;
        }
        h3
        {
            margin-top: 1.2em;
            margin-bottom: 0;
            padding: 0;
        }
        ul
        {
            list-style-image: url(list-style.gif);
        }
        pre
        {
            font-family: "Lucida Console" , "Courier New" , Verdana;
            border: 1px solid #CCC;
            background: #f2f2f2;
            padding: 10px;
        }
        code
        {
            font-family: "Lucida Console" , "Courier New" , Verdana;
            margin: 0;
            padding: 0;
        }
        #descriptions
        {
            position: relative;
            height: 50px;
            background: #EEE;
            margin-top: 10px;
            width: 900px;
            padding: 10px;
            overflow: hidden;
        }
        #descriptions .ad-image-description
        {
            position: absolute;
        }
        #descriptions .ad-image-description .ad-description-title
        {
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" clientidmode="Inherit" style="border-style: ridge;
    font-family: Arial; font-size: medium; font-weight: normal; font-style: normal; background-color: #CCCCCC;
    width: 1003px" enableviewstate="True">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="container">
        <div id="gallery" class="ad-gallery">
            Date:
            <asp:TextBox ID="DateTextBox" runat="server" OnTextChanged="SelectDate" Width="110" />
            <ajaxtoolkit:CalendarExtender runat="server" ID="Calendarextender1" PopupButtonID="btnDate2"
                TargetControlID="DateTextBox" Format="MM/dd/yyyy" />
            <asp:LinkButton ID="SearchButton" runat="server" OnClick="SelectDate" Text="SEARCH" />
            <asp:RequiredFieldValidator ID="RequireDate" runat="server" ErrorMessage="Please enter a date"
                ControlToValidate="DateTextBox" />
            <div class="ad-image-wrapper">
            </div>
            <div class="ad-controls">
            </div>
            <div class="ad-nav" >
                <div class="ad-thumbs">
                    <ul class="ad-thumb-list">
                        <asp:Repeater ID="ThumbNailRepeater" runat="server">
                            <ItemTemplate>
                                <li><a id="ImageLink" href='<%# ((ScreenWatchData.ScreenShot)Container.DataItem).filePath%>'
                                    runat="server">
                                    <img id="ThumbImage" alt="" src='<%# ((ScreenWatchData.ScreenShot)Container.DataItem).thumbnailFilePath%>'
                                        runat="server" />
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
