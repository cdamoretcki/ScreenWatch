﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Deleted
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">

    <h2>User Deleted</h2>

    <div>
        <p>The user was successfully deleted.</p>
    </div>

    <div>
        <p><%: Html.ActionLink("Back to ScreenWatch Users", "Index") %></p>
    </div>

</asp:Content>
