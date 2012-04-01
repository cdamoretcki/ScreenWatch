<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToneTriggers.aspx.cs" Inherits="ScreenWatchUI.ToneTriggers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tone Triggers</title>
</head>
<body>
    <form id="form1" runat="server" clientidmode="Inherit" style="border-style: ridge;
    font-family: Arial; font-size: medium; font-weight: normal; font-style: normal; background-color: #CCCCCC;
    width: 1003px" enableviewstate="True">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="style7">
        ScreenWatcher - Dashboard - Tone Triggers
    </div>
    <br />
    <span>
        <asp:Button ID="Button4" runat="server" Text="Add" OnClick="SubmitNewTone" />
        User to Monitor:
        <asp:DropDownList ID="UserDropDownList" runat="server" />
        Color:
        <asp:TextBox ID="colorLBTB" runat="server" Width="60px" />
        <ajaxtoolkit:ColorPickerExtender ID="ColorPickerExtender3" runat="server" TargetControlID="colorLBTB" />
        Color:
        <asp:TextBox ID="colorUBTB" runat="server" Width="60px" />
        <ajaxtoolkit:ColorPickerExtender ID="ColorPickerExtender4" runat="server" TargetControlID="colorUBTB" />
        Percentage of screen:
        <asp:TextBox ID="colorSensitivity" runat="server" Width="50px" />
    </span><span>
        <asp:RequiredFieldValidator ID="RFV4" runat="server" BackColor="DarkGray" BorderColor="Black"
            BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Font-Bold="true" Font-Names="Verdana"
            ErrorMessage="Pick a color" ControlToValidate="colorLBTB" />
        <asp:RegularExpressionValidator ID="RXcolorLBTB" runat="server" ErrorMessage="Enter color"
            ControlToValidate="colorLBTB" ValidationExpression="^[a-fA-F0-9'.\s]{1,6}$" />
        <asp:RequiredFieldValidator ID="RFV5" runat="server" BackColor="DarkGray" BorderColor="Black"
            BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Font-Bold="true" Font-Names="Verdana"
            ErrorMessage="Pick a color" ControlToValidate="colorUBTB" />
        <asp:RegularExpressionValidator ID="RXcolorUBTB" runat="server" ErrorMessage="Enter color"
            ControlToValidate="colorUBTB" ValidationExpression="^[a-fA-F0-9'.\s]{1,6}$" />
        <asp:RequiredFieldValidator ID="RFV6" runat="server" BackColor="DarkGray" BorderColor="Black"
            BorderStyle="Solid" BorderWidth="1px" ForeColor="White" Font-Bold="true" Font-Names="Verdana"
            ErrorMessage="Enter percentage of screen sensitivity" ControlToValidate="colorSensitivity" />
        <asp:RegularExpressionValidator ID="RXcolorSensitivity" runat="server" ErrorMessage="Enter percentage of screen sensitivity"
            ControlToValidate="colorSensitivity" ValidationExpression="^1?[0-9]{1,2}$" /></span>
    <asp:Repeater ID="colorRepeater" runat="server">
        <HeaderTemplate>
            <table border="1" width="100%">
                <tr>
                    <th>
                        User Name
                    </th>
                    <th>
                        Lower Color
                    </th>
                    <th>
                        Upper Color
                    </th>
                    <th>
                        Sensitivity
                    </th>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table border="1" width="100%" id="tableTextTrigger">
                <tr>
                    <asp:HiddenField ID="colorID" runat="server" Value="<%# ((ScreenWatchData.ToneTrigger)Container.DataItem).id %>" />
                    <td>
                        <asp:Label ID="colorUser" runat="server" Text="<%# ((ScreenWatchData.ToneTrigger)Container.DataItem).userName %>" />
                    </td>
                    <td>
                        <asp:Label ID="colorLB" runat="server" Text="<%# ((ScreenWatchData.ToneTrigger)Container.DataItem).lowerColorBound%>" />
                    </td>
                    <td>
                        <asp:Label ID="colorUB" runat="server" Text="<%# ((ScreenWatchData.ToneTrigger)Container.DataItem).upperColorBound%>" />
                    </td>
                    <td>
                        <asp:Label ID="colorSensitivity" runat="server" Text="<%# ((ScreenWatchData.ToneTrigger)Container.DataItem).sensitivity%>" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    </form>
</body>
</html>
