using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.BL;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.control.Commons
{
    public partial class sidebar : BaseUserControl
    {
        protected AccountCollection listNewAccount = new AccountCollection();
        protected void Page_Load(object sender, EventArgs e)
        {
            listNewAccount = AccountDA.SelectNewAccount(10);
        }
    }
}