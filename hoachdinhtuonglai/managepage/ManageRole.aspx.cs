using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.BL;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.managepage
{
    public partial class ManageRole : BasePage
    {
        protected string mess = "";
        protected int id;
        protected bool allow = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Current_user != null && Current_user.ID > 0)
            {
                if (!IsAdmin)
                {

                    mess = "Bạn cần đăng nhập quyền Admin/Manager để xem trang này.";
                    allow = false;
                    return;


                }
                else
                    allow = true;

            }
            else
            {
                mess = "Bạn cần đăng nhập quyền Admin/Manager để xem trang này. <a href='/dang-nhap/'>Đăng nhập</a>";
                allow = false;
                return;
            }
            allow = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                Account ac = AccountDA.SelectByUsername(txtUsername.Text.Trim().ToLower());
                if (ac != null)
                {
                    int roleid;
                    int.TryParse(DropDownList1.SelectedValue, out roleid);
                    AccountInRole air = AccountInRoleDA.SelectByRoleIDnAccountID(roleid, ac.ID);
                    if (air != null)
                    {
                        mess = "Bạn đã cấp quyền " + DropDownList1.SelectedItem.Text + " cho user này rồi";
                        return;
                    }
                    else if (roleid > 0)
                    {
                        air = new AccountInRole();
                        air.AccountID = ac.ID;
                        air.RoleID = roleid;
                        air.RoleName = DropDownList1.SelectedItem.Text;
                        AccountInRoleDA.Insert(air);
                        mess = "Cấp quyền thành công!";
                    }
                }
            }
        }
    }
}