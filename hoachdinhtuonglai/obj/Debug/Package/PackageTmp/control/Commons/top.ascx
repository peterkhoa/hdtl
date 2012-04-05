<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="top.ascx.cs" Inherits="hoachdinhtuonglai.control.Commons.top" %>
<div style="margin: 10px 0 5px 10px; width: 450px; float: left;">
    <script type='text/javascript' src='/Scripts/jwplayer.js'></script>
    <div class="player">
        <object width="430px" height="355px" name="player" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"
            id="player">
            <param value="/player.swf" name="movie" />
            <param value="true" name="allowfullscreen" />
            <param value="never" name="allowscriptaccess" />
            <param value="file=file=/playlist.xml&amp;autostart=true&amp;shuffle=1&amp;repeat=list&amp;autostart=true"
                name="flashvars" />
            <embed width="430px" height="355px" flashvars="file=/playlist.xml&amp;autostart=true&amp;shuffle=1&amp;repeat=list"
                allowfullscreen="true" allowscriptaccess="always" src="/player.swf" name="player2"
                id="player2" type="application/x-shockwave-flash">
        </object>
    </div>
    <div id='mediaspace'>
    </div>
</div>
