using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.BL;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.control.Sidebar
{
    public partial class funny : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string urlfeed = AutoFeedDA.url_cuoi;
            AutoFeedCollection listFeed = AutoFeedDA.SelectByUrlFeed(urlfeed);
            ListFeedRepeater.DataSource = listFeed;
            ListFeedRepeater.DataBind();
        }
    }
}