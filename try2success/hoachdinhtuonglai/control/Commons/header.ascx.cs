using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;
using hoachdinhtuonglai.Data.BL;

namespace hoachdinhtuonglai.control.Commons
{
    public partial class header : BaseUserControl
    {
        protected bool is_show = false;
        
        protected string display;
        protected string page = "", current_url="";
        protected Account account;
        protected Page<ActionLog> actionLog;
        protected long countActionLog = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Current_user != null && Current_user.ID > 0){
                account = Current_user;
               
                countActionLog = ActionLogDA.CountUnreadNotification(account.ID);
            }

            page = Request["page"];            

                display = "none";
           
        }
    }
}