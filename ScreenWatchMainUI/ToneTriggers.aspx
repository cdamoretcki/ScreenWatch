<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToneTriggers.aspx.cs" Inherits="ScreenWatchUI.ToneTriggers" %>

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
                ScreenWatcher - Dashboard - Tone Triggers
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="LinkButton3" runat="server">Home</asp:LinkButton> &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton4" runat="server">Text Trigger</asp:LinkButton>    
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
                <asp:TextBox ID="colorUserTB" runat="server" />

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
                ControlToValidate="colorUserTB"/>
                
                 <asp:RegularExpressionValidator ID="RXcolorUserTB" runat="server"     
                ErrorMessage="This expression does not validate." 
                ControlToValidate="colorUserTB"     
                ValidationExpression="^[a-zA-Z'.\s]{1,40}$" />
            </td>            
            <td>
                <asp:TextBox ID="colorLBTB" runat="server" />

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
                ControlToValidate="colorLBTB"/>
                
                  <asp:RegularExpressionValidator ID="RXcolorLBTB" runat="server"     
                ErrorMessage="This expression does not validate." 
                ControlToValidate="colorLBTB"     
                ValidationExpression="^[a-zA-Z0-9'.\s]{6}$" />
            </td>
            <ajaxtoolkit:ColorPickerExtender ID="ColorPickerExtender3" runat="server" TargetControlID="colorLBTB" />
            
            <td>
                <asp:TextBox ID="colorUBTB" runat="server" />

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
                ControlToValidate="colorUBTB"/>
                
                 <asp:RegularExpressionValidator ID="RXcolorUBTB" runat="server"     
                ErrorMessage="This expression does not validate." 
                ControlToValidate="colorUBTB"     
               ValidationExpression="^[a-zA-Z0-9'.\s]{6}$" />
            </td>
            <ajaxtoolkit:ColorPickerExtender ID="ColorPickerExtender4" runat="server" TargetControlID="colorUBTB" />
            <td>
                <asp:TextBox ID="colorSensitivity" runat="server" />
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
                ControlToValidate="colorSensitivity"/>
              
               <asp:RegularExpressionValidator ID="RXcolorSensitivity" runat="server"     
                ErrorMessage="This expression does not validate." 
                ControlToValidate="colorSensitivity"     
                ValidationExpression="^[0-9'.\s]{1,3}$" />
            </td>
        </tr>
    </table>                           
    </form>
</body>
</html>