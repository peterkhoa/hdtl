﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using hoachdinhtuonglai.Data.Core;
using hoachdinhtuonglai.Data.BL;

namespace hoachdinhtuonglai.control.Commons
{
    public partial class login : BaseUserControl
    {
        protected string mess = "";
        protected Account account;
        protected bool is_show = false;
        HttpCookie cookie;
        protected void Page_Load(object sender, EventArgs e)
        {
            cookie = Request.Cookies["current_url"];
            if (Current_user != null && Current_user.ID > 0)
            {
                if (Request["signout"] == "true")
                    Logout();

               

                if (cookie != null && (!cookie.Value.Contains("dang-nhap") & !cookie.Value.Contains("dang-ky")))
                    Response.Redirect(cookie.Value);
                else
                    Response.Redirect("/");
            }


        }

        private void Logout()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/");
        }

        private void Login()
        {
            
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                mess = "Bạn chưa điền Username/Email.";
              
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                mess = "Bạn chưa điền password.";
                
                return;
            }

            account = hoachdinhtuonglai.Data.BL.Unit.checkLogin(txtUsername.Text.Trim(), txtPassword.Text.Trim());
            if (account != null)
            {
                AccountInRoleCollection airc = AccountInRoleDA.SelectByAccountID(account.ID);
                if (airc != null)
                {
                    string role = "";
                    foreach (AccountInRole item in airc)
                    {
                        role += (AccountRoleDA.SelectByID(item.RoleID)).Name + ",";
                    }

                    AuthenUtil.writeCookie(account, role, CheckBoxNho.Checked);
                    if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
                        Response.Redirect(Request["ReturnUrl"]);
                    if (cookie == null || cookie.Value == "/default.aspx?" || !cookie.Value.Contains("dang-nhap") || !cookie.Value.Contains("dang-ky"))
                        Response.Redirect("/?page=home");

                    

                    string url = cookie.Value;
                    HttpContext.Current.Response.Cookies.Remove("current_url");
                    Response.Redirect(url);
                    
                }
                mess = "Đăng nhập thành công!";



            }
            else
                mess = "Tài khoản không hợp lệ, bạn kiểm tra lại.";
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}