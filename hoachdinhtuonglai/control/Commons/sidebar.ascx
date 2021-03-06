﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sidebar.ascx.cs" Inherits="hoachdinhtuonglai.control.Commons.sidebar" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<div>
    <div class="gray-box2">
        <div class="gray-box-top">
            <div class="l">
                <div class="r">
                    <strong>Thành viên mới</strong>
                </div>
            </div>
            <div class="gray-box-content">
                <div class="inner" style="min-height: 130px;">
                    <ul class="newuser">
                        <% foreach (Account a in listNewAccount)
                           { %>
                        <li style="font-size: 12px; list-style-type: none; list-style-image: none; height: 30px;
                            border-bottom: 1px solid #D5D8DF;">
                            <div style="float: left; width: 60px;">
                                <img src="<%=a.getAvatar() %>" height="30px" /></div>
                            <div style="float: none; width: 200px;">
                                <a style="color: #1b1b1b;" href="<%=LinkBuilder.getLinkProfile(a) %>" title="<%=a.FullName %>">
                                    <%=a.FullName %></a></div>
                        </li>
                        <%} %>
                    </ul>
                </div>
            </div>
            <div class="gray-box-bottom">
                <div class="l">
                    <div class="r">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="outerbox" id="position1">
        <div class="position">
            
            <div id="fb-root">
            </div>
            <script type="text/javascript">                (function (d, s, id) {
                    var js, fjs = d.getElementsByTagName(s)[0];
                    if (d.getElementById(id)) return;
                    js = d.createElement(s); js.id = id;
                    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
                    fjs.parentNode.insertBefore(js, fjs);
                } (document, 'script', 'facebook-jssdk'));</script>
            <div class="fb-like" data-send="true" data-layout="button_count" data-width="450"
                data-show-faces="true">
            </div>
        </div>
    </div>
</div>
