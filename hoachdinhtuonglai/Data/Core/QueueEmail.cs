using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace hoachdinhtuonglai.Data.Core
{
    

    public class QueueEMailCollection : List<QueueEMail>
    {
        public QueueEMailCollection() { }
        public QueueEMailCollection(IEnumerable<QueueEMail> list) : base(list) { }
    }

    public static class QueueEMailDA
    {
        static Database petapoco = ConnectionPP.getConnection();
        static Cache cache = HttpContext.Current.Cache;
        public static void delele_oldmail()
        {
            //using (f8roomDataContext context = new f8roomDataContext())
            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;
                /*
                var emailmoinhat = from item in context.QueueEMails
                                   orderby item.ID descending
                                   select item;*/


                QueueEMail emailmoi = petapoco.FirstOrDefault<QueueEMail>("Select * from QueueEMail order by ID desc");

                //chi luu lai 10.000 moi nhat thoi

                long id_caonhatphaixoa = emailmoi.ID - 10000;



                //var emailCunhatCanxoa = from item in context.QueueEMails
                //                        //  where item.IsSent == true // chi tinh nhung email da gui thanh cong
                //                        orderby item.ID ascending// lay tu thap len cao
                //                        select item;



                QueueEMail emailcu = petapoco.FirstOrDefault<QueueEMail>("Select * from QueueEMail order by ID");

                //moi lan xoa 1000 email thoi
                long id_tieptheocanxoa = emailcu.ID + 1000;

//                string command = @"DELETE FROM QueueEMail
//											 WHERE [isSent]=1 and [ID] < {0} and [ID] < {1}";

                petapoco.Execute(@"DELETE FROM QueueEMail WHERE [isSent]=1 and [ID] < {0} and [ID] < {1}", id_caonhatphaixoa, id_tieptheocanxoa);

                //context.ExecuteCommand(command, id_caonhatphaixoa, id_tieptheocanxoa);

                petapoco.CloseSharedConnection();

            }


        }
        public static QueueEMailCollection SelectAll()
        {
            //using (f8roomDataContext context = new f8roomDataContext())
            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;

                //var query = from item in context.QueueEMails
                //            orderby item.ID descending
                //            select item;
                QueueEMailCollection qr = new QueueEMailCollection(petapoco.Fetch<QueueEMail>("Select * from QueueEMail order by [ID] desc"));
                petapoco.CloseSharedConnection();
                return qr;
            }


        }

        public static void SelectToSend(int numberOfEmail, out QueueEMailCollection qc)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;

                //var query = (from item in context.QueueEMails
                //             where
                //             item.IsSent == false
                //             && item.IsSelected == false
                //             && item.SentDate <= DateTime.Now
                //             && item.SentTimes < 1
                //             orderby item.ID descending //lay tu moi nhat toi cu nhat
                //             select item).Take(numberOfEmail);

                QueueEMailCollection query = new QueueEMailCollection(petapoco.Fetch<QueueEMail>("select * from QueueEMail where [IsSelected]=0 and [IsSent]=0 and [SentDate] <= getDate() and [SentTimes] < 1 order by [ID] desc").Take(numberOfEmail));

                //    if (query.Count() > 0)
                {

                    //return value
                    qc = new QueueEMailCollection();
                    try
                    {
                        qc = new QueueEMailCollection(query);
                    }
                    catch (Exception ex) { }

                    //Update value
                    foreach (QueueEMail email in query)
                    {
                        email.IsSelected = true;
                        email.SentTimes = (Int16)(email.SentTimes + 1);
                        Update(email);
                        //Later if sent successfully, IsSent = true, IsSelected = false, SentDate = Now
                    }
                    //Update object

                   
                    //context.SubmitChanges();

                    //Then return for sending
                    //Ack ack, can not do this, after submit changes, query.Count()--> ZERO @@
                    //return new QueueEMailCollection(query);
                }
                //else
                //{
                //    qc = new QueueEMailCollection();
                //}
                petapoco.CloseSharedConnection();
            }
        }

        public static void UpdateAfterSent(QueueEMailCollection emailList)
        {
            //using (f8roomDataContext context = new f8roomDataContext())
            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;
                foreach (QueueEMail email in emailList)
                {
                    //var query = (from item in context.QueueEMails
                    //             where item.ID == email.ID
                    //             select item).FirstOrDefault();
                    QueueEMail query = petapoco.FirstOrDefault<QueueEMail>("select * from QueueEMail where [ID]={0}",email.ID);
                    if (query != null)
                    {
                        //Later if sent successfully, IsSent = true, SentDate = Now
                        if (email.IsSent == true)
                        {
                            query.SentDate = DateTime.Now;
                            query.IsSent = true;
                        }
                        else // IsSent = false;
                        {
                            query.IsSent = false;
                        }
                        //Update selected status for next selection
                        query.IsSelected = false;

                        //Submit changes to database
                        //context.SubmitChanges();
                        Update(query);
                    }

                }

                petapoco.CloseSharedConnection();

            }
        }


        public static QueueEMail SelectByID(long id)
        {
            //try
            {
               // using (f8roomDataContext context = new f8roomDataContext())
                {
                    petapoco = ConnectionPP.getConnection();
                    petapoco.EnableAutoSelect = false;
                    petapoco.EnableNamedParams = false;
                    petapoco.ForceDateTimesToUtc = false;

                    QueueEMail qr = petapoco.FirstOrDefault<QueueEMail>("Select * from QueueEMail where [ID]={0}",id);
                    petapoco.CloseSharedConnection();
                    return qr;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); throw new Exception( "Database Error");
                //return null;
            }
        }

        public static long Insert(QueueEMail QueueEMail)
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    petapoco = ConnectionPP.getConnection();
                    petapoco.EnableAutoSelect = false;
                    petapoco.EnableNamedParams = false;
                    petapoco.ForceDateTimesToUtc = false;

                    //context.QueueEMails.InsertOnSubmit(QueueEMail);
                    //context.SubmitChanges();

                    petapoco.Insert("QueueEMail", "ID", true, QueueEMail);
                    petapoco.CloseSharedConnection();
                    return QueueEMail.ID;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); throw new Exception( "Database Error");
                //return -1;
            }
        }
        public static long Update(QueueEMail QueueEMail)
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    petapoco = ConnectionPP.getConnection();
                    petapoco.EnableAutoSelect = false;
                    petapoco.EnableNamedParams = false;
                    petapoco.ForceDateTimesToUtc = false;

                    //QueueEMail original = SelectByID(QueueEMail.ID);

                    //context.QueueEMails.Attach(QueueEMail, original);
                    //context.SubmitChanges();

                    petapoco.Update(QueueEMail,QueueEMail.ID);
                    petapoco.CloseSharedConnection();
                    return QueueEMail.ID;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); 
                //throw ex;

            }
        }
        public static long Delete(QueueEMail QueueEMail)
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    petapoco = ConnectionPP.getConnection();
                    petapoco.EnableAutoSelect = false;
                    petapoco.EnableNamedParams = false;
                    petapoco.ForceDateTimesToUtc = false;

                    //context.QueueEMails.Attach(QueueEMail, SelectByID(QueueEMail.ID));
                    //context.QueueEMails.DeleteOnSubmit(QueueEMail);
                    //context.SubmitChanges();
                    petapoco.Delete("QueueEMail", "ID", QueueEMail, QueueEMail.ID);

                    return QueueEMail.ID;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex); 
                throw new Exception("Database Error");

            }
        }
        public static int CountAll()
        {
            //try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    petapoco = ConnectionPP.getConnection();
                    petapoco.EnableAutoSelect = false;
                    petapoco.EnableNamedParams = false;
                    petapoco.ForceDateTimesToUtc = false;
                    int c = petapoco.ExecuteScalar<int>("select count(*) from QueueEMail");
                    petapoco.CloseSharedConnection();
                    return c;
                }
            }
            // catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex);
                throw new Exception("Database Error");

            }
        }






        public static void UpdateAfterSucess(QueueEMail email)
        {
            email.SentDate = DateTime.Now;
            email.IsSent = true;
            Update(email);
        }

        public static void UpdateAfterNotSucess(QueueEMail email)
        {
            email.IsSelected = false;
            Update(email);
        }

    }
}