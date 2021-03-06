﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Timers;
using System.Threading;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
//using hoachdinhtuonglai.Data.BL;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.Data.BL
{
    public static class Trigger
    {
        private static bool m_Running = false;

        private static System.Timers.Timer m_Timer;
        private static int delete_counter = 0;

        /// <summary>
        /// Start timer
        /// </summary>
        /// <param name="interval">Time in millisecond</param>
        public static void Start(int interval)
        {
            // Initialize timer
            m_Timer = new System.Timers.Timer(interval);

            // Auto-reset (recurring)
            m_Timer.AutoReset = true;

            // Add event
            m_Timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Enable timer
            m_Timer.Enabled = true;
        }

        public static void Stop()
        {
            // Stop timer
            m_Timer.Enabled = false;

            // Dispose timer
            m_Timer.Dispose();
        }

        private static void WriteLog(string filePath, string message)
        {
            // Log
            StreamWriter writer = new StreamWriter(filePath, true);

            // Write error to file
            writer.WriteLine(DateTime.Now.ToString());
            writer.WriteLine(message);
            writer.WriteLine();
            writer.Flush();
            writer.Close();
        }

        private static void Processing()
        {
            string filePath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            filePath += "Bin\\TriggerError.log";

            if (m_Running == true)
            {
                return;
            }

            m_Running = true;

            // Add process here
            try
            {
                // Call SendMail here
                Option option = OptionDA.SelectRandomByName("SMTP");


                QueueEMailCollection QueueEMail = new QueueEMailCollection();
                QueueEMailDA.SelectToSend(10, out QueueEMail);
                if (QueueEMail.Count > 0)
                {
                    foreach (QueueEMail email in QueueEMail)
                    {


                        string[] arrayconfig = option.Value.Split(new char[] { ';' });
                        if (arrayconfig.Length == 5)
                            SendMail.SetConfigSMTP(arrayconfig[0], int.Parse(arrayconfig[1]), bool.Parse(arrayconfig[2]), arrayconfig[3], arrayconfig[4]);
                        if (!SendMail.ToSendEmail(email.Body, email.Subject, email.FromAddress, email.SenderName, email.ToAddress, email.ReceiverName))
                        {
                            //not success

                            QueueEMailDA.UpdateAfterNotSucess(email);



                        }
                        else
                        {
                            //success
                            QueueEMailDA.UpdateAfterSucess(email);


                        }
                    }
                }
            }
            catch (Exception exc)
            {
                // Write error here
                WriteLog(filePath, "Có lỗi xảy ra trong quá trình gửi mail, vui lòng kiểm tra thông tin gửi hay config smtp");
            }


            m_Running = false;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Processing();

            if (delete_counter < 12 * 60)
            // 5 giay x 12 lan x 60 = 60 phut se xoa 1 lan
            {
                QueueEMailDA.delele_oldmail();
                delete_counter = 0;
            }
            else
                delete_counter += 1;
        }


        public static void CallTriggerManually()
        {
            Processing();
        }

    }


    public class SendMail
    {
        public static SmtpClient smtpClient = new SmtpClient();

        public SendMail()
        {

        }

        public static string SubjectMailReg = "[Try2Success] - Chào mừng bạn đến với cộng đồng chia sẻ thành công Try2Success!";
        public static string SubjectMailGetPassword = "Khôi phục mật khẩu thành công";
        public static string SubjectMailComment = " đã bình luận ";

        public static string contentReg = "<strong> Hi You!</strong>"
                + "<br/>"
                 + "<br/>"
                + "Chào mừng bạn đã đến với gia đình Try2Success!"
                + "<br/>"
            //+ "Hiện tại tài khoản của bạn chưa được kich hoạt, để kích hoạt tài khoản, bạn soạn tin SMS với cú pháp: <b>REG Try2Success " + uid + " GỬI 8377</b><br />"
            //+ "Ví dụ:Username của bạn là vitcon thì bạn soạn: REG Try2Success vitcon sau đó gửi đến số 8377. Chúc bạn thành công! <br />"
                 + "<br/>"
                + "Mình là Nguyễn Thanh Hiền - một thành viên trong ban admin website."

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
                + "Nick hiện tại của mình trên Try2Success là thanhhien, địa chỉ " + "http://www.try2success.net/t2s/thanhhien" + ". Bạn add mình nhé . Ah, bạn ơi nick  admin@Try2Success.com mình chỉ dùng để thông báo chứ không dùng để nhận email hồi âm bạn thông cảm nhé."
                + "<br/>"
                 + "<br/>"
                + "Chúc bạn có một tuần mới thật nhiều niềm vui và may mắn! "
                + "<br/>"
                 + "<br/>"
                + "Thân mến,"
                + "<br/>"
                + "Ban quản lý website Try2Success";



        public static bool sendMail(string content, string subject, string senderaddress, string senderDisplayName, string receiveraddress, string receiverDisplayName)
        {

            Option option = OptionDA.SelectByName("SMTP");

            string[] arrayconfig = option.Value.Split(new char[] { ';' });
            if (arrayconfig.Length == 5)
            {
                SendMail.SetConfigSMTP(arrayconfig[0], int.Parse(arrayconfig[1]), bool.Parse(arrayconfig[2]), arrayconfig[3], arrayconfig[4]);


                return SendMail.ToSendEmail(content, subject, senderaddress, senderDisplayName, receiveraddress, receiverDisplayName);

            }
            return false;

        }

        public static bool ToSendEmail(string content, string subject, string senderaddress, string senderDisplayName, string receiveraddress, string receiverDisplayName)
        {
            if (!CheckEmailAddress(senderaddress) || !CheckEmailAddress(receiveraddress))
            {
                // throw new Exception("Địa chỉ email gửi hoặc nhận không hợp lệ");
                return false;
            }
            try
            {
                MailAddress senderAddress = new MailAddress(senderaddress, senderDisplayName);
                MailAddress receiverAddressG = new MailAddress(receiveraddress, receiverDisplayName);
                MailMessage message = new MailMessage(senderAddress, receiverAddressG);
                message.Body = content;
                message.BodyEncoding = Encoding.UTF8;
                message.Subject = subject;
                message.SubjectEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;

                smtpClient.Send(message);
                return true;
            }
            catch (SmtpException exc)
            {
                //throw new Exception("Có lỗi về đường truyền, vui lòng thử lại.");
                return false;
            }
        }
        public static void SetConfigSMTP(string host, int port, bool enableSSL, string username, string password)
        {
            smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = enableSSL;
            if (username == null || username == "")
                smtpClient.Credentials = new System.Net.NetworkCredential();
            else
                smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
        }
        public static bool CheckEmailAddress(string emailaddress)
        {
            Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return regex.IsMatch(emailaddress);
        }

        public static bool CheckKyTuDacBiet(string st)
        {
            string check = "-/?<>;,#\\@!~`^&*)(+%$:[]{}|âấẩậẫầăắ\"'ẵằắặáàảạãéèẻẽẹêếềểễệùúủũụưứừữựửíìỉĩịýỳỷỹỵóòỏõọôốồổỗộơớờởỡợđ";
            char[] arr = st.ToCharArray();
            foreach (char c in arr)
            {
                if (check.IndexOf(c) > -1)
                {
                    //msg = "Email không được chứa các ký tự đặc biệt hoặc gõ có dấu!";
                    return false;
                }
            }

            return true;
        }

        public static bool CheckKyTuCoDau(string st)
        {
            string check = "âấẩậẫầăắ\"'ẵằắặáàảạãéèẻẽẹêếềểễệùúủũụưứừữựửíìỉĩịýỳỷỹỵóòỏõọôốồổỗộơớờởỡợđ";
            char[] arr = st.ToCharArray();
            foreach (char c in arr)
            {
                if (check.IndexOf(c) > -1)
                {
                    //msg = "Email không được chứa các ký tự đặc biệt hoặc gõ có dấu!";
                    return false;
                }
            }

            return true;
        }

        public static void SendNotification(string content, string subject, string senderaddress, string senderDisplayName, string receiveraddress, string receiverDisplayName)
        {
            QueueEMail email = new QueueEMail();

            email.Subject = subject;
            //  email.FromAddress = senderaddress;
            email.FromAddress = "admin@try2success.com";
            email.ToAddress = receiveraddress;
            email.ReceiverName = receiverDisplayName;
            email.SenderName = senderDisplayName;
            email.SentDate = DateTime.Now;

            Option op = OptionDA.SelectByName("Notification_Email");

            if (op != null)
                content = op.Value.Replace("{name}", receiverDisplayName).Replace("{content}", content);

            email.Body = content;

            QueueEMailDA.Insert(email);


        }

        public static void SendNotification(string content, string subject, string senderaddress, string senderDisplayName, string receiveraddress, string receiverDisplayName, string type, long receiverid)
        {

            if (EmailPermissionDA.SelectOrInsertByAccountAndType(receiverid, type).Allow)
                SendNotification(content, subject, senderaddress, senderDisplayName, receiveraddress, receiverDisplayName);


        }
        public static void SendNotification(string content, string subject, string senderaddress, string senderDisplayName, string receiveraddress, string receiverDisplayName, DateTime date)
        {
            QueueEMail email = new QueueEMail();

            email.Subject = subject;
            //  email.FromAddress = senderaddress;
            email.FromAddress = "admin@try2success.com";
            email.ToAddress = receiveraddress;
            email.ReceiverName = receiverDisplayName;
            email.SenderName = senderDisplayName;
            email.SentDate = date;



            Option op = OptionDA.SelectByName("Notification_Email");

            if (op != null)
                content = op.Value.Replace("{name}", receiverDisplayName).Replace("{content}", content);

            email.Body = content;

            QueueEMailDA.Insert(email);


        }
        public static void SendNotification_noform(string content, string subject, string senderaddress, string senderDisplayName, string receiveraddress, string receiverDisplayName, DateTime date)
        {
            QueueEMail email = new QueueEMail();

            email.Subject = subject;
            //  email.FromAddress = senderaddress;
            email.FromAddress = "admin@try2success.com";
            email.ToAddress = receiveraddress;
            email.ReceiverName = receiverDisplayName;
            email.SenderName = senderDisplayName;
            email.SentDate = date;



            Option op = OptionDA.SelectByName("Notification_Email");

            // if (op != null)
            //    content = op.Value.Replace("{name}", receiverDisplayName).Replace("{content}", content);

            email.Body = content;

            QueueEMailDA.Insert(email);


        }

    }


    public class CommentTotal
    {
        public static string getDomain()
        {
            string url = HttpContext.Current.Request.Url.ToString().Replace("default.aspx", "");

            string domain = url.Remove(url.IndexOf("/", url.IndexOf("//") + 3));
            return domain;
        }

        public static long SendComment(long comment_id_parent, string content, Account commenter, Account receiver, string objectType, long objectID, string target_link, string target_object_name, int objecttypeID, string userAgent, string status, string UserHostName, bool for_group)
        {
            long commentID = 0;
            if (for_group)
            {

                AccountCollection listReceiver = new AccountCollection();
                CommentCollection listOldComment = CommentDA.SelectByPerformOnObjectID(objectID);
                if (receiver.ID != commenter.ID)
                    listReceiver.Add(receiver);
                foreach (Comment cm in listOldComment)
                {
                    if (cm.AccountID != commenter.ID && cm.AccountID != receiver.ID)
                    {
                        Account a = AccountDA.SelectByID(cm.AccountID);
                        if (a != null && (listReceiver.Find(l=>l.ID == a.ID)==null))
                            listReceiver.Add(a);
                    }
                }



                content = content.Replace("\n", "<br />");



                Comment c = new Comment();

                c.AccountID = commenter.ID;
                c.Author_IP = UserHostName;
                c.Content = content;
                c.ObjID = objectID;
                c.ObjectType = objectType;
                c.ObjectType = objectType;
                c.Agent = userAgent;
                c.Status = status;
                c.ReceiverID = receiver.ID; //owner
                c.ReceiverUsername = receiver.Username;
                c.ParentID = comment_id_parent;
                c.Target_link = target_link;
                c.Target_object_name = target_object_name;
                c.Username = commenter.Username;

                commentID = CommentDA.Insert(c);
                List<long> emailed_accounts = new List<long>();
                //Account commenter = AccountDA.SelectByID(senderID);//((Account)S-ession["Account"]);
                string emailcontent = commenter.Username + " đã viết lời bình luận  <b>" + target_object_name + "</b><br/> "
                                     + "<br/><br/> \"" + c.Content + "\" <br/><br/>"
                                     + "Bạn có thể xem chi tiết và trả lời bằng cách sử dụng link dưới đây: <br/> <a href='" + target_link + " '> " + target_link + "</a>"
                                     + "<br/>" + DateTime.Now.ToString();
                foreach (Account r in listReceiver)
                {
                    //ActionLogDA.Add(receiver.ID, receiver.Username,

                    //       objecttypeID,
                    //        objectID,
                    //        "viết bình luận <a href='" + target_link + "'>" + target_object_name + "</a> của nhóm <a href='" + target_link + "'>" + team.Name + "</a>"
                    //        , r.ID
                    //        , ""
                    //        , ""
                    //        , ""//Request.RawUrl
                    //        , c.ObjectID.ToString() + "+" + ObjectTypeID.Goal.ToString()

                    //        );

                    ActionLog acl = new ActionLog();
                    acl.AuthorID = commentID;
                    acl.Date = DateTime.Now;
                    acl.TargetAccount = r.ID;
                    acl.PerformOnObjectID = objectID;
                    acl.Username = r.Username;
                    acl.Href = " viết bình luận trên <a href='" + target_link + "' target='_blank'>" + target_object_name + "</a>";
                    acl.XCommentID = c.ObjID + "+" + objectType;
                    acl.ShortDescription = "Bạn <a href='" + LinkBuilder.getLinkProfile(commenter) + "' target='_blank'>" + commenter.Username + "</a> vừa" + acl.Href;
                    acl.ToUser = r.Username;

                    ActionLogDA.Insert(acl);

                    emailed_accounts.Add(r.ID);


                    //notify to 
                    if (commenter.ID != r.ID)
                        //SendMail.sendMail(emailcontent, commenter.Username + " viết lời bình luận  " + target_object_name, commenter.Email, commenter.FirstName + " " + commenter.LastName, r.Email, r.FirstName + " " + r.LastName, email_type.goal_comment, r.ID);
                        SendMail.SendNotification(emailcontent, commenter.Username + " viết lời bình luận  " + target_object_name, commenter.Email, commenter.FullName, r.Email, r.FullName, email_type.goal_comment, r.ID);


                }



            }
            return commentID;
        }
    }
}