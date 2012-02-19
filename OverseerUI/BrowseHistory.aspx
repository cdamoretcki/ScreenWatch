<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseHistory.aspx.cs" Inherits="ScreenWatchUI.BrowseHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PC Watcher</title>
    <style type="text/css">
        #form1
        {
            height: 873px;
        }
    .rg_hi{border:0;display:block;margin:0 auto 4px}.rg_hi{border: 2px ridge #0000FF;
            display:block;margin:0 auto 4px 0px;
            height: 175px;
            width: 230px;
        }
    .rg_i{border:0;-ms-interpolation-mode:bicubic;display:block}.rg_i{border:0;-ms-interpolation-mode:bicubic;display:block}
        .style1
        {
            width: 11px;
        }
        .style2
        {
            width: 207px;
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
<body bgcolor="000000">
    <form id="form1" runat="server" clientidmode="Inherit"     
    style="border-style: ridge; font-family: Arial; font-size: medium; font-weight: normal; font-style: normal; background-color: #6699ff;  width: 995px; margin-right: 79px;" 
    enableviewstate="True">
    <span class="style8">PCWatcher - Browser History<br /></span>
    <table cellpadding="4" > 
        <tr><th align="left"> Image Info: </th></tr>  
        <tr>
            <th align="left">  
                <asp:Label ID="Label1" runat="server" Text="Image ID:" Font-Size="Small" 
                    Font-Underline="False"></asp:Label>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox1" runat="server" Width="221px" BorderStyle="Inset"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Load Picture" />  
                              
            </th>
        </tr>
            <tr> 
                 <th>  
                <asp:Label ID="Label2" runat="server" Text="From Date:" Font-Size="Small" 
                    Font-Underline="False"></asp:Label>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox2" runat="server" Width="157px" BorderStyle="Inset" 
                         ontextchanged="TextBox2_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label3" runat="server" Text="To Date:" Font-Size="Small" 
                    Font-Underline="False"></asp:Label>&nbsp;&nbsp;&nbsp;
                 <asp:TextBox ID="TextBox3" runat="server" Width="150px" BorderStyle="Inset" 
                         ontextchanged="TextBox3_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" onclick="ClearDates_Click" Text="Clear Dates" />                
            </th>
            </tr>
    </table>

    
    <table>
        <tr>
            <th class="style2">
                <asp:Calendar ID="Calendar2" runat="server" DatePickerMode="true"
                         TextBoxId="Textbox2" BackColor="White" 
                    BorderColor="Black" BorderWidth="3px" Font-Names="Verdana" Font-Size="9pt" 
                    ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" 
                    onselectionchanged="Calendar2_SelectionChanged">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
                        VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" 
                        Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                </asp:Calendar> 
               
                <asp:Calendar ID="Calendar3" runat="server" BackColor="White" 
                    BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" 
                    ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" 
                    onselectionchanged="Calendar3_SelectionChanged" Visible="False">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
                        VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" 
                        Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                    <TodayDayStyle BackColor="#CCCCCC" />
                </asp:Calendar> 
               
            </th>
            <th>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
            <th class="style1">
                <asp:Image ID="DisplayImage" runat="server" Height="284px" Width="555px" />
                &nbsp;</th>
                    <img alt="no image to display" src="file:///C:\Documents%20and%20Settings\harbak1\Local%20Settings\Temporary%20Internet%20Files\Content.IE5\V5HQCKIZ\imagesCA64EJ9O.jpg" />
        </tr>
        </table>
        <h3 class="style7">Thumbnail Images</h3>
        <table border=3 style="height: 9px" width=100%">        
           <tr align='left'> 
                <th>
                    <asp:Image ID="ThumbNail1" runat="server" Height="60px" Width="163px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="ThumbNail2" runat="server" Height="60px" Width="163px" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="ThumbNail3" runat="server" Height="60px" Width="163px" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="ThumbNail4" runat="server" Height="60px" Width="163px" />
                </th>              
            </tr> 
                       
        </table>            
    </table>
            
    </form>
    
</body>
</html>
