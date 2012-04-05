<%@ Page Title="" Language="C#" MasterPageFile="~/managepage/SiteManagement.Master" AutoEventWireup="true" CodeBehind="manageCategories.aspx.cs" Inherits="hoachdinhtuonglai.managepage.manageCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <h4><%=mess %></h4>
    <h2>Add Category</h2>
    <div>Category name:</div>
    <div>
        <asp:TextBox ID="txtCateName" runat="server"></asp:TextBox></div>
    <div>
        <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" /></div>
</div>
<br />
<div>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="CateName" HeaderText="CateName" 
                SortExpression="CateName" />
            <asp:BoundField DataField="CateSlugName" HeaderText="CateSlugName" 
                SortExpression="CateSlugName" />
            <asp:BoundField DataField="Order" HeaderText="Order" SortExpression="Order" />
            <asp:CheckBoxField DataField="Active" HeaderText="Active" 
                SortExpression="Active" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        DataObjectTypeName="hoachdinhtuonglai.Data.Core.Categories" DeleteMethod="Delete" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" 
        TypeName="hoachdinhtuonglai.Data.Core.CategoryDA" UpdateMethod="Update"></asp:ObjectDataSource>
</div>
</asp:Content>
