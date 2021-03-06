﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ScreenWatchUI.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ScreenWatch - The Woodstock to your Snoopy</title>
    <link type="text/css" href="Scripts/jquery/css/start/jquery-ui-1.8.18.custom.css" rel="Stylesheet"/>
    <script type="text/javascript" src="Scripts/jquery/js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery/js/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript">
        $(function () { $("#tabs").tabs(); });
        $("#tabs").tabs({ fx: { opacity: "toggle"} });
    </script>
</head>
<body>
    <div class="content">
        <div id="tabs" style="width: 100%; height: 100%">
            <ul>
                <li><a href="#tab_BrowserHistory">Browser History</a></li>
                <li><a href="#tab_TextTriggers">Text Triggers</a></li>
                <li><a href="#tab_ToneTriggers">Tone Triggers</a></li>
                <li><a href="#tab_Administration">Administration</a></li>
            </ul>
            <div id="tab_BrowserHistory">
                <iframe id="BrowseHistory" src="BrowseHistory.aspx" style="width: 100%; height: 550px"></iframe>
            </div>
            <div id="tab_TextTriggers">
                <iframe id="TextTriggers" src="TextTrigger/" style="width: 100%; height: 550px"></iframe>
            </div>
            <div id="tab_ToneTriggers">
                <iframe id="ToneTriggers" src="ToneTrigger/" style="width: 100%; height: 500px"></iframe>
            </div>
            <div id="tab_Administration">
                <iframe id="Administration" src="User/" style="width: 100%; height: 550px"></iframe>
            </div>
        </div>
    </div>
</body>
</html>