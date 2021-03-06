﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Security;

using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Library;
using hoachdinhtuonglai.Data.BL;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.Data.BL
{
    public static class AuthenUtil
    {
        public static void writeCookie(Account acc, string role, bool permanance)
        {
            HttpCookie cookie;

            string encryptedStr;

            //     HttpCookie data;

            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, acc.ID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), permanance, role);
            FormsAuthenticationTicket ticket;

            if (permanance)// if checkbox is checked
            {
                ticket = new FormsAuthenticationTicket(1, acc.Username, DateTime.Now, DateTime.Now.AddDays(30), permanance, role);
            }
            else
            {
                ticket = new FormsAuthenticationTicket(1, acc.Username, DateTime.Now, DateTime.Now.AddMinutes(30), permanance, role);
            }

            encryptedStr = FormsAuthentication.Encrypt(ticket);
            cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedStr);
            //   da-ta = new HttpCookie("uid", Convert.ToString(uid));//for storing account ID
            //   da-ta.Expires = DateTime.Now.AddDays(1000);

            if (permanance)// if checkbox is checked
            {
                cookie.Expires = DateTime.Now.AddDays(30);

            }


            HttpContext.Current.Response.Cookies.Add(cookie);//return cookie
            //  context.Response.Cookies.Add(data);

            //after login

            ////add online user list
            // OnlineUsers.AddOnlineUser(user.ID, user.Username);
            //context.Response.Cookies["BoardID"].Expires.AddYears(10);
            //context.Response.Cookies["BoardID"].Value = acc.BoardID.Value.ToString();


            //loging in
            //context.

        }

        public static void HandleRelyingPartyRequest()
        {
            //try
            {
                using (var openid = new OpenIdRelyingParty())
                {
                    var request = openid.CreateRequest(HttpContext.Current.Request.Form["openid_identifier"]);
                    var fetchRequest = new FetchRequest();

                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.First);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.Last);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.HomeAddress.Country);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.FullName);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.BirthDate.DayOfMonth);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.BirthDate.Month);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.BirthDate.Year);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Person.Gender);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Media.Images.Aspect11);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Phone.Mobile);
                    fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.HomeAddress.City);

                    request.AddExtension(fetchRequest);
                    //       HttpContext.Current.rs.Write(WellKnownAttributes.Contact.Email.ToString());
                    request.RedirectToProvider();

                }

            }
            //catch (ProtocolException pExp)
            //{
            //    Response.Write("error");
            //}
            //catch (WebException ex)
            //{
            //    Response.Write("error");
            //}


        }

        public static void HandleOpenIdProviderResponse(ref string reload)
        {
            string UserName = " ";
            string Email = " ";
            string FirstName = " ";
            string LastName = " ";
            string City = " ";
            string Phone = " ";
            string FullName = "";
            string day = "";
            string month = "";
            string year = "";
            string gender = "";
            string img = "";

            using (var openid = new OpenIdAjaxRelyingParty())
            {

                var response = openid.GetResponse();

                if (response == null) return;
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        var fetchResponse = response.GetExtension<FetchResponse>();
                        //bo sesssion     HttpContext.Current.S-ession["FetchRespose"] = fetchResponse;
                        Email = fetchResponse.GetAttributeValue(WellKnownAttributes.Contact.Email);
                        Phone = fetchResponse.GetAttributeValue(WellKnownAttributes.Contact.Phone.Mobile);
                        FirstName = fetchResponse.GetAttributeValue(WellKnownAttributes.Name.First);
                        FullName = fetchResponse.GetAttributeValue(WellKnownAttributes.Name.FullName);
                        LastName = fetchResponse.GetAttributeValue(WellKnownAttributes.Name.Last);
                        day = fetchResponse.GetAttributeValue(WellKnownAttributes.BirthDate.DayOfMonth);
                        month = fetchResponse.GetAttributeValue(WellKnownAttributes.BirthDate.Month);
                        year = fetchResponse.GetAttributeValue(WellKnownAttributes.BirthDate.Year);
                        City = fetchResponse.GetAttributeValue(WellKnownAttributes.Contact.HomeAddress.City);
                        gender = fetchResponse.GetAttributeValue(WellKnownAttributes.Person.Gender);
                        img = fetchResponse.GetAttributeValue(WellKnownAttributes.Media.Images.Aspect11);
                        // FormsAuthentication.RedirectFromLoginPage(response.ClaimedIdentifier, false);

                        Account account = new Account();

                        Account acctemp = AccountDA.SelectByEmail(Email);
                        int random = 0;
                        if (acctemp == null)
                        {
                            account.Username = Email.Substring(0, Email.IndexOf('@')).Trim();
                            if (AccountDA.SelectByUsername(account.Username) != null)
                            {
                                Random r = new Random(DateTime.Now.Second);
                                random = r.Next(100, 999);
                                account.Username = account.Username + random.ToString();

                            }
                            account.Email = Email.Trim();
                            //account.Password = hashedPassword;
                            int pas = 0;
                            List<int> list = new List<int>();
                            string ps = "";
                            Random ra = new Random(DateTime.Now.Second);
                            pas = ra.Next(100000000, 999999999);
                            for (int i = 65; i <= 90; i++)
                            {
                                list.Add(i);
                            }

                            for (int i = 97; i <= 122; i++)
                            {
                                list.Add(i);

                            }
                            Random rs = new Random(DateTime.Now.Second);
                            for (int i = 0; i < 3; i++)
                            {

                                ps += (char)list[rs.Next(0, list.Count)];
                            }

                            string password = pas.ToString() + ps;
                            account.Password = Encryptor.Encrypt(password);


                            if (Phone != null) account.PhoneNumber = Phone;// Phonetxt.Text;


                            if (City != null) account.City = City;


                            if (day != null && month != null && year != null)
                            {
                                DateTime date = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
                                account.Birthday = date;

                            }
                            if (gender != null) account.Gender = gender;

                            if (img != null) account.Avatar = img;



                            account.Company = "Bí Mật";// txtOrganization.Text;
                            account.School = "Bí Mật";// txtSchool.Text;
                            if (FirstName != null && LastName != null)
                            {
                                account.FullName = FirstName + " " + LastName;//txtFirstName.Text;
                                //account.LastName = LastName;//txtLastName.Text;
                            }
                            else
                            {
                                if (FullName != null)
                                {
                                   
                                    account.LastName = FullName;
                                }
                                else
                                {
                                    account.FirstName = "";
                                    account.LastName = "";
                                }

                            }

                            account.IP = HttpContext.Current.Request.UserHostAddress.ToString();
                            account.Active = false;

                            string uid = UserName.Trim();

                            long accountID = AccountDA.Insert(account);

                            //dat role default la member luon
                            AccountInRole air = new AccountInRole();
                            air.AccountID = accountID;
                            air.RoleID = AccountRoleDA.SelectByRoleName(RolesAccess.Member).ID;
                            AccountInRoleDA.Insert(air);

                            //success --> notice
                            //  HttpContext.Current.S-ession["xusername"] = account.Username.ToUpper();
                            //MultiView1.SetActiveView(View2);                            

                            //dang ky xong --> cho tu dong dang nhap luon? chac la minh se cho chay qua trang dang nhap truoc hen


                            // HttpContext.Current.S-ession["first_login"] = true;
                            HttpCookie fist_login = new HttpCookie("first_login", "true");

                            HttpContext.Current.Response.Cookies.Add(fist_login);

                            HttpCookie new_user = new HttpCookie("new_user", account.Username);
                            HttpContext.Current.Response.Cookies.Add(new_user);
                            //   Response.Redirect("http://bit.ly/dang_nhap_pro");//LinkBuilder.SignupSuccess());
                            string goal = HttpContext.Current.Request.Form["txtgoal"];
                            

                            //Send Mail To User After registration


                            string content = "<div style='font-family:arial;color:#333333;'><strong>Gửi bạn " + account.Username + ",</strong>"
                                + "<br/>"
                                 + "<br/>"
                                + "Chào mừng bạn đã đăng ký tài khoản Try2Success thành công."
                                + "<br/>"
                                //+ "Hiện tại tài khoản của bạn chưa được kich hoạt, để kích hoạt tài khoản, bạn soạn tin SMS với cú pháp: <b>REG Try2Success " + uid + " GỬI 8377</b><br />"
                                //+ "Ví dụ:Username của bạn là vitcon thì bạn soạn: REG Try2Success vitcon sau đó gửi đến số 8377. Chúc bạn thành công! <br />"
                                 + "<br/>"
                                + "Bạn hãy thường xuyên ghé thăm website <strong> <a style='text-decoration:underline;' href='www.try2sucess.com'>www.try2sucess.com</a> </strong> học hỏi chia sẻ để phát triển bản thân mỗi ngày nhé!"

                                + "<br/>"
                                 + "<br/>"
                                + "<strong>Để truy cập website bạn có thể sử dụng một trong hai cách sau:</strong>"
                                + "<br/>"
                                + "<br/>"
                                + "<span style='color:#006600;'><strong>1. Truy cập nhanh, thuận tiện, không cần mật khẩu:</strong></span> Ấn vào hình Google/Yahoo trong cửa sổ đăng nhập. Sau đó ấn <span style='color:#006600;'>\"Đồng ý/Allow\"</span>"
                                + "<br/>"
                                + "<br/>"
                                + "<span style='color:#006600;'><strong>2. Truy cập bằng tài khoản Try2Success ID</strong></span>"
                                + "<ul style='list-style-type:circle; margin-left:20px;'>"
                                + "<li>Tài khoản:<strong><a style='text-decoration:underline;' href='mailto:" + account.Email + "'>" + account.Email + "</a></strong></li>"
                                + "<li>Mật khẩu: <strong style='color:#cc0000'>" + password + "</strong></li>"

                                + "</ul>"
                                + "<div style='border-width:1px;border-style:solid;background-color:rgb(105, 167, 78);padding:4px 10px 5px;border-top:1px solid rgb(149, 191, 130);width:160px;margin-left:60px;'> <a target='_blank' href='" + LinkBuilder.getLinkProfile(account) + "/setting' style='color:rgb(255, 255, 255);text-decoration:none;font-weight:bold;font-size:13px'>Thay mật khẩu mặc định</a></div>"
                                // + "<table cellspacing='0' cellpadding='0' style='border-collapse: collapse;'><tbody><tr><td style='padding:4px 10px 5px;border-top:1px solid rgb(149, 191, 130)'><a target='_blank' href='" + account.getAuthorLink() + "/setting' style='color:rgb(255, 255, 255);text-decoration:none;font-weight:bold;font-size:13px'>Để đổi mật khẩu</a></td></tr></tbody></table>"
                                + "<br/>"
                                + "<strong>Xin cảm ơn và chúc một ngày vui vẻ</strong>"

                            + "</div>";

                            AccountInRoleCollection rl = AccountInRoleDA.SelectByAccountID(account.ID);
                            string role = "";
                            foreach (AccountInRole r in rl)
                            {
                                role += (AccountRoleDA.SelectByID(r.RoleID)).Name + ",";
                            }


                            //write authetication information into cookies
                            // trong hàm này la tao mới user tai login bang openid
                            writeCookie(account, role, true);
                            reload = "<script type='text/javascript'>window.location.href=window.location.href</script>";

                            try
                            {



                                SendMail.sendMail(content, "[Try2Success] Chào mừng bạn đến với gia đình Try2Success", "admin@Try2Success.com", "Try2Success Admin", account.Email, account.FirstName + account.LastName);
                            }
                            catch (Exception)
                            {

                            }




                            // Response.Redirect("http://www.Try2Success.net"+Request.RawUrl);




                        }
                        else
                        {
                            //  Role//doan dang nhap o dau 
                            AccountInRoleCollection rl = AccountInRoleDA.SelectByAccountID(acctemp.ID);
                            string role = "";
                            foreach (AccountInRole r in rl)
                            {
                                role += (AccountRoleDA.SelectByID(r.RoleID)).Name + ",";
                            }


                            //write authetication information into cookies
                            writeCookie(acctemp, role, true);
                            reload = "<script type='text/javascript'>window.location.href=window.location.href</script>";
                            // Response.Redirect("http://www.Try2Success.net"+Request.RawUrl);

                            //  AfterLoggedIn(acctemp);

                        }
                        break;

                }



            }


        }

        public static void AfterLoggedIn(Account user)
        {
            // HttpContext.Current.S-ession.Remove("new_user");
            HttpContext.Current.Response.Cookies.Remove("new_user");

            //  HttpContext.Current.S-ession["AccountID"] = user.ID;
            //  HttpContext.Current.S-ession["Account"] = user;

            ////add online user list
            //OnlineUsers.AddOnlineUser ( user.ID , user.Username );
            //Response.Cookies["BoardID"].Expires.AddYears(10);
            //Response.Cookies["BoardID"].Value = user.BoardID.Value.ToString() ;

            // Redirect to home page

            if (HttpContext.Current.Request["ReturnURL"] != null)
            {
                HttpCookie return_url = new HttpCookie("return_url", HttpContext.Current.Request["ReturnURL"]);
                HttpContext.Current.Response.Cookies.Add(return_url);
                //     HttpContext.Current.S-ession["ReturnURL"] = HttpContext.Current.Request["ReturnURL"];
            }
            //if (Request["ReturnURL"] != "djo")
            //    S-ession["ReturnURL"] = "http://journey.Try2Success.net";

            if (HttpContext.Current.Request.Cookies["return_url"] == null || string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["return_url"].Value))// S-ession["ReturnURL"] == null)
            {




                HttpCookie return_url = new HttpCookie("return_url", "/");
                HttpContext.Current.Response.Cookies.Add(return_url);



            }

            //kiem tra xem la user la hoan thanh mission 1 chua, neu chua thi minh se redirect qua mission 1?
            // chac la minh dung s-ession de check xem day la lan dang nhap dau tien hen

            HttpCookie first_login = HttpContext.Current.Request.Cookies["first_login"];
            if (first_login != null)
            {
                //string mission_link = MissionDA.Select1stMission().link;

               
                //HttpContext.Current.Response.Redirect(LinkBuilder.mission_link(mission_link));
            }
            else
            {
                HttpCookie return_url = HttpContext.Current.Request.Cookies["return_url"];

                if (return_url == null)
                {
                    HttpContext.Current.Response.Redirect("/");
                }
                else
                    HttpContext.Current.Response.Redirect(return_url.Value);
            }
        }

        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        public static void LoginDeltaID(string UserName, string Password, string Email, string Name)
        {

            string hashedPassword = Password;
            hashedPassword = Encryptor.Encrypt(Password);


            // add account to system
            Account account = new Account();



            // Static method:




            account.Username = UserName.Trim();
            account.Email = Email.Trim();
            account.Password = hashedPassword;
            account.PhoneNumber = " ";// Phonetxt.Text;
            account.Company = "Bí Mật";// txtOrganization.Text;
            account.School = "Bí Mật";// txtSchool.Text;
            account.FirstName = " ";//txtFirstName.Text;
            account.LastName = Name;//txtLastName.Text;
            account.IP = HttpContext.Current.Request.UserHostAddress.ToString();
            account.Active = false;
            string uid = UserName.Trim();

            long accountID = AccountDA.Insert(account);

            //dat role default la member luon
            AccountInRole air = new AccountInRole();
            air.AccountID = accountID;
            air.RoleID = AccountRoleDA.SelectByRoleName(RolesAccess.Member).ID;
            AccountInRoleDA.Insert(air);

            //success --> notice
            //  HttpContext.Current.S-ession["xusername"] = account.Username.ToUpper();
            //MultiView1.SetActiveView(View2);


            //dang ky xong --> cho tu dong dang nhap luon? chac la minh se cho chay qua trang dang nhap truoc hen

            HttpCookie fist_login = new HttpCookie("first_login", "true");

            HttpContext.Current.Response.Cookies.Add(fist_login);

            HttpCookie new_user = new HttpCookie("new_user", account.Username);
            HttpContext.Current.Response.Cookies.Add(new_user);

            //HttpContext.Current.S-ession["first_login"] = true;
            //HttpContext.Current.S-ession["new_user"] = account.Username;
            //   Response.Redirect("http://bit.ly/dang_nhap_pro");//LinkBuilder.SignupSuccess());
            string goal = HttpContext.Current.Request.Form["txtgoal"];
            if (!string.IsNullOrEmpty(goal) && goal != "Tôi muốn...")
            {
               // Addgoal.Add(account, goal, DateTime.Now.ToString());
            }

            HttpCookie val1 = HttpContext.Current.Request.Cookies["val1"];

            if (val1 != null)
            {
                //Addgoal.Add(account, val1.Value, DateTime.Now.ToString());

            }

            HttpCookie val2 = HttpContext.Current.Request.Cookies["val2"];

            if (val2 != null)
            {
                //Addgoal.Add(account, val2.Value, DateTime.Now.ToString());

            }
            HttpCookie val3 = HttpContext.Current.Request.Cookies["val3"];
            if (val3 != null)
            {
                //Addgoal.Add(account, val3.Value, DateTime.Now.ToString());

            }

            HttpContext.Current.Response.Cookies.Remove("val1");//.S-ession.Remove("val1");
            HttpContext.Current.Response.Cookies.Remove("val2");//.S-ession.Remove("val2");
            HttpContext.Current.Response.Cookies.Remove("val3");//.S-ession.Remove("val3");


            //Response.Redirect(LinkBuilder.SignupSuccess());

            //Send Mail To User After registration


            string content = "<strong> Hey Delta!</strong>"
                + "<br/>"
                 + "<br/>"
                + "Chào mừng bạn đã đến với gia đình Try2Success!"
                + "<br/>"
                //+ "Hiện tại tài khoản của bạn chưa được kich hoạt, để kích hoạt tài khoản, bạn soạn tin SMS với cú pháp: <b>REG Try2Success " + uid + " GỬI 8377</b><br />"
                //+ "Ví dụ:Username của bạn là vitcon thì bạn soạn: REG Try2Success vitcon sau đó gửi đến số 8377. Chúc bạn thành công! <br />"
                 + "<br/>"
                + "Mình là Trần Đăng Khoa - một thành viên trong ban admin website."

                + "<br/>"
                 + "<br/>"
                + "<strong>Try2Success là một website dành cho các bạn trẻ khao khát được trang bị kỹ năng sống, là nơi khuyến khích các bạn trẻ viết ra ước mơ, tâm sự của mình thông qua việc:</strong>"
                + "<br/>"

                + "<ul style='list-style-type:circle; margin-left:20px;'>"
                + "<li><strong>Viết ra mục tiêu, ước mơ của mình</strong></li>"
                + "<li><strong>Viết nhật ký nhìn nhận lại mình thường xuyên </strong>( nội dung có thể là về công việc, học tập, bạn bè, gia đình ..v... những gì bạn muốn viết và điều đó tốt cho sự phát triển cá nhân của bạn ) </li>"
                + "<li><strong>Chia sẻ trải nghiệm</strong> quý báu do cá nhân tự đúc kết có được trong quá trình thực hiện mục tiêu </li>"
                + "<li><strong>Làm quen, kết bạn</strong></li>"
                + "</ul>"

                + "Woa thật là thú vị đúng không nè, mình đang tự hỏi tại sao chúng ta không cùng trò truyện với nhau ngay tại website Try2Success nhỉ"
                + "<br/>"
                 + "<br/>"
                + "Nick hiện tại của mình trên Try2Success là peterkhoa, địa chỉ " + "http://www.try2success.net/t2s/peterkhoa" + ". Bạn add mình nhé . Ah, bạn ơi nick  admin@Try2Success.com mình chỉ dùng để thông báo chứ không dùng để nhận email hồi âm bạn thông cảm nhé."
                + "<br/>"
                 + "<br/>"
                + "Chúc bạn có một tuần mới thật nhiều niềm vui và may mắn! "
                + "<br/>"
                 + "<br/>"
                + "Thân mến,"
                + "<br/>"
                + "Ban quản lý website Try2Success";



            try
            {



                SendMail.sendMail(content, "[Try2Success] Chào mừng bạn đến với gia đình Try2Success", "admin@Try2Success.com", "Try2Success Admin", account.Email, account.FirstName + account.LastName);
            }
            catch (Exception)
            {

            }

            AccountInRoleCollection rl = AccountInRoleDA.SelectByAccountID(account.ID);
            string role = "";
            foreach (AccountInRole r in rl)
            {
                role += (AccountRoleDA.SelectByID(r.RoleID)).Name + ",";
            }


            //write authetication information into cookies
            writeCookie(account, role, true);

        }
    }
}