<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextTriggers.aspx.cs" Inherits="ScreenWatchUI.TextTriggers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Text Triggers</title>
    <style type="text/css">
        .style7
        {
            font-family: Calibri;
            font-size: x-large;
            font-weight: bold;
        }
    </style>
</head>
<body style="height: 697px">
    <form id="form1" runat="server" clientidmode="Inherit" style="border-style: ridge;
    font-family: Arial; font-size: medium; font-weight: normal; font-style: normal; background-color: #CCCCCC;
    width: 1003px" enableviewstate="True">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table>
        <tr>
            <td class="style7">
                ScreenWatcher - Dashboard - Text Triggers
            </td>
        </tr>
    </table>
    <br />
    <span>
        <asp:Button ID="Button3" runat="server" Text="Add" OnClick="SubmitNewText" />
        User to Monitor:
        <asp:DropDownList ID="UserDropDownList" runat="server" />
        Text to Filter:
        <asp:TextBox ID="TriggerTB" runat="server" CausesValidation="true" />
    </span><span>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" BackColor="DarkGray"
            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Font-Bold="true"
            Font-Names="Verdana" ErrorMessage="Please enter a regular expression for a word to scan for"
            ControlToValidate="TriggerTB" />
    </span>
    <asp:Repeater ID="textRepeater" runat="server">
        <HeaderTemplate>
            <table border="1" width="100%">
                <tr>
                    <th>
                        Text
                    </th>
                    <th>
                        User
                    </th>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table border="1" width="100%">
                <tr>
                    <asp:HiddenField ID="textID" runat="server" Value="<%# ((ScreenWatchData.TextTrigger)Container.DataItem).id %>" />
                    <td>
                        <asp:Label ID="triggerString" runat="server" Text="<%# ((ScreenWatchData.TextTrigger)Container.DataItem).triggerString %>" />
                    </td>
                    <td>
                        <asp:Label ID="textUser" runat="server" Text="<%# ((ScreenWatchData.TextTrigger)Container.DataItem).userName%>" />
                    </td>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="UpdateText" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="DeleteText" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    </form>
</body>
</html>
