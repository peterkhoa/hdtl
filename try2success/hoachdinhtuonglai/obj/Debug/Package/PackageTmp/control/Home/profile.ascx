<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="profile.ascx.cs" Inherits="hoachdinhtuonglai.control.Home.profile" %>
<div class="box-border box_profile">
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
    <div class="input-row">
        <label for="txtUsername">NickName</label>
        <asp:TextBox ID="txtUsername" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="txtLastName">Email</label>
        <asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
    </div>
    <hr />
    <div class="input-row">
        <label for="txtAvatar">Link avatar</label>
        <asp:TextBox ID="txtAvatar" runat="server" ClientIDMode="Static" Text="http://" Width="200px"></asp:TextBox>(Chỉ hỗ trợ các file: .jpg, .png, .gif)
    </div>
    <div class="input-row">
        <label for="txtFirstName">Họ</label>
        <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="txtLastName">Tên</label>
        <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    
    <div class="input-row">
        <label for="txtBD">Ngày sinh</label>
        <asp:TextBox ID="txtBD" runat="server" ClientIDMode="Static" CssClass="date-select"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="DropGender">Giới tính</label>
        <asp:DropDownList ID="DropGender" runat="server">
            <asp:ListItem Value="Nam">Nam</asp:ListItem>
            <asp:ListItem Value="Nữ">Nữ</asp:ListItem>
            <asp:ListItem Value="Khác">Khác</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="input-row">
        <label for="txtSlogan">Slogan:</label>
        <asp:TextBox ID="txtSlogan" runat="server" ClientIDMode="Static" Width="330px"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="txtDream">Ước mơ:</label>
        <asp:TextBox ID="txtDream" runat="server" ClientIDMode="Static"
            Width="330px"></asp:TextBox>
    </div>
    <hr />
    <h2>Thông tin phụ</h2>
    <div class="input-row">
        <label for="txtAddress">Địa chỉ</label>
        <asp:TextBox ID="txtAddress" runat="server" ClientIDMode="Static" Width="185px"></asp:TextBox><span><asp:DropDownList ID="DropCity" runat="server">
            <asp:ListItem Value="ho-chi-minh">Tp Hồ Chí Minh</asp:ListItem>
            <asp:ListItem Value="ha-noi">Hà Nội</asp:ListItem>
            <asp:ListItem Value="dong-thap">Đồng Tháp</asp:ListItem>
        </asp:DropDownList></span>
    </div>
    <div class="input-row">
        <label for="txtyahoo">Nick Yahoo</label>
        <asp:TextBox ID="txtyahoo" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
     <div class="input-row">
        <label for="txtPhone">Điện thoại</label>
        <asp:TextBox ID="txtPhone" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="txtfacebook">Nick facebook</label>
        <asp:TextBox ID="txtfacebook" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="txtSkype">Nick Skype</label>
        <asp:TextBox ID="txtSkype" runat="server" ClientIDMode="Static"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="txtSchool">Trường theo học:</label>
        <asp:TextBox ID="txtSchool" runat="server" ClientIDMode="Static"
            Width="280px"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="txtCompany">Công ty:</label>
        <asp:TextBox ID="txtCompany" runat="server" ClientIDMode="Static"
            Width="280px"></asp:TextBox>
    </div>
    <div class="input-row">
        <label for="txtSothich">Sở thích:</label>
        <asp:TextBox ID="txtSothich" runat="server" ClientIDMode="Static" Width="280px"></asp:TextBox>
    </div>
    <br />
    <div class="input-row">
        <label></label>
        <asp:Button ID="btnSave" runat="server" CssClass="button highlight" Text="Lưu" 
            Width="90px" onclick="btnSave_Click" />
    </div>
</div>