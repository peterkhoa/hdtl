﻿using System;
using System.Data;
using System.Configuration;

using System.Web;

using System.Web.UI;
using System.Linq;

using System.Web.UI.WebControls;


using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Caching;
using System.Collections.Generic;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai
{
    public class xcontrol
    {
        public xcontrol(string xlocation, string xcontrol_path)
        {
            location = xlocation;
            control_path = xcontrol_path;
        }
        private string location;
        private string control_path;

        public string Control_path
        {
            get { return control_path; }
            set { control_path = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
    }


    public class BasePage : System.Web.UI.Page
    {
        //  protected CustomLayout m_Layout;

        public int CacheTimeOut = 60 * 24;
        public bool righ_enable = true;
        public bool show_spotlight = true;
        public string seo_index = "index";
        private static Account _current_user = new Account();
        private static bool _isAdmin = false;

        public static bool IsAdmin
        {
            get { return BasePage._isAdmin; }
            set { BasePage._isAdmin = value; }
        }
        private static bool _isManager = false;

        public static bool IsManager
        {
            get { return BasePage._isManager; }
            set { BasePage._isManager = value; }
        }

        public Account Current_user
        {
            get {

                HttpContext context = HttpContext.Current;
                if (context.User.Identity.IsAuthenticated)// && ( _current_user== null || _current_user.ID==0))
                {
                    if (context.User.IsInRole("admin"))
                    {
                        _isAdmin = true;
                    }
                    if (context.User.IsInRole("manger"))
                    {
                        _isManager = true;
                    }
                    return AccountDA.SelectByUsername(context.User.Identity.Name);

                }
                else
                {
                    _current_user = new Account();
                    _current_user.ID = 0;
                }
                
                
                
                return _current_user; }
            set { _current_user = value; }
        }


        private Dictionary<string, List<xcontrol>> page_list;

        public Dictionary<string, List<xcontrol>> Page_list
        {
            get
            {

                //check caching first
                string cachekey = "xpage_list";
                if (Cache[cachekey] != null)
                {
                    page_list = (Dictionary<string, List<xcontrol>>)Cache[cachekey];
                }
                else
                {



                    page_list = new Dictionary<string, List<xcontrol>>();
                    List<xcontrol> control_list = new List<xcontrol>();



                    /*
                    //page=abs                                                                                                 

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/ABS/absmanager.ascx"));

                    page_list.Add("abs", control_list);





                    //page=abs-admin                                                                                           

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/ABS/ManageCTV.ascx"));

                    page_list.Add("abs-admin", control_list);



                    //page=payment
                    control_list = new List<xcontrol>();
                    control_list.Add(new xcontrol("body", "~/control/payment/TypePayment.ascx"));
                    page_list.Add("payment",control_list);




                    //page=hocvien                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/ABS/ManageLearner.ascx"));

                    page_list.Add("hocvien", control_list);


                    //page=chamsockhachhang
                    control_list = new List<xcontrol>();
                    control_list.Add(new xcontrol("body", "~/control/CRM/chamsockhachhang.ascx"));
                    page_list.Add("chamsockhachhang", control_list);


                    //page=openid_login                                                                                        

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/Mission/BoxUserReg.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/Exp/PositionUser.ascx"));

                    page_list.Add("openid_login", control_list);





                    //page=top                                                                                                 

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/home/top.ascx"));

                    page_list.Add("top", control_list);





                    //page=addlink                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/Parse/AddLink.ascx"));

                    page_list.Add("addlink", control_list);





                    //page=parse_link                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/Parse/LinkParse.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/Exp/PositionUser.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/exp/TopUser.ascx"));

                    page_list.Add("parse_link", control_list);





                    //page=Feedforward                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/kpi/feedforward/feedforward.ascx"));

                    page_list.Add("feedforward", control_list);





                    //page=supervisor_manager                                                                                  

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/kpi/supervisor/supervisorpage.ascx"));

                    page_list.Add("supervisor_manager", control_list);





                    //page=kpi_nhiemvu                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/kpi/NhiemVu.ascx"));

                    page_list.Add("kpi_nhiemvu", control_list);





                    //page=kpi_code                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/kpi/Code.ascx"));

                    page_list.Add("kpi_code", control_list);





                    //page=KPI-Meeting                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/kpi/TeamMeeting.ascx"));

                    page_list.Add("kpi-meeting", control_list);





                    //page=kpi_mission                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/kpi/mission.ascx"));

                    page_list.Add("kpi_mission", control_list);





                    //page=kpi_activate                                                                                        

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/kpi/activate.ascx"));

                    page_list.Add("kpi_activate", control_list);





                    //page=kpi_tutorial                                                                                        

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/kpi/Tutorials.ascx"));

                    page_list.Add("kpi_tutorial", control_list);





                    //page=buy                                                                                                 

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/groupon/Regulation.ascx"));

                    page_list.Add("buy", control_list);





                    //page=chung-suc                                                                                           

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/groupon/homegroupon.ascx"));

                    page_list.Add("chung-suc", control_list);





                    //page=quizresult                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/quiz/quizresult.ascx"));

                    page_list.Add("quizresult", control_list);





                    //page=quizdetail                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/quiz/quizdetail.ascx"));

                    page_list.Add("quizdetail", control_list);





                    //page=quizlist                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/quiz/quizlist.ascx"));

                    page_list.Add("quizlist", control_list);





                    //page=mission                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/mission/loader.ascx"));

                    page_list.Add("mission", control_list);





                    //page=hottopic                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/discussion/DV_Component_Discussion_Detail.ascx"));

                    page_list.Add("hottopic", control_list);





                    //page=kpi                                                                                                 

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("right", "~/control/kpi/nhom/chatbox.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/kpi/kpi.ascx"));

                    page_list.Add("kpi", control_list);





                    //page=ket-noi-edit                                                                                        

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("left", "~/control/connection/categoryside.ascx"));

                    control_list.Add(new xcontrol("inner_body", "~/control/Connection/edit.ascx"));

                    page_list.Add("ket-noi-edit", control_list);





                    //page=ket-noi-yeu-thich                                                                                   

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("left", "~/control/connection/categoryside.ascx"));

                    control_list.Add(new xcontrol("inner_body", "~/control/Connection/followings.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/Connection/clubcomments.ascx"));

                    page_list.Add("ket-noi-yeu-thich", control_list);





                    //page=ket-noi-thao-luan                                                                                   

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("left", "~/control/connection/categoryside.ascx"));

                    control_list.Add(new xcontrol("inner_body", "~/control/discussion/forumlist.ascx"));

                    control_list.Add(new xcontrol("inner_body", "~/control/Connection/clubcomments.ascx"));

                    page_list.Add("ket-noi-thao-luan", control_list);





                    //page=addfriends                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/mypages/addfollowings.ascx"));

                    page_list.Add("addfriends", control_list);





                    //page=following                                                                                           

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/mypages/followings.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/mypages/goal.ascx"));

                    page_list.Add("following", control_list);





                    //page=mine                                                                                                

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/newsfeed/myfeeds.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/sidebar/Hottest_Goal_Post.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/exp/TopUser.ascx"));

                    page_list.Add("mine", control_list);





                    //page=node-list                                                                                           

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/keywords/key.ascx"));

                    page_list.Add("node-list", control_list);





                    //page=tu-khoa                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/keywords/list.ascx"));

                    page_list.Add("tu-khoa", control_list);





                    //page=goal-cat                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("right", "~/control/sidebar/goal_cat_menu.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/sidebar/newest_goals.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/Goal_List/community.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/Goal_List/goal_list_cat.ascx"));

                    page_list.Add("goal-cat", control_list);





                    //page=chia-se-thanh-cong                                                                                  

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/Goal_List/Lastest_goal_post.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/Goal_List/Lastest_goal_post_commented.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/Goal_List/paging.ascx"));

                    page_list.Add("chia-se-thanh-cong", control_list);





                    //page=s                                                                                                   

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/commons/listcontent.ascx"));

                    page_list.Add("s", control_list);





                    //page=cong-dong                                                                                           

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/community/community.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/sidebar/newest_goals.ascx"));

                    page_list.Add("cong-dong", control_list);





                    //page=admin_roles                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/Control/VfCMS_Manage/Manage_Role.ascx"));

                    page_list.Add("admin_roles", control_list);





                    //page=emails                                                                                              

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/control/vfcms_manage/emails.ascx"));

                    page_list.Add("emails", control_list);





                    //page=options                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/control/vfcms_manage/Manage_Options.ascx"));

                    page_list.Add("options", control_list);





                    //page=danh-sach-cau-lac-bo                                                                                

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/club/DV_Club_List.ascx"));

                 //   control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    page_list.Add("danh-sach-cau-lac-bo", control_list);





                    //page=editclub                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/club/DV_Club_Form.ascx"));

                    page_list.Add("editclub", control_list);





                    //page=thong-tin                                                                                           

                    control_list = new List<xcontrol>();

                //    control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/sidebar/Hottest_Goal_Post.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/commons/StaticContentloader.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/Commons/Registration.ascx"));

                    page_list.Add("thong-tin", control_list);





                    //page=write_page                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/control/VfCMS_Manage/Manage_Post.ascx"));

                    control_list.Add(new xcontrol("adminbody", "~/control/VfCMS_Manage/Write.ascx"));

                    page_list.Add("write_page", control_list);





                    //page=eventform                                                                                           

                    control_list = new List<xcontrol>();

                 //   control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/event/DV_Event_Form.ascx"));

                    page_list.Add("eventform", control_list);





                    //page=popupLogin                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/Commons/login.ascx"));

                    page_list.Add("popuplogin", control_list);





                    //page=dien-dan                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/Discussion/deltabox_top.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/discussion/forumlist.ascx"));

                    control_list.Add(new xcontrol("righttop", "~/control/Discussion/Moderator.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/facebook/activity-feed.ascx"));

                    page_list.Add("dien-dan", control_list);





                    //page=delComment                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/commons/deleteComment.ascx"));

                    page_list.Add("delcomment", control_list);





                    //page=supporting                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/mygoals/supportform.ascx"));

                    page_list.Add("supporting", control_list);





                    //page=delPost                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/commons/deletePost.ascx"));

                    page_list.Add("delpost", control_list);





                    //page=Search                                                                                              

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/commons/searchresult.ascx"));

                    page_list.Add("search", control_list);





                    //page=Admin_edit_access                                                                                   

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/Control/VfCMS_Manage/EditAccessType.ascx"));

                    page_list.Add("admin_edit_access", control_list);





                    //page=admin_access                                                                                        

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/Control/VfCMS_Manage/AccessType.ascx"));

                    page_list.Add("admin_access", control_list);





                    //page=admin_groups                                                                                        

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/Control/VfCMS_Manage/Groups.ascx"));

                    page_list.Add("admin_groups", control_list);





                    //page=admin_users                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/Control/VfCMS_Manage/Manage_User.ascx"));

                    page_list.Add("admin_users", control_list);





                    //page=cau-chuyen-kinh-nghiem                                                                              

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/mygoals/story.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/sidebar/Hottest_Goal_Post.ascx"));

                    page_list.Add("cau-chuyen-kinh-nghiem", control_list);





                    //page=muon                                                                                                

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/mygoals/personalgoal.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/mygoals/personalstories.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/mygoals/visitor_message.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/mygoals/supporters.ascx"));

                    page_list.Add("muon", control_list);





                    //page=post-story                                                                                          

                    control_list = new List<xcontrol>();

                   // control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/mygoals/poststory.ascx"));

                    page_list.Add("post-story", control_list);





                    //page=cat                                                                                                 

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/control/VfCMS_Manage/Manage_Cats.ascx"));

                    page_list.Add("cat", control_list);





                    //page=admin_error                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/control/VfCMS_Manage/ErrorLog.ascx"));

                    page_list.Add("admin_error", control_list);





                    //page=Admin                                                                                               

                    control_list = new List<xcontrol>();

                    page_list.Add("admin", control_list);





                    //page=Category                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/Control/VfCMS_Manage/Categories.ascx"));

                    page_list.Add("category", control_list);





                    //page=SubCategory                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("adminbody", "~/Control/VfCMS_Manage/Forums.ascx"));

                    page_list.Add("subcategory", control_list);


                    //page=u                                                                                                   

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/newsfeed/homefeeds.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/mypages/goal.ascx"));

                    page_list.Add("u", control_list);





                    //page=member-detail                                                                                       

                    control_list = new List<xcontrol>();

                 //   control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    page_list.Add("member-detail", control_list);





                    //page=thanh-vien                                                                                          

                    control_list = new List<xcontrol>();

              //      control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/commons/DV_Members.ascx"));

                    page_list.Add("thanh-vien", control_list);





                    //page=cau-lac-bo                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/club/DV_Club_View.ascx"));

                //    control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    page_list.Add("cau-lac-bo", control_list);





                    //page=thanh-vien-clb                                                                                      

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/club/DV_Club_Members.ascx"));

                   // control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    page_list.Add("thanh-vien-clb", control_list);





                    //page=su-kien-clb                                                                                         

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/club/DV_Club_Event.ascx"));

               //     control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    page_list.Add("su-kien-clb", control_list);





                    //page=lien-he-cau-lac-bo                                                                                  

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/club/DV_Club_Contact.ascx"));

                 //   control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    page_list.Add("lien-he-cau-lac-bo", control_list);





                    //page=muc-tieu                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("right", "~/control/sidebar/newest_goals.ascx"));

                 //   control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/mygoals/my-goals.ascx"));

                    page_list.Add("muc-tieu", control_list);





                    //page=toi-muon                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/mygoals/goal.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/sidebar/newest_goals.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/mygoals/GoalComments.ascx"));

                    page_list.Add("toi-muon", control_list);





                    //page=quen-mat-khau                                                                                       

                    control_list = new List<xcontrol>();

                //    control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Event.ascx"));

                    page_list.Add("quen-mat-khau", control_list);




                    //page=ket-noi                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("left", "~/control/connection/categoryside.ascx"));

                    control_list.Add(new xcontrol("inner_body", "~/control/connection/community.ascx"));

                    control_list.Add(new xcontrol("inner_body", "~/control/Connection/clubcomments.ascx"));

                    page_list.Add("ket-noi", control_list);





                    //page=su-kien                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/event/DV_Event_Search.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/event/DV_Event_List.ascx"));

                 //   control_list.Add(new xcontrol("body", "~/control/sidebar/DV_Feature_Event.ascx"));

                    control_list.Add(new xcontrol("body", "~/control/sidebar/newest_goals.ascx"));

                    page_list.Add("su-kien", control_list);





                    //page=post                                                                                                

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("right", "~/control/sidebar/Hottest_Goal_Post.ascx"));

                    page_list.Add("post", control_list);





                    //page=bai-viet                                                                                            

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/discussion/DV_Component_Discussion_Detail.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/facebook/activity-feed.ascx"));

                    page_list.Add("bai-viet", control_list);





                    //page=phat-trien                                                                                          

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("body", "~/control/Component/Com_Forum.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/home/DV_Feature_Discussion.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/home/DV_Feature_Iwant.ascx"));

                    control_list.Add(new xcontrol("right", "~/control/sidebar/DV_Feature_Discussions.ascx"));

                    page_list.Add("phat-trien", control_list);
                    */




                    //page=dang-ky                                                                                             

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("top", "~/control/Commons/Registration.ascx"));

                    page_list.Add("dang-ky", control_list);

                    //page=dang-nhap                                                                                           

                    control_list = new List<xcontrol>();

                    control_list.Add(new xcontrol("top", "~/control/Commons/login.ascx"));

                    page_list.Add("dang-nhap", control_list);



                    //page=home                                                                                                

                    control_list = new List<xcontrol>();

                    //control_list.Add(new xcontrol("body", "~/control/Mission/BoxUserReg.ascx"));

                    //control_list.Add(new xcontrol("body", "~/control/home/sharelink.ascx"));

                    //control_list.Add(new xcontrol("feature", "~/control/Commons/DV_Main_Banner.ascx"));

                    //control_list.Add(new xcontrol("body", "~/control/home/Portal.ascx"));

                    //control_list.Add(new xcontrol("right", "~/control/exp/TopUser.ascx"));

                    //control_list.Add(new xcontrol("right", "~/control/Exp/PositionUser.ascx"));

                    control_list.Add(new xcontrol("sideleft", "~/control/Post/newpost.ascx"));
                    control_list.Add(new xcontrol("top", "~/control/commons/top.ascx"));
                    control_list.Add(new xcontrol("top", "~/control/Post/hotpost.ascx"));
                    control_list.Add(new xcontrol("top", "~/control/Post/feedrss.ascx"));

                    control_list.Add(new xcontrol("foot", "~/control/Sidebar/funny.ascx"));
                    page_list.Add("home", control_list);

                    //page=profile                                                                                             

                    control_list = new List<xcontrol>();

                    //control_list.Add(new xcontrol("right", "~/control/mypages/goal.ascx"));

                    control_list.Add(new xcontrol("sideleft", "~/control/Home/profile.ascx"));

                    page_list.Add("profile", control_list);

                    //page=compose                                                                                             

                    control_list = new List<xcontrol>();

                    //control_list.Add(new xcontrol("right", "~/control/mypages/goal.ascx"));

                    control_list.Add(new xcontrol("sideleft", "~/control/Post/compose.ascx"));

                    page_list.Add("compose", control_list);

                    //page=bai-viet                                                                                             

                    control_list = new List<xcontrol>();

                    //control_list.Add(new xcontrol("right", "~/control/mypages/goal.ascx"));

                    control_list.Add(new xcontrol("sideleft", "~/control/Post/post.ascx"));

                    page_list.Add("bai-viet", control_list);

                    //page=feed
                    control_list = new List<xcontrol>();
                    control_list.Add(new xcontrol("sideleft", "~/control/Post/detailrss.ascx"));
                    page_list.Add("feed", control_list);


                    //page=chia-se                                                                                             

                    control_list = new List<xcontrol>();

                    //control_list.Add(new xcontrol("right", "~/control/mypages/goal.ascx"));

                    control_list.Add(new xcontrol("sideleft", "~/control/Post/sharing.ascx"));

                    page_list.Add("chia-se", control_list);

                    //   Cache.Add(cachekey,page_list,
                    int CacheTimeOut = 60 * 24;


                    //caching o day ne :D
                    Cache.Add(cachekey, page_list, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(CacheTimeOut), CacheItemPriority.High, null);

                }

                return page_list;
            }
            set { page_list = value; }
        }

        public BasePage()
        {
        }



        public string css;
        public string rss = "";
        public string script;

        protected override void OnInit(EventArgs e)
        {
            //string pageRequest = "home";

            //if (Request.QueryString["p"] != null)
            //{
            //    pageRequest = Request.QueryString["p"];
            //}

            if (Request.QueryString["error"] != null)
            {
                showError(Request.QueryString["error"]);
            }

            if (Request.QueryString["notice"] != null)
            {
                showNotice(Request.QueryString["notice"]);
            }

            string url = Request.Url.ToString().ToLower().Replace("default.aspx", "").Replace("defaultnoform.aspx", "");
            string domain = url.Remove(url.IndexOf("/", url.IndexOf("//") + 3)).ToLower().Replace("http://", "").Replace("www", "");

            //add referral log
            if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().ToLower().Contains("google") && !Request.UrlReferrer.ToString().ToLower().Contains(domain))
            {
                //TODO: tam thoi remove de giam toc do         ReferralLogDA.Add(Request.RawUrl.ToLower(), Request.UrlReferrer.ToString(), Request.UserAgent, Request.UserHostName);
            }

         
            base.OnInit(e);
        }

        /// <summary>
        /// In the future, if we support multi-language
        /// just uncomment these following lines.
        /// </summary>
        protected override void InitializeCulture()
        {

            //string lang = string.Empty;//default to the invariant culture

            //HttpCookie cookie = Request.Cookies [ "SelLang" ];



            //if ( cookie != null && cookie.Value != null )
            //{

            //    lang = cookie.Value;

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("VI");

            //    //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);

            //}

            base.InitializeCulture();

        }

        public void showError(string ErrorMsg)
        {
            Literal ErrLb = (Literal)this.FindControl("ErrLb");
            ErrLb.Text = "<div class='err round'><div class=\"contenterr\">" + ErrorMsg + "</div><div class=\"closebut\"><a href=\"#\" onclick=\"$('.err').css('display','none')\">[x]</a></div><div class=\"clear\"></div></div>";
        }
        public void setIndexSEO(string index)
        {
            Literal robots = (Literal)this.FindControl("robots");
            robots.Text = "<meta name=\"robots\" content='" + index + ", follow' />";
        }

        public void setIndexDescription(string description)
        {
            Literal robots = (Literal)this.FindControl("description");
            robots.Text = "<meta name=\"description\" content='" + description + "' />";
        }

        public void showNotice(string noticeMessage)
        {
            Literal ErrLb = (Literal)this.FindControl("ErrLb");
            ErrLb.Text = "<div class='noti round'><div class=\"contenterr\">" + noticeMessage + "</div><div class=\"closebut\"><a href=\"#\" onclick=\"$('.noti').css('display','none')\">[x]</a></div><div class=\"clear\"></div></div>";
        }


        public string errorLink()//cal error link
        {
            string responseAddress = Context.Request.RawUrl;// Request.Url.PathAndQuery;
            if (!responseAddress.Contains("e=yes"))
            {

                if (responseAddress.IndexOf("?") >= 0)//already have query , does not need insert ?
                {
                    responseAddress += "&e=yes";

                }
                else
                {
                    responseAddress += "?e=yes";
                }
            }

            return responseAddress;
        }



        public void AddMetaData(string name, string content)
        {
            // <META NAME="Description" CONTENT="Your descriptive sentence or two goes here.">
            HtmlGenericControl meta = new HtmlGenericControl("meta");
            meta.Attributes.Add("name", name);
            meta.Attributes.Add("content", content);
            //meta.Attributes.Add ( "DESCRIPTION" , description );
            this.Header.Controls.AddAt(0, meta);
        }

        public void AddRss(string name, string link)
        {
            // <META NAME="Description" CONTENT="Your descriptive sentence or two goes here.">
            //    HtmlGenericControl rss = new HtmlGenericControl("link");

            //rss.Attributes.Add("rel", "alternate");
            //rss.Attributes.Add("type", "application/rss+xml");
            //rss.Attributes.Add("title", name);
            //rss.Attributes.Add("href", link);
            //meta.Attributes.Add ( "DESCRIPTION" , description );
            //this.Header.Controls.Add( rss);

            //HtmlGenericControl rss2 = new HtmlGenericControl("link");

            //rss2.Attributes.Add("rel", "alternate");
            //rss2.Attributes.Add("type", "application/atom+xml");
            //rss2.Attributes.Add("title", name);
            //rss2.Attributes.Add("href", link);
            string rss = " <link rel=\"alternate\" type=\"application/rss+xml\" title=\"" + name + "\" href=\"" + link + "\" /> ";
            ((Literal)this.FindControl("HeadTitle")).Text = rss;

            //meta.Attributes.Add ( "DESCRIPTION" , description );
            //this.Header.Controls.Add(rss2);


        }
    }
}
