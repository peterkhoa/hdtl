<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="compose.ascx.cs" Inherits="hoachdinhtuonglai.control.Post.compose" %>
<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<div>
<%if (is_login)
  {
      
       %>
    <script src="/Scripts/tinyeditor.js" type="text/javascript"></script>
    
    <div>
        <div style="color:Red;font-size:13px;"><%=mess %></div>
        <div>Tiêu đề:</div>
        <div>
            <asp:TextBox ID="txttitle" runat="server" Width="388px"></asp:TextBox></div>
        <div>Nội dung:</div>
        <i>Nên viết có dấu để mọi người dễ đọc nhé mọi người! (Font: Verdana, Size: 
        Small)</i><asp:ScriptManager ID="ScriptManager1"
            runat="server">
        </asp:ScriptManager>
        <!--div><textarea id="input" style="width:400px; height:200px" name="txteditor"></textarea></div-->
        <div>
                                
                                    <cc:HtmlEditor ID="Editor" runat="server" Height="400px" Width="600px" 
                                        
                                        AllowedAttributes="href,target,src,align,valign,face,dir,alt,title,width,height" 
                                        AllowedTags="p,b,i,u,em,big,small,div,img,span,blockquote,code,pre,br,hr,ul,ol,li,del,ins,strong,a,dl,dd,dt,h1,h2,h3,h4,h5,h6,address,sub,sup,iframe,font" />
                                
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator" 
                                        ControlToValidate="Editor" ValidateEmptyText="True"></asp:CustomValidator>
                                
                                </div>
    </div>
    <div>Mục:</div>
    <div>
        <asp:RadioButtonList ID="RadioButtonListCate" runat="server" 
            RepeatDirection="Horizontal">        
        </asp:RadioButtonList>
    </div>
    <div>Từ khóa:</div>
    <div>
        <asp:TextBox ID="txtKeyword" runat="server" Width="256px"></asp:TextBox></div>
        <i>Phân cách các từ khóa bằng dấu phẩy (,)</i>
    <p>
    <%if (type == "edit")
      { %>
        <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" 
            onclick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Xóa bài viết" 
            onclick="btnDelete_Click" />
    <%}
      else
      { %>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Gửi bài viết" />
            <%} %>
            
            </p>
            <script type="text/javascript">
                function checkEditorNotEmpty(source, args) {
                    var editor = $find("<%=Editor.ClientID%>");
                    var cont = editor.get_text();
                    var isValid = cont.length > 0;
                    args.IsValid = isValid;
                }
            </script>
<!--script type="text/javascript">
   var infoeditor = new TINY.editor.edit('editor', {
        id: 'input',
        width: 584,
        height: 175,
        cssclass: 'te',
        controlclass: 'tecontrol',
        rowclass: 'teheader',
        dividerclass: 'tedivider',
        controls: ['bold', 'italic', 'underline', 'strikethrough', '|', 'subscript', 'superscript', '|',
			  'orderedlist', 'unorderedlist', '|', 'outdent', 'indent', '|', 'leftalign',
			  'centeralign', 'rightalign', 'blockjustify', '|', 'unformat', '|', 'undo', 'redo', 'n',
			  'font', 'size', 'style', '|', 'image', 'hr', 'link', 'unlink', '|', 'cut', 'copy', 'paste', 'print'],
        footer: true,
        fonts: ['Verdana', 'Arial', 'Georgia', 'Trebuchet MS'],
        xhtml: true,
        cssfile: 'style.css',
        bodyid: 'editor',
        footerclass: 'tefooter',
        toggle: { text: 'source', activetext: 'wysiwyg', cssclass: 'toggle' },
        resize: { cssclass: 'resize' }
    });

    function goo() {
        infoeditor.post();
        var textAreaHtml = infoeditor.t.value;
        //alert(textAreaHtml);

        $('#input').text(textAreaHtml);
    }
</script-->
<%}
  else
  { %>
  <p style="color:Red;">Bạn cần đăng nhập trước khi viết bài. <a href="/dang-nhap/" title="Click để đăng nhập" >Đăng nhập</a></p>
<%} %>
</div>
<script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function getContent() {
        var contents = $('iframe html body#editor').html();
        alert(contents);
    }
</script>

