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
    public partial class hotpost : BaseUserControl
    {
        protected PostCollection listPostHot = new PostCollection();
        protected void Page_Load(object sender, EventArgs e)
        {
            listPostHot = PostDA.TopPostHotView(8);
        }
    }
}