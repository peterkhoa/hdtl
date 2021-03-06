﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="funny.ascx.cs" Inherits="hoachdinhtuonglai.control.Sidebar.funny" %>
<script src="/Scripts/slides.min.jquery.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $('#slides2').slides({
            generateNextPrev: false,
            play: 8000,
            generatePagination: false,
            fadeSpeed: 250,
            preload: true
        });
    });
    </script>
<div class="topright" style="width:100%;">
    <div class="gray-box2">
        <div class="gray-box-top">
            <div class="l">
                <div class="r">
                    <strong>Góc cười</strong>
                </div>
            </div>
            <div class="gray-box-content">
                <div class="inner" style="min-height: 130px;">
                    <div id="slides2">
                        <div class="slides_container">
                            <asp:Repeater ID="ListFeedRepeater" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <p style="font-size: 12px; padding:5px;">
                                            
                                                <img src="<%# (hoachdinhtuonglai.Data.BL.StringFormat.GetFirstImage(Eval("Description").ToString())) %>" style="width:80px;height:auto;float:left;margin-right:5px;"
                                                    alt="" />
                                                    <span><a style="font-weight:bold;" href="<%# Eval("Link") %>" title="<%# Eval("Title") %>">
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