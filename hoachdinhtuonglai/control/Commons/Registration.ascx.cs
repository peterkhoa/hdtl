﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;
using hoachdinhtuonglai.Data.BL;

namespace hoachdinhtuonglai.control.Commons
{
    public partial class Registration : System.Web.UI.UserControl
    {
        Account ac;
        protected int a, b, kq;
        protected string msg;
        protected Random rand = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
               
            }

            Page.Title = "T2S - Dang ky";
           
        }

        private bool validate()
        {
            if (string.IsNullOrEmpty(txtFirstName.Text.Trim()) || string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {
                msg = "Bạn cần nhập đầy đủ họ & tên.";
                return false;
            }

            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                msg = "Bạn chưa điền Username.";
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                msg = "Bạn chưa điền Email. ";
                return false;
            }

            if (string.IsNullOrEmpty(txtPass.Text.Trim()))
            {
                msg = "Bạn chưa điền Password. ";
                return false;
            }

            int kqua = 0;

            HttpCookie ketqua = Request.Cookies["ketqua"];
            if (ketqua == null)
                ketqua = new HttpCookie("ketqua", "-1");

            string kq_string = Library.Encryptor.Decrypt(ketqua.Value);

            int.TryParse(kq_string, out kqua);

            if (string.IsNullOrEmpty(txtKq.Text.Trim()) || txtKq.Text.Trim() != kqua.ToString())
            {
                msg = "Câu trả lời chưa đúng. Hãy trả lời lại!";
               
                return false;
            }

            
            string st = txtUsername.Text.Trim();
            
            if (!SendMail.CheckKyTuDacBiet(st))
                {
                    msg = "Username không được chứa các ký tự đặc biệt hoặc gõ có dấu!";
                    return false;
                }
            

            st = txtPass.Text.Trim();
            
              if (!SendMail.CheckKyTuCoDau(st))
                {
                    msg = "Password không được chứa các ký tự đặc biệt hoặc gõ có dấu!";
                    return false;
                }
           
            st = txtEmail.Text.Trim();
            if (!SendMail.CheckEmailAddress(st))
            {
                msg = "Email không hợp lệ!";
                return false;
            }


            st = st.Substring(0, st.LastIndexOf('@'));
            
                if (!SendMail.CheckKyTuDacBiet(st))
                {
                    msg = "Email không được chứa các ký tự đặc biệt hoặc gõ có dấu!";
                    return false;
                }
            
            return true;
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {

            if (validate())
            {
                ac = AccountDA.SelectByEmail(txtEmail.Text.Trim());
                if (ac == null)
                {
                    ac = AccountDA.SelectByUsername(txtUsername.Text.Trim());
                    if (ac == null)
                    {
                        ac = new Account();
                        ac.Username = txtUsername.Text.Trim();
                        
                        string pass = Library.Encryptor.Encrypt(txtPass.Text.Trim());
                        ac.Password = pass;
                        ac.Email = txtEmail.Text.Trim();
                        ac.FullName = txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim();
                        ac.FirstName = txtFirstName.Text.Trim();
                        ac.LastName = txtLastName.Text.Trim();
                        ac.Birthday = DateTime.Now;
                        ac.LastLogin = DateTime.Now;
                        
                        if (AccountDA.Insert(ac) > 0)
                        {
                            //Response.Write(ac.ID);
                            AccountRole ar = AccountRoleDA.SelectByRoleName("member");
                            AccountInRole air = new AccountInRole();
                            if (ar != null)
                            {
                                air.AccountID = ac.ID;
                                air.RoleName = ar.Name;
                                air.RoleID = ar.ID;
                                AccountInRoleDA.Insert(air);

                                AccountInRoleCollection airc = AccountInRoleDA.SelectByAccountID(ac.ID);
                                if (airc != null)
                                {
                                    string role = "";
                                    foreach (AccountInRole item in airc)
                                    {
                                        role += (AccountRoleDA.SelectByID(item.RoleID)).Name + ",";
                                    }

                                    AuthenUtil.writeCookie(ac, role, false);
                                   // try
                                    {
                                        SendMail.sendMail(SendMail.contentReg, SendMail.SubjectMailReg, "admin@try2success.com", "Try2Success Admin", ac.Email, ac.FullName);
                                    }
                                    //catch { }
                                    Response.Redirect(LinkBuilder.getLinkProfile(ac.Username));
                                    //Response.Redirect("/");
                                }

                                
                            }
                            Response.Redirect("/dang-nhap/");
                        }
                    }
                    else
                    {
                        msg = "Username này đã có người dùng, hãy chọn username khác bạn nhé!";
                        return;
                    }
                }
                else
                {
                    msg = "Email này đã có người dùng, hãy chọn email khác bạn nhé!";
                    return;
                }

            }
            else
            {
                //msg = "Có lỗi!";
                return;
            }

          

           
        }
    }
}