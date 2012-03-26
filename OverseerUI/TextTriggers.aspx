<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextTriggers.aspx.cs" Inherits="ScreenWatchUI.TextTriggers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                ScreenWatcher - Dashboard - Text Triggers
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="LinkButton3" runat="server">Home</asp:LinkButton> &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton4" runat="server">Tone Trigger</asp:LinkButton>    
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
                <asp:TextBox ID="TriggerTB" runat="server" CausesValidation="true" />
                
              <asp:RequiredFieldValidator 
                id="RequiredFieldValidator1" runat="server" 
                BackColor="DarkGray"
                BorderColor="Black"
                BorderStyle="Solid"
                BorderWidth="1px"
                ForeColor="White"
                Font-Bold="true"
                Font-Names="Verdana"
                 ErrorMessage="Required!"
                ControlToValidate="TriggerTB"/>
              
              <asp:RegularExpressionValidator ID="RXTriggerTB" runat="server"     
                ErrorMessage="This expression does not validate." 
                ControlToValidate="TriggerTB"     
                ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />

            </td>
            <td>
                <asp:TextBox ID="UserNameTB" runat="server" CausesValidation="false" />
                
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
                ControlToValidate="UserNameTB"/>
              
                 <asp:RegularExpressionValidator ID="RXUserNameTB" runat="server"     
                ErrorMessage="This expression does not validate." 
                ControlToValidate="UserNameTB"     
                ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
            </td>            
        </tr>
    </table> 
    </form>   
</body>
</html>
