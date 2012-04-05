<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="post.ascx.cs" Inherits="hoachdinhtuonglai.control.Post.post" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<%@ Import Namespace="Library" %>
<%@ Register Src="~/control/Comment/commentajax.ascx" TagName="commentajax" TagPrefix="uc1" %>
<div>
    <%
        if (posts != null)
        {
        //string content = StringFormat.StripHtml(posts.Content);
        
    %>
    <h1 class="post_title">
        <%=posts.Title%></h1>
    <div class="note">
        <span><a href="<%=LinkBuilder.getLinkProfile(posts.UserName)%>">
            <%=posts.UserName%></a></span>;<span><%=posts.CreatedDate%></span>;<span>Like</span>;<span>Báo
                spam</span>
        <%if (is_owner)
          { %>
        ;<span><a href="/?page=compose&type=edit&postid=<%=posts.ID %>">Sửa | Xóa</a></span>
        <%} %>
        <%--<div id="fb-root">
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
            </div--%>>
       <div id="fb-root"></div>
      <script type="text/javascript">
          (function (d, s, id) {
              var js, fjs = d.getElementsByTagName(s)[0];
              if (d.getElementById(id)) { return; }
              js = d.createElement(s); js.id = id;
              js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
              fjs.parentNode.insertBefore(js, fjs);
          } (document, 'script', 'facebook-jssdk'));
      </script>
      <div class="fb-like"></div>
        </div>
    </div>
    <hr />
    <div class="post_content">
        <%=content%></div>
    <div class="note">
        <span><a href="<%=LinkBuilder.getLinkProfile(posts.UserName)%>">
            <%=posts.UserName%></a></span>;<span><%=posts.CreatedDate%></span>;<span>Like</span>;<span>Báo
                spam</span>
        <div class="addthis_toolbox addthis_default_style " style="float: right;">
            <!--span href="#share" style="text-decoration: none;color:#7b7b7b;" class="title_share">Share</span-->
            <a rel="" class="addthis_button_facebook" title="Share on facebook" href="https://www.facebook.com/dialog/feed?redirect_uri=http%3A%2F%2Fs7.addthis.com%2Fstatic%2Fpostshare%2Fc00.html&app_id=140586622674265&name=<%=posts.Title %>&description=<%=shortContent %>&picture=<%=matchString %>&link=<%=urlShare %>"
                target="_blank"></a>
            <!--a class="addthis_button_facebook" style="margin-right: 10px;">facebook</a-->
            <a class="addthis_button_zingme" style="margin-right: 10px;"></a><a class="addthis_button_email"
                style="margin-right: 10px;"></a><a class="addthis_button_tweet" style="margin-right: 10px;">
                </a><a class="addthis_button_google_plusone"></a><a class="addthis_button_compact"
                    style="margin-right: 10px;"></a>
            <!--a class="addthis_counter addthis_bubble_style"></a-->
            <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4f0d5e081f825580"></script>
        </div>
    </div>
    <hr />
    <h4>
        Bài viết liên quan</h4>
    <div class="relation">
        <ul>
            <%foreach (Post p in listPost)
              { %>
            <li><a href="<%= LinkBuilder.getLinkPost(p.ID, p.ObjectID.Value, p.Title) %>">
                <%=p.Title %></a></li>
            <%} %>
        </ul>
    </div>
    <!-- Load comment here -->
    <div class="box_comment">
        <uc1:commentajax ID="commentajax1" runat="server" />
    </div>
    <div class="clear">
    </div>
    <%}
    else
    {
        //if (mess != "")
        {%>
    <p style="color: Red;">
        Bài viết này không có thực hoặc đã bị khóa, bạn hãy chọn bài viết khác nhé!</p>
    <%}        
    %>
    <% 
    } %>
</div>
