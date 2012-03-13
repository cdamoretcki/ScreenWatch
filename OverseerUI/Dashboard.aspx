<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ScreenWatchUI.Dashboard" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PCWatcher Dashboard</title>
    <style type="text/css">
.rg_i{border:0;-ms-interpolation-mode:bicubic;display:block}.rg_i{border:0;-ms-interpolation-mode:bicubic;display:block}.rg_hi{border:0;display:block;margin:0 auto 4px}.rg_hi{border:0;display:block;margin:0 auto 4px}
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
    <form id="form1" runat="server" clientidmode="Inherit"     
        style="border-style: ridge; 
        font-family: Arial; 
        font-size: medium; 
        font-weight: normal; 
        font-style: normal; 
        background-color: #CCCCCC;  
        width: 1003px; 
        height: 1147px;" 
        enableviewstate="True">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
   
    <table>
      <tr>
        <td class="style7">ScreenWatcher - Dashboard</td>
      </tr>
    </table>
    <br /><br /><br />

    <table>
      <tr>
        <td class="style1">Text Triggers</td>
      </tr>
    </table>
    <asp:Table ID="DocumentsToDownloadTable" runat="server" Width="50%" 
        GridLines="Both" Height="16px" >
          <asp:TableRow VerticalAlign="top"> 
                <asp:TableCell 
                    Height="15"                      
                    Width="15%" 
                    HorizontalAlign="Center" Font-Bold="true">ID</asp:TableCell>    
                         
                <asp:TableCell 
                     Height="15" 
                    Width="15%" 
                    HorizontalAlign="Center" Font-Bold="true">Text Type</asp:TableCell> 
                    
                    <asp:TableCell 
                     Height="25" 
                    Width="15%" 
                    HorizontalAlign="Center" Font-Bold="true">Text Change</asp:TableCell>              
          </asp:TableRow>
    </asp:Table>  <br />
    <asp:Button ID="Button3" runat="server" Text="Load Text" 
        onclick="Button3_Click"/>
    <br /><br />

    <table>
      <tr>
        <td class="style1">Color Triggers</td>
      </tr>
    </table>
    <asp:Table ID="DocumentsToDownloadTable1" runat="server" Width="50%" 
        GridLines="Both" Height="16px" >
          <asp:TableRow VerticalAlign="top"> 
                <asp:TableCell 
                    Height="15"                      
                    Width="15%" 
                    HorizontalAlign="Center" Font-Bold="true">ID</asp:TableCell>    
                         
                <asp:TableCell 
                     Height="15" 
                    Width="15%" 
                    HorizontalAlign="Center" Font-Bold="true">Color Code</asp:TableCell> 
                    
                    <asp:TableCell 
                     Height="25" 
                    Width="15%" 
                    HorizontalAlign="Center" Font-Bold="true">Color Change</asp:TableCell>              
          </asp:TableRow>
    </asp:Table>  

    <br /><br />
 
    <asp:Button ID="Button4" runat="server" Text="Load Color" 
        onclick="Button4_Click"/>
    <br />

    <br />
 

     <table id="tblSSFreq" border="1" cellpadding="3">
        <tr>
            <td class="style4">&nbsp;
                <asp:RadioButton 
                ID="RadioButton0" 
                runat="server" 
                Text="5 Screenshot per Minute" 
                CssClass="style5"/>
                
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton 
                ID="RadioButton1" 
                runat="server" 
                Text="15 Screenshot per minute" 
                CssClass="style5"/>
                
                &nbsp;&nbsp;
        </td>
     </tr>

      <tr><br /><br />
            <td class="style4"> 
                <asp:RadioButton 
                ID="RadioButton2" 
                runat="server" 
                Text="20 Screenshot per minute" 
                CssClass="style5" />
                
                &nbsp;&nbsp;&nbsp;
                <asp:RadioButton 
                ID="RadioButton3" 
                runat="server" 
                Text="25 Screenshot per minute" 
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
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button1" runat="server" Text="Save Changes" />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button2" runat="server" Text="Undo Changes" />
            </th>
        </tr>
     </table>
     <br />
     <br />

     <table border=1>
     <tr align="left">
       <th><span class="style3">Real-time Images</span><br /></th>          
     </tr>
     </table>
     <table border="1" cellpadding="3">
        <tr>
         <th>
             <asp:RadioButton ID="RadioButton4" runat="server" Text="Show recent Image" 
                 style="font-size: small" />
         </th>
         <th>
             <asp:RadioButton ID="RadioButton5" runat="server" Text="Keep Refreshing" 
                 style="font-size: small" />
         </th>
        </tr>
     </table>
     <table border=1>
     <tr><th>&nbsp;<asp:Image ID="Image1" runat="server" style="margin-left: 0px" 
             Width="759px" Height="218px" /></th></tr>
  
     </table>
     
     </form>

    </body>
</html>
