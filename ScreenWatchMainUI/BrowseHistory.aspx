<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseHistory.aspx.cs" Inherits="ScreenWatchUI.BrowseHistory" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PC Watcher</title>
    <style type="text/css">
        .style1
        {
            width: 11px;
        }
        .style7
        {
            text-decoration: underline;
            font-weight: normal;
            font-family: Calibri;
            font-size: small;
        }
        .style8
        {
            font-family: Calibri;
            font-weight: bold;
            text-decoration: underline;
            font-size: x-large;
            letter-spacing: 1pt;
        }
    </style>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function Img2_onclick() {

        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server" clientidmode="Inherit" style="border-style: ridge;
    font-family: Arial; font-size: medium; font-weight: normal; font-style: normal; background-color: #CCCCCC;
    width: 1003px" enableviewstate="True">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <span class="style8">ScreenWatch - Browser History<br />
    </span>
    <span>
                <asp:Label ID="Label2" 
                runat="server" 
                Text="From Date:" 
                Font-Size="Small" 
                Font-Underline="False"/>&nbsp;&nbsp;&nbsp;

                <asp:TextBox ID="DateTextBox" runat="server" Width="150px" BorderStyle="Inset" 
                    OnTextChanged="SelectDate" Height="25px"/>&nbsp;&nbsp;<br />
                &nbsp;&nbsp;&nbsp;
                <ajaxtoolkit:calendarextender runat="server" ID="Calendarextender1"
                    PopupButtonID="btnDate2"
                    TargetControlID="DateTextBox" 
                    Format="MM/dd/yyyy" />
          </span>
    <table>
        <tr>
           
            <th>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </th>
            <th class="style1">
                <asp:Image ID="DisplayImage" runat="server" Height="284px" Width="555px" />                   
                      
            </th>
           
        </tr>
    </table>
    <h3 class="style7">
        Thumbnail Images</h3>
    <table border="3" style="height: 9px" width="100%" cellpadding="4" cellspacing="4">
        <tr align='left'>
            <td id="ThumbNailHeader" runat="server" align="center">              
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
