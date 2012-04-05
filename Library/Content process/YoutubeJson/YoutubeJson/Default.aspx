<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YoutubeJson._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>you</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
        <br />
        <br />
        <asp:TextBox ID="TextBox2" runat="server" Height="235px" TextMode="MultiLine" 
            Width="576px"></asp:TextBox>

            	<p>
			<textarea>
			<% if (TextBox1.Text.Length>0)
			{ 
            //string temp = TextBox1.Text;
            //temp = temp.Replace("watch?v=","v/");
			%>
			 
			<%
			}%>
			</textarea></p>
    </div>
    </form>
    <script type="text/javascript">
        function changeTitle() { document.title = 'new'; }
        setTimeout("changeTitle()",18000);
</script>
</body>
</html>
