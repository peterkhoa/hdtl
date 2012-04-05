<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Registration.ascx.cs"
    Inherits="hoachdinhtuonglai.control.Commons.Registration" %>
<div class="Reg">
    <p class="titleHead" style="height: 40px; font-size: 20px; text-align: center; color: Blue;
        padding: 5px; margin: 0 0 0 0;">
        Đăng ký</p>
    <p class="msgError">
        <%=msg %></p>
    <div class="RegLeft">
        <div class="text">
            Họ:</div>
        <div>
            <asp:TextBox ID="txtFirstName" runat="server" Width="181px"></asp:TextBox></div>
             <div class="text">
            Tên:</div>
        <div>
            <asp:TextBox ID="txtLastName" runat="server" Width="181px"></asp:TextBox></div>
        <div class="text">
            Username:</div>
        <div>
            <asp:TextBox ID="txtUsername" runat="server" Width="181px"></asp:TextBox></div>
        <div class="text">
            Password:</div>
        <div>
            <asp:TextBox ID="txtPass" runat="server" Height="20px" Width="181px" TextMode="Password"></asp:TextBox></div>
        <div class="text">
            Email:</div>
        <div>
            <asp:TextBox ID="txtEmail" runat="server" Width="179px"></asp:TextBox></div>
        
        <div class="text">
        <% 
            a = rand.Next(0, 9);
                         b = rand.Next(0, 9);
                             kq = a + b;
                             HttpCookie ketqua = new HttpCookie("ketqua");
                             ketqua.Value = Library.Encryptor.Encrypt(kq.ToString());
                             Response.Cookies.Add(ketqua);
                              %>
            Câu hỏi kiểm tra an toàn: <strong>
                <%=a %>
                +
                <%=b %>
                = ?</strong> (trả lời bằng số)</div>
    <div>
        <asp:TextBox ID="txtKq" runat="server" Width="50px"></asp:TextBox></div>
    </div>
    <%--<div class="RegRight">
        <div>Hình đại diện của bạn</div>
        <div><img src="" id="imgAvatar" class="avatarLarge" alt="" /></div>
        <div><input type="file" id="txtfile" runat="server" class="selectAvatar" onchange="javascript:changePic(this.value.replace('C:\\fakepath\\', ''))" /></div>
    </div>--%>
    <div class="RegRight">
        <div>
            <img src="/images/main/logo.jpg" width="200px" alt="" /></div>
    </div>
    <p style="width: 500px; text-align: center; float: left;">
        <asp:Button ID="btnReg" runat="server" Text="Đăng ký" CssClass="btnYes" Height="26px"
            Width="70px" OnClick="btnReg_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <input type="button" id="btnCancel" value="Làm lại" class="btnNo" />
    </p>
    <div class="clear">
    </div>
</div>
<div class="clear">
</div>
<script type="text/javascript">
    function changePic(obj) {
        var src = obj;
        //alert(src);
        var img = document.images['imgAvatar'];
        img.src = "file://" + src;
    }

    
</script>
