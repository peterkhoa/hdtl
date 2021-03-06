﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sharing.ascx.cs" Inherits="hoachdinhtuonglai.control.Commons.sharing" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<div class="categorybox">
    
    <hr />
    <br />
    <div>
        <h2>Đọc gì hôm nay ?</h2>
        <hr />
        <div>
            <%
                Account ac = new Account();
                string link, title, shortContent;
                if (listPost.Count > 0)
                {
                    foreach (Post p in listPost)
                    {
                        link = LinkBuilder.getLinkProfile(p.UserName);
                        title = p.Title;
                        shortContent = Library.LanguageConvert.StripHtml(p.Content, false);
                        shortContent = shortContent.Length > 250 ? shortContent.Substring(0, shortContent.IndexOf(" ", 230)) : shortContent;
                        if (title.Length > 100)
                        {
                            title = title.Substring(0, title.LastIndexOf(" ", 80));
                            title += " ...";
                        }
                        int countComment = CommentDA.countAll(p.ID, "comment");
                        if (p.Comment != countComment)
                        {
                            p.Comment = countComment;
                            PostDA.Update(p);
                        } 
                %>
                <div class="box_post">
                    <div class="listleft">
                        <div class="user">
                            <div class="avatar"><a href="<%=link %>" target="_blank" ><img src="<%=p.getLinkAvatar() %>" alt="" /></a></div>
                            <p class="nickname"><a href="<%=link %>" target="_blank" ><%=p.UserName%></a></p>
                        </div>
                    </div>
                    <div class="listright">
                        <div class="post_title"><a href="<%=LinkBuilder.getLinkPost(p.ID, p.ObjectID.Value, p.Title) %>" target="_blank" title=""><%=title%></a></div>
                        <div class="note"><span>Xem (<%=p.View%>)</span> <span>Thích (<%=p.Vote%>)</span><span> Gửi bởi <a href="<%=link %>" target="_blank"><%=p.UserName %></a>&nbsp;Bình luận (<%=countComment %>)</span></div>
                        <div class="post_content"><%=shortContent%> ...</div>
                    </div>
                </div>
                
                <%
                    }
                }
                 %>
        </div>
    </div>
</div>