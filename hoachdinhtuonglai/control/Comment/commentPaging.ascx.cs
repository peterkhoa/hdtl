﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;
using hoachdinhtuonglai.Data.BL;

namespace hoachdinhtuonglai.control.Comment
{
    public partial class commentPaging : BaseUserControl
    {
        public CommentCollection CC;

        public long objectID = 0;
        public long receiverID = 0;

        public Account receiver;

        public string objecttype = "";
        public int objecttypeID = 0;

        //public static Tag tag;
        public int pagesize = 20;
        public int page = 1;

        public string headline = " đã bình luận ";
        public string target_link = "/";
        public string target_object_name = "";
        protected CommentCollection commentParent = new CommentCollection();
        protected int number = 0;
        protected Account usercurr;
        public long comment_id_parent = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.ToString().Replace("default.aspx", "");
            string domain = url.Remove(url.IndexOf("/", url.IndexOf("//") + 3));

            target_link = domain + target_link;


            usercurr = Current_user;
            int.TryParse(Request.QueryString["cpg"], out page);
            objecttype = Request["objecttype"];
            long.TryParse(Request["objectid"], out objectID);

            //target_link = Request["targetlink"];
            //target_object_name = Request["targetobjectname"];

            objecttypeID = ObjectTypeID.get(objecttype);

            CC = CommentDA.SelectByObjectID(objectID,  pagesize, page);
            CC = CommentDA.getAllComment(CC);



            receiver = Current_user;
        }
    }
}