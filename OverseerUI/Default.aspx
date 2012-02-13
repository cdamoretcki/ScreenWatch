<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ParentsEyes._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        End-to-End Demo</h2>
    <p>
        Enter an image ID&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </p>
    <p>
        Click to load the image&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Height="20px" onclick="Button1_Click" 
            Text="Load" Width="57px" />
    </p>
    <p>
        <asp:Image ID="DisplayImage" runat="server" style="width: 600px" />
    </p>
</asp:Content>
