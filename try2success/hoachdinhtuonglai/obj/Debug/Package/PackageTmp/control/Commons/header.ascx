<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="hoachdinhtuonglai.control.Commons.header" %>
<%--<%@ Register src="login.ascx" tagname="login" tagprefix="uc1" %>--%>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<div style="padding: 2px 0; width: 100%;" class="mainheader">
    <div>
        <!--div class="logo">
        <a href="/?page=home">
            <img src="https://lh6.googleusercontent.com/-rl8cwc2uXI8/T1HbLgam87I/AAAAAAAAAA0/eiyY1IH8glM/s500-c-k/logo1_small2.png"
                alt="" height="120px" /></a></div>
    <div class="bannertop">
        <img src="/images/main/bannertop.jpg" alt="" height="120px" /></div-->
        <a href="/"><img  src="/images/main/bannertop.png" alt="" /></a>
    </div>
    
    <div style="width: 100%;float:left;">
        <ul class="glossymenu" style="float:left;">
            <li id="home"><a href="/"><b>Home</b></a></li>
            <li id="chiase"><a id="chia_se" class="mn" href="/chia-se/" onclick="return addClassNew();">
                <b>Chia sẻ</b></a></li>
            <li id="hoinhom"><a id="hoi_nhom" class="mn" href="/hoi-nhom/" onclick="return addClassNew();">
                <b>Hội-Nhóm</b></a></li>
            <li id="tailieu"><a id="tai_lieu" class="mn" href="/tai-lieu/" onclick="return addClassNew();">
                <b>Kho tài liệu</b></a></li>
            <li id="vieclam"><a id="viec_lam" class="mn" href="/viec-lam/" onclick="return addClassNew();">
                <b>Việc làm</b></a></li>
            <li id="hinhanh"><a id="hinh_anh" class="mn" href="/hinh-anh/" onclick="return addClassNew();">
                <b>Gallery</b></a></li>
        </ul>
        <%
            if (account != null)
            {
        %>
        <div class="common">
            <div style="float: right;">
                &nbsp;&nbsp;&nbsp; <a href="/?page=dang-nhap&signout=true" title="Sign Out">Thoát</a></div>
            <%--phan notify--%>
            <div id="ifo" style="float: right; padding: 0 5px 9px; min-width: 50px; max-width: 150px;">
                <ul>
                    <%


                if (!(Request.Browser.Browser == "IE" && (Request.Browser.MajorVersion <= 6)))
                {
                    %>
                    <li class="" id="liactive" style="position: relative">
                        <div class="inconmsg">
                        </div>
                        <a onclick="$('#notification_box').css('display','block');" href="#" id="notiBtn"
                            class="r5" style="position: relative; color: #fff"><strong class="" style="color: Green;">
                                Tin nhắn&nbsp;<span id="nummsg"><%= countActionLog > 0? "("+countActionLog.ToString() + ")":""%></span></strong>
                        </a>
                        <%--<div style="height:1px; background:#fff; width:100%; padding:0 5px; z-index:999; margin-left:-5px; position:absolute; bottom:-10px; left:-5px"></div>--%>
                        <ul style="position: relative">
                            <li>
                                <div id="notification_box" style="display: none; max-height: 350px; overflow: auto;
                                    margin-left: -6px; margin-top: 8px; z-index: 333">
                                    <div id="loading_bar" style="text-align: center; padding: 5px; margin: 2px;">
                                        <img src="http://dl.dropbox.com/u/19053289/DV/loadingAnimation.gif" alt="" />
                                    </div>
                                    <div id="notification_content">
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <% } %>
                    <%--<li class="taikhoan" id="thongtinataikhoan" ><img src="/images/Header%20new/taikhoangheader.png" /><a style="color:#000" href="<%= DeltaViet.LinkBuilder.Profile(Page.User.Identity.Name)%>">
                <%= Page.User.Identity.Name%></a><img style=" margin-left:5px; margin-right:0px;" src="/images/Header%20new/btnxuong.png" /></li>--%>
                </ul>
            </div>
            <%--ket thuc phan notify--%>
            <div style="float: right;">
                Chào <span style="font-size: 13px; font-weight: bold;"><a href="<%=LinkBuilder.getLinkProfile(account)%>">
                    <%=account.LastName %></a></span></div>
            
        </div>
        <%
            }
            else
            {
        %>
        <div class="common">
            <a href="/dang-nhap/<%=current_url %>" class="button highlight very large" id="login">
                Đăng nhập</a> <a href="/dang-ky/" class="button showlight very large">Đăng ký</a>
        </div>
        <%} %>
        
    </div>
    
    <div id="loginshow" class="btn" style="display: <%=display%>;">
        <div>
            <%--<uc1:login ID="login1" runat="server" /></div>--%>
            <div class="close">
                <img src="http://dl.dropbox.com/u/19053289/DV/close.png" width="16px" alt="close" /></div>
        </div>
        <input type="hidden" id="pagehide" value="<%=page %>" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var id = $('#pagehide').val();

            id = id.replace("-", "");
            if (id == '')
                id = 'home';
            $('#' + id).addClass("current");
        });

        $("#notiBtn").click(function () {


            $("#loading_bar").css('display', 'block');
            $("#notification_content").css('display', 'none');

            $.ajax({
                type: "POST",
                url: "/control/notification.aspx",
                data: "",
                cache: false,
                success: function (html) {
                    //  document.getElementById("loadplace"+Id).innerHTML = html;
                    $("#loading_bar").css('display', 'none');
                    $("#notification_content").css('display', 'block');
                    $("#notification_content").html(html);
                    $("#nummsg").text("");
                }
            });
            return false;

        });

        var i = 1;
        var j = 1;
        $("body").click(function () {
            //alert("body" + i);
            if (j == 2 && $("#notification_box").css('display') != "none") {
                $("#ifo").removeClass("liactive");
                $("#ifo a").css("color", "#fff");
                $("#notification_box").css('display', 'none');
                //i = 1;
            } else {
                j = 2;
            }
        });
        $("#notification_box").click(function () {
            j = 1;
        });
        $("#notiBtn").click(function () {
            //if (i != 2) {
            $("#ifo").addClass("liactive");
            $("#ifo a").css("color", "#1e598e");
            j = 2;
            //}
        });
    </script>
    
    </div>
    