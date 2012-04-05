<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="hoachdinhtuonglai._Index" %>

<%--<%@ Register src="/control/Commons/header.ascx" tagname="header" tagprefix="uc1" %>
<%@ Register src="/control/Commons/LS_Foot.ascx" tagname="footer" tagprefix="uc2" %>  --%>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html class="no-js" xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="google-site-verification" content="FBtM7_zOCVG6KhTBBYQN-zKeV6L9eFssAqLiyGcQG0M" />
    <meta name="robots" content="Index,Follow"/>
    <meta name="description" content="try to success, success, try, hoạch định tương lai, thành công, cố gắng, chia sẻ, trải nghiệm, yêu thương"/>
    <meta name="keywords" content="chia se, chiase, thanh cong, try, success, try to success, tuong lai, tintuc, tin tuc, video, clip hay"/>
    <link href="/Styles/Site.css" type="text/css" rel="Stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js" type="text/ecmascript"></script>
    
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-29264508-1']);
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
                        <asp:PlaceHolder ID="top" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="side_left">
                        <asp:PlaceHolder ID="sideleft" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="side_right">
                        <asp:PlaceHolder ID="sideright" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="foot">
                        <asp:PlaceHolder ID="foot" runat="server"></asp:PlaceHolder>
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

<script type="text/javascript">
    $(document).ready(function () {
        //alert($('body div:last-child').text());
    });
</script>
