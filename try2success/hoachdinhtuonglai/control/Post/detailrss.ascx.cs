﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.control.Post
{
    public partial class detailrss : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request["url"];            
            LabelContent.Text = AutoFeedDA.GetContent(url);
        }
    }
}