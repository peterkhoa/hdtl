using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;


namespace hoachdinhtuonglai.control
{
    public partial class notification : BasePage
    {
        protected ActionLogList listAction = new ActionLogList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Current_user != null && Current_user.ID > 0)
            {
                listAction = ActionLogDA.SelectAllUnreadNotification(Current_user.ID);
                RepeaterNotification.DataSource = listAction;
                RepeaterNotification.DataBind();
                ActionLogDA.UpdateIsViewed(Current_user.ID);
            }
        }
    }
}