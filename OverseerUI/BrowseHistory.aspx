<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseHistory.aspx.cs" Inherits="ScreenWatchUI.BrowseHistory" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PC Watcher</title>
    <style type="text/css">
        #form1
        {
            height: 873px;
        }
        .rg_hi
        {
            border: 0;
            display: block;
            margin: 0 auto 4px;
        }
        .rg_hi
        {
            border: 2px ridge #0000FF;
            display: block;
            margin: 0 auto 4px 0px;
            height: 175px;
            width: 230px;
        }
        .rg_i
        {
            border: 0;
            -ms-interpolation-mode: bicubic;
            display: block;
        }
        .rg_i
        {
            border: 0;
            -ms-interpolation-mode: bicubic;
            display: block;
        }
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
        .style9
        {
            width: 520px;
        }
    </style>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function Img2_onclick() {

        }

// ]]>
    </script>
</head>
<body bgcolor="000000">
    <form id="form1" runat="server" clientidmode="Inherit" style="border-style: ridge;
         font-family: Arial; font-size: medium; font-weight: normal; font-style: normal; background-color: #6699ff;
         width: 995px; margin-right: 79px;" enableviewstate="True">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <span class="style8">ScreenWatch - Browser History<br />
    </span>
    <table cellpadding="1">
       <tr align="left">
            <th style="margin-left: 40px" class="style9">
                <asp:Label ID="Label2" 
                runat="server" 
                Text="From Date:" 
                Font-Size="Small" 
                Font-Underline="False"/>&nbsp;&nbsp;&nbsp;

                <asp:TextBox ID="TextBox2" runat="server" Width="150px" BorderStyle="Inset" 
                    OnTextChanged="TextBox2_TextChanged" Height="25px"/>&nbsp;&nbsp;<br />
                &nbsp;&nbsp;&nbsp;
                <ajaxtoolkit:calendarextender runat="server" ID="Calendarextender1"
                    PopupButtonID="btnDate2"
                    CssClass="AjaxCalendar"
                    TargetControlID="TextBox2" 
                    Format="MM/dd/yyyy" />
                <br />
                <asp:Label ID="Label3" runat="server" Text="To Date:" Font-Size="Small" Font-Underline="False"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:TextBox ID="TextBox3" runat="server" Width="150px" Height="25px" BorderStyle="Inset" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>&nbsp;&nbsp;<br />
                &nbsp;&nbsp;&nbsp;
                <ajaxtoolkit:calendarextender runat="server" ID="calExtender2"
                    PopupButtonID="btnDate3"
                    CssClass="AjaxCalendar"
                    TargetControlID="TextBox3" 
                    Format="MM/dd/yyyy" />
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Load Picture" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" OnClick="ClearDates_Click" Text="Clear Dates" />
                <br />
            </th>
        </tr>
    </table>
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
