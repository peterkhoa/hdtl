﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;
//using hoachdinhtuonglai.Data.BL;

namespace hoachdinhtuonglai
{
    public partial class Default : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.setIndexSEO("index");
            if (Request.Url.ToString().Contains("http://try2success.com") || Request.Url.ToString().Contains("https://try2success.com"))
                Response.Redirect("http://www.try2success.com");
            if (Request["page"] != null && Request["page"] != "")
            {
                //string ctrl = "/control/commons/Registration.ascx";
                //sideleft.Controls.Add(LoadControl(ctrl));

            }

            //sideleft.Controls.Add(LoadControl("/control/Post/newpost.ascx"));

            LoadControl();
            //headermain.Controls.Add(LoadControl("/control/Commons/LS_Foot.ascx"));
            //footermain.Controls.Add(LoadControl("/control/Commons/header.ascx"));
            HttpCookie cookie = Request.Cookies["current_url"];
            if (cookie != null && cookie.Value != "/default.aspx?" && !cookie.Value.Contains("dang-nhap") && !cookie.Value.Contains("dang-ky"))
            {
                //cookie = new HttpCookie("current_url", HttpContext.Current.Request.RawUrl);
                //cookie.Expires = DateTime.Now.AddSeconds(30);
                //Response.Cookies.Add(cookie);
            }
            else
            {
                cookie = new HttpCookie("current_url", HttpContext.Current.Request.RawUrl);
                cookie.Expires = DateTime.Now.AddSeconds(30);
                Response.Cookies.Add(cookie);
            }
        }

        private void LoadControl()
        {
            string pagename = "";
            if (!string.IsNullOrEmpty(Request["page"]))
                pagename = Request["page"];
            else
                pagename = "home";

            includeControl();
            InitAutoControl(pagename);
        }

        private void includeControl()
        {
            string pagename = "";
            if (!string.IsNullOrEmpty(Request["page"]))
                pagename = Request["page"];
            else
                pagename = "home";

            if (pagename == "home")
            {
                sideright.Controls.Add(LoadControl("~/control/Commons/sidebar.ascx"));
            }
            else
            {
                sideright.Controls.Add(LoadControl("~/control/Sidebar/sidebarright.ascx"));
            }

            List<xcontrol> listcontrol = new List<xcontrol>();
            Page_list.TryGetValue(pagename, out listcontrol);
            if (listcontrol != null)
            {
                foreach (xcontrol control in listcontrol)
                {
                    if (control.Location == top.ID)
                        top.Controls.Add(LoadControl(control.Control_path));
                    if (control.Location == sideleft.ID)
                        sideleft.Controls.Add(LoadControl(control.Control_path));

                    if (control.Location == sideright.ID)
                        sideright.Controls.Add(LoadControl(control.Control_path));

                    if (control.Location == foot.ID)
                        foot.Controls.Add(LoadControl(control.Control_path));
                    if (control.Location == body.ID)
                        body.Controls.Add(LoadControl(control.Control_path));

                }
            }
        }

        private void InitAutoControl(string pagename)
        {
            if (pagename != "")
            {
                headermain.Controls.Add(LoadControl("~/control/commons/header.ascx"));
                footermain.Controls.Add(LoadControl("~/control/commons/LS_Foot.ascx"));
            }
        }

        
    }
}
