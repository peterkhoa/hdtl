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
    public partial class sharing : BaseUserControl
    {
        protected PostCollection listPost = new PostCollection();
        protected Account account;
        protected void Page_Load(object sender, EventArgs e)
        {
            listPost = PostDA.SelectByCateID(CategoriesEnums.idCate_sharing);
            if (Current_user != null && Current_user.ID > 0)
                account = Current_user;
        }
    }
}