﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="commentajax.ascx.cs" Inherits="hoachdinhtuonglai.control.Comment.commentajax" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.BL" %>
<style type="text/css">
    .commentajax
    {
        display: none;
    }
    .in-reply a
    {
        color: #44aa00;
        cursor: pointer;
        margin-left: 5px;
        margin-bottom: 8px;
    }
    #Button1
    {
        width: 58px;
    }
    .pageSelected{background:#fcfcfc;border:1px solid #ccc;text-align:center;max-width:160px;}
    .pageSelected:hover{cursor:pointer;}
    .page{max-width:700px;text-align:center;margin-left:300px;}
</style>


<%
    //Account a = AccountDA.SelectByID(long.Parse(Request["uid"])); //DeltaViet.control.mygoals.PersonalGoal.author;
    //; Current_user;
    if(usercurr != null && usercurr.ID > 0){
    double count = (double)CommentDA.countAll(objectID, objecttype);
%>
<div class="container special_box" id="mdl-forum-sub">
    <div id="mdl-forum-sub">
        <div class="comment-unit" style="margin-top: 0px; padding-top: 0px;">
            <div class="unit" style="background:#fff;">
                <div class="usr" style="width: 100%;">
                    <p style="margin-bottom: 10px; margin-top: -15px;">
                        <a href="#" title=""><strong>Gửi lời bình luận</strong></a>
                    </p>
                    <%--<p>
                        <asp:TextBox ID="txtComment" TextMode="MultiLine" Width="670px" runat="server"></asp:TextBox>
                    </p>
                    <asp:Button ID="SendComment" runat="server" Text="Send" OnClick="SendComment_Click" />--%>
                    <p>
                        <textarea id="txtComment2" name="txtComment" rows="4" style="width: 97%"></textarea>
                    </p>
                    <p>
                        <input type="button" id="btnSend" value="Gửi" onclick="return commentajax2('<%=objectID %>', '<%=objecttype %>','<%=target_link %>','<%=target_object_name %>', 'txtComment2', 'resultComment', 'imgsending', '<%=receiverID %>','1')" />
                        <span
                            style="display: none" id="imgsending"><img src="http://dl.dropbox.com/u/19053289/DV/sending.gif"
                                style="width: 20px; margin-bottom: -5px;" /></span></p>
                    <p>
                    </p>
                </div>
                <div class="clear">
                </div>
            </div>
            <script type="text/javascript">                

                function commentajax2(objectid, objecttype, targetlink, targetobjectname, textid, resultid, imgaesedingid, uid, parent) {

                    var link = '/ajax.aspx?xcontrol=comment-childcommentajax';
                    var text = $('#' + textid).val();

                    //alert(text);
                    //alert(document.getElementById(textid).value);

                    if (text != '') {
                        $('#' + imgaesedingid).show();
                        $.ajax({
                            type: 'POST',
                            url: link,
                            data: 'objectid=' + objectid + '&objecttype=' + objecttype + '&targetlink=' + targetlink + '&targetobjectname=' + targetobjectname + '&text=' + text + '&uid=' + uid + '&par=' + parent,
                            success: function (html) {
                                //$('#' + resultid).text("");
                                //$('#' + resultid).append(html);
                                $('#commentdetail').prepend(html);
                                //$('#' + hidecomment).hide();
                                $('#' + imgaesedingid).hide();
                                $('#' + textid).val("");
                            },
                            error: function (html) {
                                $('#' + resultid).append("");
                            }
                        });
                    }
                    else {
                        alert('Bạn chưa nhập nội dung.');
                        return;
                    }
                }

                function commentajax3(commentidparent, objectid, objecttype, targetlink, targetobjectname, textid, resultid, imgaesedingid, uid) {

                    var link = '/ajax.aspx?xcontrol=comment-childcommentajax';
                    var text = $('#' + textid).val();

                    //alert(text);
                    //alert(document.getElementById(textid).value);

                    if (text != '' && text != 'Viết bình luận...') {
                        $('#' + textid).hide();
                        $('#' + imgaesedingid).show();

                        $.ajax({
                            type: 'POST',
                            url: link,
                            data: 'commentidparent=' + commentidparent + '&objectid=' + objectid + '&objecttype=' + objecttype + '&targetlink=' + targetlink + '&targetobjectname=' + targetobjectname + '&text=' + text + '&uid=' + uid,
                            success: function (html) {
                                //$('#' + resultid).text("");
                                //$('#' + resultid).append(html);
                                $('#' + resultid).append(html);
                                //$('#' + hidecomment).hide();
                                $('#' + imgaesedingid).hide();
                                $('#' + textid).show();
                                $('#' + textid).val("");
                            },
                            error: function (html) {
                                $('#' + resultid).append("");
                            }
                        });
                    }
                    else {

                        alert('Bạn chưa nhập nội dung.');
                        $('#' + textid).val('Viết bình luận...');
                        return;
                    }
                }

                function showcomment(item) {
                    $('#' + item).show();
                }
                function hidecomment(item) {
                    $('#' + item).css('display', 'none');
                }

                $(document).ready(function () {
                    $('.special_box').css("background-color", "white");
                    //alert("Hen xui!");
                });

                $('.traloi').click(function () {
                    $('#')
                });
            </script>
            <p id="commentdetail">
            </p>
            <p id="resultComment">
            </p>
            <div id="pageComment">
            <%
                int k = 0,number=0;
                foreach (Comment c in listComment)
                {
                    Account author = AccountDA.SelectByID(c.AccountID);
                    string cl = "", border = "";
                    if (c.comment_level == 0)
                    {
                        cl = "#fff";
                        if(k > 0)
                            border = "1px solid #BDC7D8";
                    }
                    else
                    {
                        cl = "#EEF4F8";
                        border = "0";
                    }
                    ++k;
                    string profile = LinkBuilder.getLinkProfile(author);
            %>
            <div class="unit" style="background:<%=cl%>; margin-left: <%=(c.comment_level) * 50%>px; border-top:<%=border%>;" >
                <div class="usl" style="width: 40px;">
                    <a href="<%=profile %>" title="<%= author.Username %>">
                        <img style="height: 40px; width: 40px" src="<%= author.Avatar  %>" alt="no ava" /></a></div>
                <div class="usr" style="width: <%=(625-c.comment_level * 50)%>px; float: none; margin-left: 45px;">
                    <p style="margin-bottom: 0;">
                        <a href="<%=profile%>" title="<%=author.Username %>">
                            <strong>
                                <%=author.Username %></strong></a> <span class="ucomment_small">[
                                    <%= Library.ParseContent.ParsePostBodyWithHtml( author.Dream, 20 ) +  " ... "%>
                                    ]</span></p>
                    <p style="margin: 0px; width: 660px;">
                        <%= c.Content  %>
                    </p>
                    <% if ((c.Username == Page.User.Identity.Name) || (Context.User.IsInRole("Admin")) || (Context.User.IsInRole("Manager")))
                       { %>
                    <p class="leave">
                        <a href="#"
                            class="thickbox" title="Xóa bài viết">Xóa</a></p>
                    <%} %>
                    <p class="small">
                        <%= Library.Time.GetDuration( c.Date.Value)  %></p>
                    <br />
                </div>
                 <div id="result_<%=c.ID %>"></div>
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
                                            name="add_comment_text" title="Viết bình luận..." class="textInput" onkeydown="return setData(event,'<%=c.ID %>','<%=objectID %>', '<%=objecttype %>','<%=target_link %>','<%=target_object_name %>', 'texthome_<%=c.ID %>', 'result_<%=c.ID %>', 'imgsending<%=c.ID %>', '<%=receiverID %>')" >Viết bình luận...</textarea>
                                            <%--<input type="text" id="texthome_<%=c.ID %>" class="textInput" value="Viết bình luận" />--%>
                                            <span style="display: none" id="imgsending<%=c.ID %>"><img src="http://dl.dropbox.com/u/19053289/DV/sending.gif"
                                style="width: 20px; margin-bottom: -5px;" /></span>
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
           
            <%number++;
       } %>
       </div>
        </div>
        <div class="clear">
        </div>
        <ul style="margin-top: 20px; list-style-type:none;text-align:center;" id="jumppage">
          
            
       <%
           if (page == 0)
               page = 1;
           for (int i = 0; i < Math.Ceiling(count / pagesize); i++)
           {
               
               if(i > 0)
                   Response.Write("<li " + (i == 1 ? " style='display:block;height:20px;padding-top:5px;'" : "style='display:none;'") + " id='" + (i + 1) + "'><span class='pageSelected'  id='" + (i + 1) + "_" + objectID + "_" + objecttype + "'" + (i == 1 ? " style='display:block;'" : "style='display:none;'") + ">Xem thêm</span><span id='pro" + (i + 1) + "_" + objectID + "_" + objecttype + "' style='display:none;' ><img src='http://dl.dropbox.com/u/19053289/DV/loading.gif' width='50px' /></span></li>");
               }
            %>
            
        </ul>
        <div style="clear: both" >
        </div>
    </div>
</div>
<div style="clear: both">
</div>
<script type="text/javascript">

    function pagings() {

        var id = this.id;
        alert(id);
        var p = id.substring(0, id.indexOf('_'));
        var oi = id.substring(id.indexOf('_') + 1, id.lastIndexOf('_'));
        var ot = id.substring(id.lastIndexOf('_') + 1, id.length);
        alert(p + "_" + oi + "_" + ot);
        if (page != '') {
            $('.pageSelected').addClass(' selected');
            var link = '/ajax.aspx?xcontrol=comment-commentPaging';
            $.ajax({
                url: link,
                data: 'cpg=' + p + '&objectid=' + oi + '&objecttype=' + ot,
                success: function (html) {
                    $('#pageComment').append(html);
                }
            });
        }
        else {
            alert('Đã hết bình luận.');
            return;
        }
    }
    $(document).ready(function () {

    });


    $('.pageSelected').click(function () {

        var id = this.id.toString();
        var p = id.substring(0, id.indexOf('_'));
        var oi = id.substring(id.indexOf('_') + 1, id.lastIndexOf('_'));
        var ot = id.substring(id.lastIndexOf('_') + 1, id.length);
        //alert(p + "+" + oi + "+" + ot);
        if (p != '') {

            var n = parseInt(p);
            $('#' + id).removeClass("pageSelected");
            $('#' + id).css("display", "none");
            $('#pro' + id).show();

            //alert(n + 1);
            var link = '/ajax.aspx?xcontrol=comment-commentPaging';
            $.ajax({
                url: link,
                data: 'cpg=' + p + '&objectid=' + oi + '&objecttype=' + ot,
                success: function (html) {
                    $('#pageComment').append(html);
                    $('#pro' + id).hide();
                    $('#' + (n + 1) + '_' + oi + '_' + ot).css("display", "block");
                    $('#' + (n + 1)).css("display", "block");
                }
            });
        }
        else {
            alert('Trang nay khong co.');
            return;
        }
    });

    $('.textInput').click(function () {
        $('#' + this.id.toString()).val("");
        //alert("vv");
    });

    function setData(e, commentidparent, objectID, objectType, target_link, target_object_name, idContent, result, img, receiverID) {

        //alert(idContent);
        if (!e)
            e = window.event;
        var keynum; // = this.window.event.keyCode;
        if (e) {
            keynum = e.keyCode;
        }
        else if (e.which) {
            keynum = e.which;
        }

        //alert(keynum);

        if (keynum == '13' && !e.shiftKey) {
            if (commentidparent != '') {
                commentajax3(commentidparent, objectID, objectType, target_link, target_object_name, idContent, result, img, receiverID);
            }
        }
    }

    
    
</script>
<%}else{ %>
<div>Bạn cần đăng nhập mới sử dụng được chức năng bình luận. <a href="/dang-nhap/" >Đăng nhập</a></div>
<%} %>