<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="commentPaging.ascx.cs" Inherits="hoachdinhtuonglai.control.Comment.commentPaging" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<%@ Import Namespace="Library" %>
<div id="pageComment">
            <%
                foreach (Comment c in CC)
                {
                    Account author = AccountDA.SelectByID(c.AccountID);
                    string profile = LinkBuilder.getLinkProfile(c.Username);
            %>
            <div class="unit" style="margin-left: <%=(c.comment_level) * 50%>px;" >
                <div class="usl" style="width: 20px; height: 20px">
                    <a href="<%= profile %>" title="<%= author.Username %>">
                        <img style="height: 20px; width: 20px" src="<%= author.getAvatar()  %>" alt="no ava" /></a></div>
                <div class="usr" style="width: 675px; float: none; margin-left: 18px;">
                    <p style="margin-bottom: 0;">
                        <a href="<%= profile %>" title="<%=author.Username %>">
                            <strong>
                                <%=author.Username %></strong></a> <span class="ucomment_small">[
                                    <%= Library.ParseContent.ParsePostBodyWithHtml( author.Slogan, 20 ) +  " ... "%>
                                    ]</span></p>
                    <p style="margin: 0px; width: 660px;">
                        <%= c.Content  %>
                    </p>
                    <% if ((receiver.Username == Page.User.Identity.Name) || (Context.User.IsInRole("Admin")) || (Context.User.IsInRole("Manager")))
                       { %>
                    <p class="leave">
                        <a href="/popup.aspx?page=delcomment&cid=<%=c.ID %>&placeValuesBeforeTB_=savedValues&TB_iframe=true&height=300&width=300&modal=true"
                            class="thickbox" title="Xóa bài viết">Xóa</a></p>
                    <%} %>
                    <p class="small">
                        <%= Library.Time.GetDuration( c.Date.Value)  %></p>
                    <br />
                </div>
                <% if (c.comment_level == 0)
                   { %>
                  <div class="commentArea UIImageBlock_Content UIImageBlock_ICON_Content" style="margin-left:45px;width:600px;">
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
                                        <textarea id="texthome_<%=c.ID %>"  autocomplete="off" onfocus=""
                                            name="add_comment_text" title="Viết bình luận..." class="textInput" onkeydown="return setData(event,'<%=c.ID %>','<%=objectID %>', '<%=objecttype %>','<%=target_link %>','<%=target_object_name %>', 'texthome_<%=c.ID %>', 'result_<%=c.ID %>', 'imgsending', '<%=receiverID %>')" >Viết bình luận...</textarea>
                                            <%--<input type="text" id="texthome_<%=c.ID %>" class="textInput" value="Viết bình luận" />--%>
                                            </div>
                                    <%}
                                      else
                                      { %>
                                    <div class="innerWrap">
                                        <textarea id="texthome_<%=number %>" onkeydown="csscody.alert(&#39;&lt;h1&gt;Bạn chưa đăng nhập&lt;/h1&gt;&lt;br/&gt;&lt;p&gt;Đã có tài khoản: click vào  &lt;a href=\&#39;http://www.deltaviet.net/dang-nhap/\&#39;&gt;đăng nhập&lt;/a&gt; &lt;/p&gt;&lt;p&gt; Hoặc chưa có tài khoản: click vào  &lt;a href=\&#39;http://www.deltaviet.net/dang-ky/\&#39;&gt;đăng ký&lt;/a&gt; &lt;/p&gt;&#39;);return false;"
                                            autocomplete="off" onfocus="" name="add_comment_text" title="Viết bình luận..."
                                            class="textInput" style="height:30px;width:600px;">Viết bình luận...</textarea></div>
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
            <div id="result_<%=c.ID %>"></div>
            <%number++;
       } %>
       </div>