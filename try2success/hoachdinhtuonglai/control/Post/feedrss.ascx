<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="feedrss.ascx.cs" Inherits="hoachdinhtuonglai.control.Post.feedrss" %>
<%@ Import Namespace="hoachdinhtuonglai.Data.Core" %>
<script src="/Scripts/slides.min.jquery.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $('#slides').slides({
            generateNextPrev: false,
            play: 8000,
            generatePagination: false,
            fadeSpeed: 250, 
            preload: true
        });
    });
</script>
<div class="topright">
    <div class="gray-box2">
        <div class="gray-box-top">
            <div class="l">
                <div class="r">
                    <strong>News feed</strong>
                </div>
            </div>
            <div class="gray-box-content">
                <div class="inner" style="min-height: 130px;">
                    <div id="slides">
                        <div class="slides_container">
                            <asp:Repeater ID="ListFeedRepeater" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <p style="font-size: 12px; padding:5px;">
                                            
                                                <img src="<%# (hoachdinhtuonglai.Data.BL.StringFormat.GetFirstImage(Eval("Description").ToString())) %>" style="width:80px;height:auto;float:left;margin-right:5px;"
                                                    alt="" />
                                                    <span><a style="font-weight:bold;" href="<%# Eval("Link") %>" title="<%# Eval("Title") %>" target="_blank">
                                                        <%# Eval("Title")%></a></span><br />
                                                        <%# hoachdinhtuonglai.Data.BL.StringFormat.Truncate(Eval("Description").ToString(),150) %>
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
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
