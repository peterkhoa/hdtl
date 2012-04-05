﻿<%@ OutputCache VaryByParam="none" VaryByCustom="userName" Duration="5" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notification.aspx.cs" Inherits="hoachdinhtuonglai.control.notification" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
   <%-- <% foreach (ActionLog a in listAction)
       {%>
    <div class="notification">
        <%=a.ShortDescription %>
    </div>
    <%} %>--%>
    <asp:Repeater ID="RepeaterNotification" runat="server">
        <ItemTemplate>
            <div class="notification">
        <%# ((hoachdinhtuonglai.Data.Core.ActionLog)Container.DataItem).ShortDescription %>
    </div>
        </ItemTemplate>
    </asp:Repeater>
</body>
</html>
