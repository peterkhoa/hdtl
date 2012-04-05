using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;
using hoachdinhtuonglai.Data.BL;

namespace hoachdinhtuonglai.control.Post
{
    public partial class newpost1 : BaseUserControl
    {
        protected PostCollection listPost = new PostCollection();
        protected CommentCollection listComment = new CommentCollection();
        protected int page = 0;
        protected int pagesize = 20;
        protected bool is_owner = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //listPost = PostDA.SelectByPageSize(page,pagesize);
            listPost = PostDA.SelectListPostNewest(20);


        }
    }
}