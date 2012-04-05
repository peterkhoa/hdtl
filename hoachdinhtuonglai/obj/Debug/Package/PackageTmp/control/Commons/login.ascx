﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="hoachdinhtuonglai.control.Commons.login" %>
<div style="margin:0 auto 0 auto;">
    
    <div style="width:300px;border:1px solid #3d3d3d;margin:0 auto 0 auto;">
        <div style="background:#6d6d6b;height:30px;color:#fff;font-size:14px;font-weight:bold;vertical-align:-5px;padding:5px;">Login</div>
        <div style="color:Red;"><%=mess %></div>
        <div style="margin:5px 0 5px 5px;"><label for="username">Username or Email:</label></div>
        <div>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></div>
            <div style="margin:5px 0 5px 5px;"><label for="password">Password:</label></div>
        <div>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></div>
            <div class="button">
                <asp:Button ID="btnLogin" runat="server" Text="Login" Width="66px" CssClass="button highlight" OnClientClick="setCookie('is_show','1',0)" 
                    onclick="btnLogin_Click" /></div>
             <div>
                 <asp:CheckBox ID="CheckBoxNho" runat="server" Text="Tự đăng nhập lần sau" /></div>       
            <div>Quên mật khẩu?</div>
            <div><a href="/dang-ky/"</div>
            <div>Đăng nhập với tài khoản Google</div>
            </div>
   
</div>

