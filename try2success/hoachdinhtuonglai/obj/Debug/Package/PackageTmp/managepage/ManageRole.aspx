<%@ Page Title="" Language="C#" MasterPageFile="~/managepage/SiteManagement.Master" AutoEventWireup="true" CodeBehind="ManageRole.aspx.cs" Inherits="hoachdinhtuonglai.managepage.ManageRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1 style="color:Red;"><%=mess %></h1>
    <%if (allow)
      { %>
    <div>
        <h2>Thiết đặt quyền truy cập cho User</h2>
        <div>Username:</div>
        <div>
            
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></div>
            <div>Assign to: </div>
            <div>
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    DataSourceID="ObjectDataSource1" DataTextField="Name" DataValueField="ID">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAll" 
                    TypeName="hoachdinhtuonglai.Data.Core.AccountRoleDA" 
                    DataObjectTypeName="hoachdinhtuonglai.Data.Core.AccountRole" DeleteMethod="Delete" 
                    UpdateMethod="Update"></asp:ObjectDataSource>
            </div>
            <div>
                <asp:Button ID="btnAdd" runat="server" Text="Add role" onclick="btnAdd_Click" 
                    /></div>
    </div>
    <br />
    <div>
        <h2>Danh sách Roles</h2>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="ObjectDataSource1" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ID">
            <Columns>
                <asp:CommandField 
                    ShowSelectButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" 
                    SortExpression="Description" />

                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
            DataObjectTypeName="hoachdinhtuonglai.Data.Core.AccountInRole" DeleteMethod="Delete" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="SelectByRoleID" 
            TypeName="hoachdinhtuonglai.Data.Core.AccountInRoleDA" UpdateMethod="Update">
            <SelectParameters>
                <asp:ControlParameter ControlID="GridView1" Name="roleID" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource><br />
        <h2>Danh sách Account in Roles</h2>
        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ID" 
            DataSourceID="ObjectDataSource2" ForeColor="Black" GridLines="Vertical" 
            PageSize="20">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="RoleName" HeaderText="RoleName" 
                    SortExpression="RoleName" />
                <asp:BoundField DataField="RoleID" HeaderText="RoleID" 
                    SortExpression="RoleID" />
                <asp:BoundField DataField="AccountID" HeaderText="AccountID" 
                    SortExpression="AccountID" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    </div>
    <%} %>
</asp:Content>
