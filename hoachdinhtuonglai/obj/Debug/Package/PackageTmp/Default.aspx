﻿<%@ OutputCache Duration="5" VaryByCustom="is_static" VaryByParam="*" %>

<%@ Page Title="T2S - Hoạch định tương lai - chia sẻ kinh nghiệm" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="hoachdinhtuonglai.Default" %>

<%@ Register Src="/control/Commons/header.ascx" TagName="header" TagPrefix="uc1" %>
<%--<%@ OutputCache Duration="0" VaryByCustom="is_static"  VaryByParam="*" %>--%>
<%@ Register Src="/control/Commons/LS_Foot.ascx" TagName="footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml">
<head runat="server">
    <title>Hoạch định tương lai - chia sẻ kinh nghiệm</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="google-site-verification" content="FBtM7_zOCVG6KhTBBYQN-zKeV6L9eFssAqLiyGcQG0M" />
    <meta name="keywords" content="kỹ năng, ky nang, kynang, chia se, chiase, thanh cong, try, success, try to success, successful, tuong lai, tintuc, tin tuc, video, clip hay" />
    <meta http-equiv="content-language" content="vi" />
    <asp:Literal ID="canonical" runat="server"></asp:Literal>
    <asp:Literal ID="description" runat="server"></asp:Literal>
    <asp:Literal ID="robots" runat="server"></asp:Literal>
    <link href="/Styles/Site.css" type="text/css" rel="Stylesheet" />
    <link href="/Styles/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js" type="text/javascript"></script>
      
    <script src="/Scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="/Scripts/site.js" type="text/javascript"></script> 
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>   
   <script type="text/javascript">

       var _gaq = _gaq || [];
       _gaq.push(['_setAccount', 'UA-29264508-1']);
       _gaq.push(['_setLocalRemoteServerMode']);
       _gaq.push(['_trackPageview']);

       (function () {
           var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
           ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
           var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
       })();

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="headermain">
            <%--<uc1:header ID="headermain" runat="server" />--%>
            <asp:PlaceHolder ID="headermain" runat="server"></asp:PlaceHolder>
        </div>
        <div class="main-wrap">
            <div class="main">
                <asp:PlaceHolder ID="body" runat="server">
                    <div class="top">
                        <div style="height: 20px; padding: 10px; font-size: 14px;">
                            Bạn không phải là người giỏi về văn chương, nhưng bạn rất thích chia sẻ với mọi
                            người.<a href="/?page=compose">Viết chia sẻ</a></div>
                        <asp:PlaceHolder ID="top" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="main-wrap">
                        <div class="side_left">
                            <asp:PlaceHolder ID="sideleft" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="side_right">
                            <asp:PlaceHolder ID="sideright" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                    <div class="foot">
                        <asp:PlaceHolder ID="foot" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="clear">
                    </div>
                </asp:PlaceHolder>
            </div>
        </div>
        <div class="footer">
            <%--<uc2:footer ID="footermain" runat="server" />--%>
            <asp:PlaceHolder ID="footermain" runat="server"></asp:PlaceHolder>
        </div>
    </div>
    </form>
</body>
</html>
