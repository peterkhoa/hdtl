﻿using System;
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
    public partial class manageForum : BasePage
    {
        protected string mess = "",mess2;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Current_user != null && Current_user.ID > 0)
            {
                if (!IsAdmin)
                {
                    if (!IsManager)
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
            Forum f = new Forum();
            if (!string.IsNullOrEmpty(txtForumName.Text.Trim()))
            {
                int cateid;
                int.TryParse(DropDownListCate.SelectedValue, out cateid);
                if (cateid > 0)
                {
                    f.CateID = cateid;
                    f.ForumName = txtForumName.Text.Trim();
                    f.ForumNameSlug = LanguageConvert.GenerateDesc(txtForumName.Text.Trim());
                    f.Order = 0;
                    f.Active = true;
                    ForumDA.Insert(f);
                    mess = "Thêm Forum thành công.";
                    GridView1.DataBind();
                }
            }
        }

        protected void btnAddMod_Click(object sender, EventArgs e)
        {
            ForumInRole fr = new ForumInRole();
            if (!string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                Account ac = AccountDA.SelectByUsername(txtUsername.Text.Trim());
                int forumid;
                int.TryParse(DropDownListForum.SelectedValue,out forumid);
                if (ac != null && forumid > 0)
                {
                    if (ForumInRoleDA.SelectByAccountnForum(ac.ID, forumid) == null)
                    {
                        fr.AccountID = ac.ID;
                        fr.UserName = ac.Username;
                        fr.ForumID = forumid;
                        fr.Active = true;
                        ForumInRoleDA.Insert(fr);
                        mess2 = "Add mod thành công";
                        GridView2.DataBind();
                        return;
                    }
                    mess2 = "Tài khoản này đã là mod của Forum này rồi, hãy chọn Forum khác.";
                }
                else
                    mess2 = "Tài khoản không tồn tại.";
            }
        }
    }
}