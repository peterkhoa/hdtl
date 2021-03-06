﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="childcommentajax.ascx.cs"
    Inherits="hoachdinhtuonglai.control.Comment.childcommentajax" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<%
    
    if (!string.IsNullOrEmpty(text))
    {
        if (commentid > 0)
        {
            //post = PostDA.SelectByID(postid);
            cm = CommentDA.SelectByID(commentid);
            commentor = Current_user;

            if (commentor != null)
            {
                Random r = new Random();
                int orderpost = r.Next();
                string profile = LinkBuilder.getLinkProfile(commentor.Username);
                string cl = "", border = "";
                if (parent == 1)
                {
                    cl = "#fff";

                }
                else
                {
                    cl = "#EEF4F8";

                }
                //long idofcomment = PostDA.AddPostComment(Current_user.ID, Request.UserHostName, postid, text, "", "", PostType.DiscussionComment.ToString(), "vi");
               // Post p = PostDA.SelectByID(idofcomment);
%>
<div class="unit" style="background: <%=cl%>; margin-left: <%=((parent == 0) ? 45 : 0)%>px;">
    <div class="usl" style="width: 40px;">
        <a href="<%= profile %>" title="<%= commentor.Username %>">
            <img style="height: 40px; width: 40px" src="<%= commentor.getAvatar()  %>" alt="no ava" /></a></div>
    <div class="usr" style="width: 625px; float: none; margin-left: 45px;">
        <p style="margin-bottom: 0;">
            <a href="<%= profile %>" title="<%=commentor.Username %>"><strong>
                <%=commentor.Username %></strong></a> <span class="ucomment_small">[
                    <%= Library.ParseContent.ParsePostBodyWithHtml(commentor.Slogan, 20) + " ... "%>
                    ]</span></p>
        <p>
            <%= cm.Content%>
        </p>
        <% if ((receivers.Username == Page.User.Identity.Name) || (Context.User.IsInRole("Admin")) || (Context.User.IsInRole("Manager")))
           { %>
        <p class="leave">
            <a href="#" class="thickbox" title="Xóa bài viết">Xóa</a></p>
        <%} %>
        <p class="small">
            <%= Library.Time.GetDuration( cm.Date.Value)  %></p>
        <br />
    </div>
    <div id="result_<%=cm.ID %>">
    </div>
    <%
                if (parent == 1)
                {
    %>
    <div class="commentArea UIImageBlock_Content UIImageBlock_ICON_Content" style="margin-left: 45px;
        width: 600px;">
        <div class="commentBox">
            <div id="u648381_12" class="uiMentionsInput textBoxContainer">
                <div class="highlighter">
                    <div>
                        <span class="highlighterContent"></span>
                    </div>
                </div>
                <div id="u648381_13" class="uiTypeahead mentionsTypeahead">
                    <div class="wrap">
                        <input type="hidden" class="hiddenInput" autocomplete="off">
                        <%if (usercurr != null)
                          { %>
                        <div class="innerWrap">
                            <textarea id="texthome_<%=cm.ID %>" autocomplete="off" onfocus="" name="add_comment_text"
                                title="Viết bình luận..." class="textInput" onkeydown="return setData(event,'<%=cm.ID %>','<%=objectID %>', '<%=objecttype %>','<%=target_link %>','<%=target_object_name %>', 'texthome_<%=cm.ID %>', 'result_<%=cm.ID %>', 'imgsending<%=cm.ID %>', '<%=receiverID %>')"></textarea>
                            <%--<input type="text" id="texthome_<%=c.ID %>" class="textInput" value="Viết bình luận" />--%>
                            <!--span><input type="button" name="btnsend" id="btnsend" value="Gui" /></span-->
                            <span style="display: none" id="imgsending<%=cm.ID %>">
                                <img src="http://dl.dropbox.com/u/19053289/DV/sending.gif" style="width: 20px; margin-bottom: -5px;" /></span>
                        </div>
                        <%}
                          else
                          { %>
                        <div class="innerWrap">
                            <textarea id="texthome_<%=cm.ID %>" onkeydown="csscody.alert(&#39;&lt;h1&gt;Bạn chưa đăng nhập&lt;/h1&gt;&lt;br/&gt;&lt;p&gt;Đã có tài khoản: click vào  &lt;a href=\&#39;http://www.deltaviet.net/dang-nhap/\&#39;&gt;đăng nhập&lt;/a&gt; &lt;/p&gt;&lt;p&gt; Hoặc chưa có tài khoản: click vào  &lt;a href=\&#39;http://www.deltaviet.net/dang-ky/\&#39;&gt;đăng ký&lt;/a&gt; &lt;/p&gt;&#39;);return false;"
                                autocomplete="off" onfocus="" name="add_comment_text" title="Viết bình luận..."
                                class="textInput" style="height: 30px; width: 600px;">Viết bình luận...</textarea></div>
                        <%} %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
    <div class="clear">
    </div>
</div>
<div class="clear">
</div>
<%
            }
        }
    }
        
%>