using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;
using hoachdinhtuonglai.Data.BL;
namespace hoachdinhtuonglai.control.Comment
{
    public partial class commentajax : BaseUserControl
    {
        public long id;
        public long objectID;
        public int page = 1,pagesize = 20;
        public string objecttype = "";
        public string target_link = "";
        public string target_object_name = "";
        public long receiverID;
        public string receiverUsername;
        protected Account usercurr = Current_user;
        protected CommentCollection listComment = new CommentCollection();
        protected void Page_Load(object sender, EventArgs e)
        {
            listComment = CommentDA.SelectByObjectTypeObjID(objecttype,objectID, page, pagesize);
            listComment = CommentDA.getAllComment(listComment);
        }
    }
}