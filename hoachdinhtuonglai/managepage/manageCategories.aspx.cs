using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library;
using hoachdinhtuonglai.Data.BL;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.managepage
{
    public partial class manageCategories : System.Web.UI.Page
    {
        protected string mess;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.User.IsInRole("admin"))
            {
                if (!Page.User.IsInRole("manager"))
                {
                    //if (!IsManager)
                    {
                        mess = "Bạn cần đăng nhập quyền Admin/Manager để xem trang này.";
                        return;
                    }
                }

            }
            else
            {
                mess = "Bạn cần đăng nhập quyền Admin/Manager để xem trang này. <a href='/dang-nhap/'>Đăng nhập</a>";
                return;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCateName.Text.Trim()))
            {
                Category c = new Category();
                c.CateName = txtCateName.Text.Trim();
                c.CateSlugName = LanguageConvert.GenerateDesc(txtCateName.Text.Trim());
                c.Active = true;
                c.Order = 0;
                CategoryDA.Insert(c);
                mess = "Thêm mục thành công.";
                GridView1.DataBind();
            }
        }
    }
}