<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="hotpost.ascx.cs" Inherits="hoachdinhtuonglai.control.Post.hotpost" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<div class="topright">
    <div class="gray-box2">
        <div class="gray-box-top">
            <div class="l">
                <div class="r">
                    <strong>Bài xem nhiều</strong>
                </div>
            </div>
            <div class="gray-box-content">
                <div class="inner" style="min-height: 130px;">
                    <ul>
                        <% foreach (Post p in listPostHot)
                           { %>
                        <li style="font-size: 12px;"><a style="color: #1b1b1b;" href="<%=LinkBuilder.getLinkPost(p.ID,p.Title) %>" target="_blank"
                            title="<%=p.Title %>">
                            <%=p.Title %></a></li>
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
</div>
