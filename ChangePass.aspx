<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="hoachdinhtuonglai.control.Commons.ChangePass" %>

<head>
    <style type="text/css">
        .highlight
        {}
    </style>
</head>

<div style="margin:0 auto 0 auto; height: 272px;">
    
    <div style="width:300px;border:1px solid #3d3d3d;margin:0 auto 0 auto;">
        <div style="background:#6d6d6b;height:30px;color:#fff;font-size:14px;font-weight:bold;vertical-align:-5px;padding:5px;">ChangePass</div>
        <div style="color:Red;"><%=mess %></div>
        <div style="margin:5px 0 5px 5px;"><label for="username">Username or Email:</label></div>
        <div>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></div>
            <div style="margin:5px 0 5px 5px;"><label for="password">Password_Old:</label></div>
        <div>
            <asp:TextBox ID="txtPassword_old" runat="server" TextMode="Password"></asp:TextBox></div>
            <div style="margin:5px 0 5px 5px;"><label for="password">Password_New:</label></div>
            <asp:TextBox ID="txtPassword_new" runat="server" TextMode="Password"></asp:TextBox></div>
            <div style="margin:5px 0 5px 5px;"><label for="password">ConfigPassword_New:</label></div>
            <asp:TextBox ID="txtPassword_confignew" runat="server" TextMode="Password"></asp:TextBox>

            <div class="button">
                <asp:Button ID="btnChangePass" runat="server" Text="ChangePass" Width="107px" 
                    CssClass="button highlight" OnClientClick="setCookie('is_show','1',0)" 
                    onclick="btnLogin_Click" />
            </div>
                   
            <div>Về trang chủ</div>
           
            
            </div>

   
</div>

