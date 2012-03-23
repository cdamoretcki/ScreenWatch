<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ScreenWatchUI.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PCWatcher Dashboard</title>
    <style type="text/css">
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
        .rg_hi
        {
            border: 0;
            display: block;
            margin: 0 auto 4px;
        }
        .rg_hi
        {
            border: 0;
            display: block;
            margin: 0 auto 4px;
        }
        .style1
        {
            font-family: Calibri;
            font-size: large;
            text-decoration: underline;
        }
        .style3
        {
            font-family: Calibri;
            text-decoration: underline;
        }
        .style4
        {
            width: 478px;
        }
        .style5
        {
            font-size: small;
        }
        .style7
        {
            font-family: Calibri;
            font-size: x-large;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" clientidmode="Inherit" style="border-style: ridge;
    font-family: Arial; font-size: medium; font-weight: normal; font-style: normal; background-color: #CCCCCC;
    width: 1003px; height: 1147px;" enableviewstate="True">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table>
        <tr>
            <td class="style7">
                ScreenWatcher - Dashboard
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <table>
        <tr>
            <td class="style1">
                Text Triggers
            </td>
        </tr>
    </table>
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
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    <table border="1" width="100%" cellspacing="4">
        <tr>
        <td><asp:Button ID="Button3" runat="server" Text="Submit" OnClick="SubmitNewText" /></td>            
            <td>
                <asp:TextBox ID="txtTriggerTB" runat="server" />
                
                <asp:RequiredFieldValidator 
                id="RFV1" runat="server" 
                BackColor="DarkGray"
                BorderColor="Black"
                BorderStyle="Solid"
                BorderWidth="1px"
                ForeColor="White"
                Font-Bold="true"
                Font-Names="Verdana"
                 ErrorMessage="Text is Required!"
                ControlToValidate="txtTriggerTB">
              </asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:TextBox ID="txtUserNameTB" runat="server" />
                
                <asp:RequiredFieldValidator 
                id="RFV2" runat="server" 
                BackColor="DarkGray"
                BorderColor="Black"
                BorderStyle="Solid"
                BorderWidth="1px"
                ForeColor="White"
                Font-Bold="true"
                Font-Names="Verdana"
                 ErrorMessage="Required!"
                ControlToValidate="txtUserNameTB">
              </asp:RequiredFieldValidator>
            </td>            
        </tr>
    </table>
    <br />
    <br />
    <br />
 
    <table>
        <tr>
            <td class="style1">
                Color Triggers
            </td>
        </tr>
    </table>
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
    <table border="1" width="100%" cellspacing="4">
        <tr align="right">
            <td><asp:Button ID="Button4" runat="server" Text="Submit" OnClick="SubmitNewTone" /></td>
            <td>
                <asp:TextBox ID="txtcolorUserTB" runat="server" />

                <asp:RequiredFieldValidator 
                id="RFV3" runat="server" 
                BackColor="DarkGray"
                BorderColor="Black"
                BorderStyle="Solid"
                BorderWidth="1px"
                ForeColor="White"
                Font-Bold="true"
                Font-Names="Verdana"
                 ErrorMessage="Required!"
                ControlToValidate="txtcolorUserTB">
                </asp:RequiredFieldValidator>
            </td>            
            <td>
                <asp:TextBox ID="txtcolorLBTB" runat="server" />

                 <asp:RequiredFieldValidator 
                id="RFV4" runat="server" 
                BackColor="DarkGray"
                BorderColor="Black"
                BorderStyle="Solid"
                BorderWidth="1px"
                ForeColor="White"
                Font-Bold="true"
                Font-Names="Verdana"
                 ErrorMessage="Required!"
                ControlToValidate="txtcolorLBTB">
                </asp:RequiredFieldValidator>
            </td>
            <ajaxtoolkit:ColorPickerExtender ID="ColorPickerExtender3" runat="server" TargetControlID="txtcolorLBTB" />
            
            <td>
                <asp:TextBox ID="txtcolorUBTB" runat="server" />

                 <asp:RequiredFieldValidator 
                id="RFV5" runat="server" 
                BackColor="DarkGray"
                BorderColor="Black"
                BorderStyle="Solid"
                BorderWidth="1px"
                ForeColor="White"
                Font-Bold="true"
                Font-Names="Verdana"
                 ErrorMessage="Required!"
                ControlToValidate="txtcolorUBTB">
                </asp:RequiredFieldValidator>
            </td>
            <ajaxtoolkit:ColorPickerExtender ID="ColorPickerExtender4" runat="server" TargetControlID="txtcolorUBTB" />
            <td>
                <asp:TextBox ID="txtcolorSensitivity" runat="server" />
                  <asp:RequiredFieldValidator 
                id="RFV6" runat="server" 
                BackColor="DarkGray"
                BorderColor="Black"
                BorderStyle="Solid"
                BorderWidth="1px"
                ForeColor="White"
                Font-Bold="true"
                Font-Names="Verdana"
                 ErrorMessage="Required!"
                ControlToValidate="txtcolorSensitivity">
              </asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <br />
    <br />   
    <table id="tblSSFreq" border="1" cellpadding="3">
        <tr>
            <td class="style4">
                &nbsp;
                <asp:RadioButton ID="RadioButton0" runat="server" Text="5 Screenshot per Minute"
                    CssClass="style5" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="RadioButton1" runat="server" Text="15 Screenshot per minute"
                    CssClass="style5" />
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <br />
            <br />
            <td class="style4">
                <asp:RadioButton ID="RadioButton2" runat="server" Text="20 Screenshot per minute"
                    CssClass="style5" />
                &nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="RadioButton3" runat="server" Text="25 Screenshot per minute"
                    CssClass="style5" />
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table>
        <tr>
            <th align="justify">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="Save Changes" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="Undo Changes" />
            </th>
        </tr>
    </table>
    <br />
    <br />
    <table border="1">
        <tr align="left">
            <th>
                <span class="style3">Real-time Images</span><br />
            </th>
        </tr>
    </table>
    <table border="1" cellpadding="3">
        <tr>
            <th>
                <asp:RadioButton ID="RadioButton4" runat="server" Text="Show recent Image" Style="font-size: small" />
            </th>
            <th>
                <asp:RadioButton ID="RadioButton5" runat="server" Text="Keep Refreshing" Style="font-size: small" />
            </th>
        </tr>
    </table>
    <table border="1">
        <tr>
            <th>
                &nbsp;<asp:Image ID="Image1" runat="server" Style="margin-left: 0px" Width="759px"
                    Height="218px" />
            </th>
        </tr>
    </table>
    </form>
</body>
</html>
