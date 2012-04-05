using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.control.Post
{
    public partial class feedrss : System.Web.UI.UserControl
    {
        protected AutoFeedCollection listFeed = new AutoFeedCollection();
        protected void Page_Load(object sender, EventArgs e)
        {
            string urlfeed = AutoFeedDA.url_trangchu;
            listFeed = AutoFeedDA.SelectByUrlFeed(urlfeed);
            ListFeedRepeater.DataSource = listFeed;
            ListFeedRepeater.DataBind();
        }
    }
}