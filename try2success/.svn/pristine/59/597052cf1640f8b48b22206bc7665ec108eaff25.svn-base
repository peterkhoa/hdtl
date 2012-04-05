<%@ Page Title="" Language="C#" MasterPageFile="~/managepage/SiteManagement.Master" AutoEventWireup="true" CodeBehind="manageForum.aspx.cs" Inherits="hoachdinhtuonglai.managepage.manageForum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <h1>Thêm Forum</h1>
    <h5><%=mess %></h5>
    <div>
        <div>Tên Forum:</div>
        <div>
            <asp:TextBox ID="txtForumName" runat="server"></asp:TextBox></div>
        <div>Mục:</div>
        <div>
            <asp:DropDownList ID="DropDownListCate" runat="server" 
                DataSourceID="ObjectDataSource2" DataTextField="CateName" 
                DataValueField="ID">
            </asp:DropDownList>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" 
                SelectMethod="SelectAll" TypeName="hoachdinhtuonglai.Data.Core.CategoryDA"></asp:ObjectDataSource>
        </div>
        <asp:Button ID="btnAdd" runat="server" Text="Thêm" onclick="btnAdd_Click" />
    </div>
    <h1>Danh sách các Forum</h1>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="ForumName" HeaderText="ForumName" 
                SortExpression="ForumName" />
            <asp:BoundField DataField="CateID" HeaderText="CateID" 
                SortExpression="CateID" />
            <asp:BoundField DataField="ForumNameSlug" HeaderText="ForumNameSlug" 
                SortExpression="ForumNameSlug" />
            <asp:BoundField DataField="Order" HeaderText="Order" SortExpression="Order" />
            <asp:CheckBoxField DataField="Active" HeaderText="Active" 
                SortExpression="Active" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="hoachdinhtuonglai.Data.Core.Forum" DeleteMethod="Delete" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="SelectAll" TypeName="hoachdinhtuonglai.Data.Core.ForumDA" 
        UpdateMethod="Update"></asp:ObjectDataSource>

    <br />
    <br />
    <h2>Add mod cho Forum</h2>
    <h5><%=mess2 %></h5>
    <div>Username:</div>
    <div>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></div>
    <div>Forum:</div><div>
        <asp:DropDownList ID="DropDownListForum" runat="server" 
            DataSourceID="ObjectDataSource1" DataTextField="ForumName" 
            DataValueField="ID">
        </asp:DropDownList>
        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" 
            TypeName="hoachdinhtuonglai.Data.Core.ForumDA"></asp:ObjectDataSource>
    </div>
    <asp:Button ID="btnAddMod" runat="server" Text="Add mod" 
        onclick="btnAddMod_Click" />
</div>
<br />
<div>
    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSource4">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" 
                SortExpression="UserName" />
            <asp:CheckBoxField DataField="Active" HeaderText="Active" 
                SortExpression="Active" />
            <asp:BoundField DataField="AccountID" HeaderText="AccountID" 
                SortExpression="AccountID" />
            <asp:BoundField DataField="ForumID" HeaderText="ForumID" 
                SortExpression="ForumID" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
        DataObjectTypeName="hoachdinhtuonglai.Data.Core.ForumInRole" DeleteMethod="Delete" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" 
        TypeName="hoachdinhtuonglai.Data.Core.ForumInRoleDA" UpdateMethod="Update"></asp:ObjectDataSource>
</div>
</asp:Content>
