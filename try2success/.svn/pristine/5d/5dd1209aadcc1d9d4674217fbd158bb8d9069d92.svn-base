using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hoachdinhtuonglai
{
    public partial class ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["xcontrol"]))
            {
                string control = Request["xcontrol"].Replace("-", "/");
                ajaxPh.Controls.Add(LoadControl("~/control/" + control + ".ascx"));


            }
        }
    }
}