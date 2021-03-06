﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="newpost.ascx.cs" Inherits="hoachdinhtuonglai.control.Post.newpost1" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<%@ Import Namespace="Library" %>
<div>
    <%
        string shortContent = "";
        
        foreach (Post p in listPost)
        {
            shortContent = LanguageConvert.StripHtml(p.Content,false);
            if(shortContent.Length > 500){
                shortContent = shortContent.Substring(0, shortContent.IndexOf(" ", 450)) + " ...";
                
            }
            
            int countComment = CommentDA.countAll(p.ID, "comment");
            if (p.Comment != countComment)
            {
                p.Comment = countComment;
                PostDA.Update(p);
            }
            is_owner = (Current_user.Username == p.UserName) ? true : false;
            %>
            <div class="box_post">
                <h3 class="post_title"><a href="<%=LinkBuilder.getLinkPost(p.ID, p.ObjectID.Value, p.Title) %>" title="<%=p.Title %>" target="_blank"><%=p.Title %></a></h3>
               
                <div class="post_content"><%=shortContent %></div>
                 <div class="note" style="text-align:right;">Gửi bởi: <span class="author"><a href="<%=LinkBuilder.getLinkProfile(p.UserName) %>" target="_blank"><%=p.UserName %></a></span>;<span>Xem(<%=p.View %>)</span>;<span>Bình luận(<%=p.Comment %>)</span>
                <%
            if (is_owner || IsAdmin)
            {
                     %>
                ;<span><a href="/?page=compose&type=edit&postid=<%=p.ID %>">Sửa | Xóa</a></span>
                
                <%} %>

               </div>
            </div>
            <%
        }
         %>
</div>