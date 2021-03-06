﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Caching;
using System.Web;

namespace hoachdinhtuonglai.Data.Core
{
    

    public class EmailPermissionCollection : List<EmailPermission>
    {
        public EmailPermissionCollection()
        { }
        public EmailPermissionCollection(IEnumerable<EmailPermission> list)
            : base(list)
        { }
    }

    

    [DataObject]
    public static class EmailPermissionDA
    {
        static Database petapoco = ConnectionPP.getConnection();
        public static Cache cache = HttpContext.Current.Cache;
        public static EmailPermissionCollection SelectAll()
        {

            {
                //using (f8roomDataContext context = new f8roomDataContext())
                //{
                //    var query = from item in context.EmailPermissions
                //                orderby item.ID descending

                //                select item;
                //    return new EmailPermissionCollection(query);
                //}
                petapoco = ConnectionPP.getConnection();
                EmailPermissionCollection qr = new EmailPermissionCollection(petapoco.Fetch<EmailPermission>("Select * from EmailPermission"));
                petapoco.CloseSharedConnection();
                return qr;
            }

        }




        public static EmailPermission SelectByID(long id)
        {
            {

                //using (f8roomDataContext context = new f8roomDataContext())
                //{
                //    context.DeferredLoadingEnabled = false;
                //    var query = from item in context.EmailPermissions
                //                where item.ID == id
                //                select item;
                //    return query.FirstOrDefault<EmailPermission>();


                //}
                petapoco = ConnectionPP.getConnection();
                EmailPermission qr = petapoco.FirstOrDefault<EmailPermission>("Select * from EmailPermission where [ID]=@0",id);
                petapoco.CloseSharedConnection();
                return qr;
            }

        }
        public static EmailPermission SelectOrInsertByAccountAndType(long aid, string type)
        {
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                //{
                //    context.DeferredLoadingEnabled = false;
                //    var query = from item in context.EmailPermissions
                //                where item.AccountID == aid && item.Type == type
                //                select item;

                //    if (query.Count() == 0)
                //    {
                //        EmailPermission em = new EmailPermission();
                //        em.AccountID = aid;
                //        em.Type = type;
                //        em.Allow = true;
                //        Insert(em);
                //    }
                //    return query.FirstOrDefault<EmailPermission>();


                //}
                petapoco = ConnectionPP.getConnection();
                EmailPermission qr = petapoco.FirstOrDefault<EmailPermission>("Select * from EmailPermission where [AccountID]=@0 and [Type]=@1",aid,type);
                petapoco.CloseSharedConnection();
                if (qr != null)
                    return qr;

                qr = new EmailPermission();
                qr.AccountID = aid;
                qr.Allow = true;
                qr.Type = type;

                Insert(qr);

                return qr;

            }

        }



        public static long Insert(EmailPermission EmailPermission)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;
            var x = petapoco.Insert("EmailPermission", "ID", true, EmailPermission);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());

        }
        public static long Update(EmailPermission EmailPermission)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            

            long x = petapoco.Update("EmailPermission", "ID", EmailPermission, EmailPermission.ID);
            petapoco.CloseSharedConnection();
            return x;

        }

        public static long UpdatePermission(long aid, string type, bool allow)
        {
           
            EmailPermission em = SelectOrInsertByAccountAndType(aid, type);
            em.Allow = allow;
            return Update(em);

        }
        public static long Delete(EmailPermission EmailPermission)
        {

            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;

                

                long x = petapoco.Delete("Comment", "ID", EmailPermission, EmailPermission.ID);
                petapoco.CloseSharedConnection();
                return x;
            }

        }


        public static int CountAll()
        {
            //try
            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;
                int c = petapoco.ExecuteScalar<int>(@"Select count(*) from EmailPermission");
                petapoco.CloseSharedConnection();
                return c;
            }

        }
    }
}
