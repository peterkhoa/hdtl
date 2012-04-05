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
    public partial class childcommentajax : BaseUserControl
    {
        protected string text;
        protected long commentid;
        protected hoachdinhtuonglai.Data.Core.Comment cm;
        protected Account commentor;
        protected string username;

        public long objectID = 0;
        public long receiverID = 0;

        public Account receivers;

        public string objecttype = "";
        public int objecttypeID = 0;
        public string headline = " đã bình luận ";
        public string target_link = "";
        public string target_object_name = "";
        //public CommentCollection CC;
        protected AccountCollection listReceiver;
        protected Account commenter;
        public long comment_id_parent = 0;
        protected int parent = 0;
        protected Account usercurr;

        protected void Page_Load(object sender, EventArgs e)
        {
            //long.TryParse(Request["commentid"], out commentid);
            //int.TryParse(Request["margin"], out margin);
            text = Request["text"];
            //username = Request["uid"];
            long.TryParse(Request["uid"], out receiverID);
            //string url = Request.Url.ToString().Replace("default.aspx", "");
            string domain = CommentTotal.getDomain();

            objecttype = Request["objecttype"];
            long.TryParse(Request["objectid"], out objectID);

            target_link = Request["targetlink"];
            target_object_name = Request["targetobjectname"];

            //CC = CommentDA.SelectByObjectID(objectID, objecttype);
            objecttypeID = ObjectTypeID.get(objecttype);
            long.TryParse(Request["commentidparent"], out comment_id_parent);
            int.TryParse(Request["par"], out parent);

            if (Current_user.ID != 0)
                commenter = Current_user;
            usercurr = Current_user;
            receivers = AccountDA.SelectByID(receiverID);

            if (objectID > 0 && objecttype != null && text != null && receiverID > 0 && target_link != "" && target_object_name != "" && objecttypeID > 0 && commenter != null)
            {
                commentid = CommentTotal.SendComment(comment_id_parent, text, commenter, receivers, objecttype, objectID, target_link, target_object_name, objecttypeID, Request.UserAgent, "publish", Request.UserHostName, true);
            }
        }
    }
}