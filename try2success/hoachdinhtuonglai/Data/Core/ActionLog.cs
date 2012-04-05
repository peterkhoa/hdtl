using System;
using System.Linq;
using System.Collections.Generic;
using hoachdinhtuonglai;
using System.Web.Caching;
using System.Web;
using System.ComponentModel;


namespace hoachdinhtuonglai.Data.Core
{

    //public class ActionLog
    //{
    //    private long _ID;

    //    public long ID
    //    {
    //        get { return _ID; }
    //        set { _ID = value; }
    //    }

    //    private long _AccountID;

    //    public long AccountID
    //    {
    //        get { return _AccountID; }
    //        set { _AccountID = value; }
    //    }

    //    private long _PerformOnObjectID;

    //    public long PerformOnObjectID
    //    {
    //        get { return _PerformOnObjectID; }
    //        set { _PerformOnObjectID = value; }
    //    }

    //    private int _ObjectTypeID;

    //    public int ObjectTypeID
    //    {
    //        get { return _ObjectTypeID; }
    //        set { _ObjectTypeID = value; }
    //    }

    //    private string _ShortDescription;

    //    public string ShortDescription
    //    {
    //        get { return _ShortDescription; }
    //        set { _ShortDescription = value; }
    //    }

    //    private DateTime _Date;

    //    public DateTime Date
    //    {
    //        get { return _Date; }
    //        set { _Date = value; }
    //    }

    //    private string _Username;

    //    public string Username
    //    {
    //        get { return _Username; }
    //        set { _Username = value; }
    //    }

    //    string _Href;

    //    public string Href
    //    {
    //        get { return _Href; }
    //        set { _Href = value; }
    //    }

    //    long _TargetAccount;

    //    public long TargetAccount
    //    {
    //        get { return _TargetAccount; }
    //        set { _TargetAccount = value; }
    //    }

    //    string _XCommentID;

    //    public string XCommentID
    //    {
    //        get { return _XCommentID; }
    //        set { _XCommentID = value; }
    //    }

        
    //}

    // Custom comparer for the Product class.
    class ActionLogComparer : IEqualityComparer<ActionLog>
    {
        // Products are equal if their namesz and product numbers are equal.
        public bool Equals(ActionLog x, ActionLog y)
        {

            // Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            // Check whether the products' properties are equal.
            return x.PerformOnObjectID == y.PerformOnObjectID && x.ObjectTypeID == y.ObjectTypeID;
        }

        // If Equals() returns true for a pair of objects,
        // GetHashCode must return the same value for these objects.

        public int GetHashCode(ActionLog actionLog)
        {
            // Check whether the object is null.
            if (Object.ReferenceEquals(actionLog, null)) return 0;

            // Get the hash code for the Name field if it is not null.
            int hashProductObjectTypeID = actionLog.PerformOnObjectID == null ? 0 : actionLog.PerformOnObjectID.GetHashCode();

            // Get the hash code for the Code field.
            int hashProductObjectID = actionLog.PerformOnObjectID.GetHashCode();

            // Calculate the hash code for the product.
            return hashProductObjectTypeID ^ hashProductObjectID;
        }

    }
    public class ActionLogList : List<ActionLog>
    {
        public ActionLogList()
        { }
        public ActionLogList(IEnumerable<ActionLog> list)
            : base(list)
        { }

    }

    [DataObject]
    public static class ActionLogDA
    {
        static Database petapoco;
        public static Cache cache = HttpContext.Current.Cache;

        public static ActionLogList SelectAll()
        {
            try
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                //{
                //    var query = from item in context.ActionLogs
                //                select item;
                //    return new ActionLogList(query);
                //}
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;
                ActionLogList qr = new ActionLogList(petapoco.Fetch<ActionLog>("Select * from ActionLog"));
                petapoco.CloseSharedConnection();
                return qr;

            }
            catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex);
                return new ActionLogList();
            }
        }

        public static long CountAllType(int objectTypeID, DateTime date)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            //{
            //    var query = (from item in context.ActionLogs
            //                 where item.ObjectTypeID == objectTypeID && item.Date >= date
            //                 select item).Count();
            //    return query;
            //}
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            long c = petapoco.ExecuteScalar<long>("select count(*) from ActionLog where [ObjectTypeID]=@0 and [Date]>=@1",objectTypeID,date);
            petapoco.CloseSharedConnection();
            return c;

        }

        public static Page<ActionLog> UnreadNotification(long AccountID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            Page<ActionLog> qr = petapoco.Page<ActionLog>(0, 100, "Select * FROM ActionLog where [TargetAccount] = @0 and [IsViewed]!= 1", AccountID);
            petapoco.CloseSharedConnection();

            return qr;

        }

        public static ActionLogList SelectAllUnreadNotification(long AccountID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            ActionLogList qr = new ActionLogList(petapoco.Fetch<ActionLog>("Select * FROM ActionLog where [TargetAccount] = @0 and [IsViewed]!= 1", AccountID));
            petapoco.CloseSharedConnection();

            return qr;

        }

        public static long CountUnreadNotification(long AccountID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            long c = petapoco.ExecuteScalar<long>("Select count(*) FROM ActionLog where [TargetAccount] = @0 and [IsViewed] != 1", AccountID);
            petapoco.CloseSharedConnection();
            
            return c;

        }

        public static long CountAll(DateTime date)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            //{
            //    var query = (from item in context.ActionLogs
            //                 where item.Date >= date
            //                 select item).Count();
            //    return query;
            //}
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            long c =  petapoco.ExecuteScalar<long>("select count(*) from ActionLog where CONVERT(datetime,Date,101)=@0",date.ToShortDateString());
            petapoco.CloseSharedConnection();
            return c;

        }
        public static ActionLog SelectByID(long id)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            //{
            //    var query = from item in context.ActionLogs
            //                where item.ID == id
            //                select item;
            //    return query.FirstOrDefault<ActionLog>();
            //}
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            ActionLog qr = petapoco.FirstOrDefault<ActionLog>("Select * from ActionLog where [ID]=@0",id);
            petapoco.CloseSharedConnection();
            return qr;

        }

        public static ActionLog SelectByObject(long objectID, int objectTypeID)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            //{
            //    var query = from item in context.ActionLogs
            //                where item.ObjectTypeID == objectTypeID && item.PerformOnObjectID == objectID
            //                select item;
            //    return query.FirstOrDefault<ActionLog>();
            //}
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            ActionLog qr = petapoco.FirstOrDefault<ActionLog>("Select * from ActionLog where [ObjectTypeID]=@0 and [PerformOnObjectID]=@1", objectTypeID,objectID);
            petapoco.CloseSharedConnection();
            return qr;


        }
        public static ActionLogList SelectByUserID(long id)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            //{
            //    var query = from item in context.ActionLogs
            //                where item.AccountID == id
            //                orderby item.ID descending
            //                select item;
            //    ActionLogList al = new ActionLogList(query);

            //    return new ActionLogList(al.Distinct(new ActionLogComparer()));

            //}
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            ActionLogList qr = new ActionLogList( petapoco.Fetch<ActionLog>("Select * from ActionLog where [AuthorID]=@0 order by ID desc", id));
            petapoco.CloseSharedConnection();
            return qr;

        }
        public static ActionLogList SelectByUserID(long id, int pageSize, int pageNumber)
        {

           // using (f8roomDataContext context = new f8roomDataContext())
            {
                if (pageNumber == 0)
                    pageNumber = 1;
                //var query = (from item in context.ActionLogs

                //             where (item.AccountID == id || item.TargetAccount.Value == id)
                //             orderby item.ID descending
                //             select item).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                //ActionLogList al = new ActionLogList(query);

                //return new ActionLogList(al.Distinct(new ActionLogComparer()));
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                ActionLogList qr = new ActionLogList(petapoco.Fetch<ActionLog>(pageNumber, pageSize, "Select * from ActionLog where [AuthorID]=@0 or [TargetAccount] = @1 order by ID desc", id, id));
                petapoco.CloseSharedConnection();
                return qr;

            }

        }
        public static long Insert(ActionLog actionLog)
        {

            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;
            actionLog.IsViewed = false;
             

            var x = petapoco.Insert("ActionLog", "ID", true, actionLog);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }
        public static long Update(ActionLog actionLog)
        {
            try
            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;

                
                long x = petapoco.Update("ActionLog", "ID", actionLog, actionLog.ID);
                petapoco.CloseSharedConnection();
                return x;
            }
            catch (Exception ex)
            {
                
                return -1;
            }
        }

        public static void UpdateIsViewed(long accountID)
        {
            try
            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;

                string sql = "UPDATE ActionLog set IsViewed=1 WHERE [TargetAccount]=@0";

                long x = petapoco.Execute(sql, accountID);
                petapoco.CloseSharedConnection();
                
            }
            catch (Exception ex)
            {

                
            }
        }
        public static long Delete(ActionLog ActionLog)
        {
            try
            {
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.EnableNamedParams = false;
                petapoco.ForceDateTimesToUtc = false;

                

                long x = petapoco.Delete("ActionLog", "ID", ActionLog, ActionLog.ID);
                petapoco.CloseSharedConnection();
                return x;
            }
            catch (Exception ex)
            {
                //ErrorLogDA.Insert(ex);
                return -1;
            }
        }

        public static ActionLog SelectByLatestAction(long accountID)
        {

            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            ActionLog qr = petapoco.FirstOrDefault<ActionLog>("Select * from ActionLog where [AccountID]=@0 ORDER by ID desc", accountID); petapoco.CloseSharedConnection();
            return qr;
            //}

        }
        public static ActionLogList SelectByLatestActions(long accountID)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            {
                //var query = (from item in context.ActionLogs
                //             where (item.AccountID == accountID || item.TargetAccount.Value == accountID)
                //             orderby item.ID descending
                //             select item);
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                ActionLogList al = new ActionLogList(petapoco.Fetch<ActionLog>("Select * from ActionLog where [AccountID]=@0 or [TargetAccount]=@1 ORDER by ID desc", accountID,accountID));
                petapoco.CloseSharedConnection();
                return new ActionLogList(al.Distinct(new ActionLogComparer()));
            }


        }
        public static ActionLogList SelectByLatestAllActions(int numberOfItems, int start, long accountID)
        {
           // using (f8roomDataContext context = new f8roomDataContext())
            {
                //var friends = from f in context.Followings
                //              where f.Follower == accountID
                //              select f.Followee;
                //var query = (from item in context.ActionLogs

                //             where (friends.Contains(item.AccountID) || friends.Contains(item.TargetAccount.Value))
                //             orderby item.ID descending
                //             select item).Skip(start).Take(numberOfItems);

                string sql = "Select * from ActionLog where (AccountID in (Select Followee from Following where [Follower]=@0)) or ([TargetAccount] in (Select Followee from Following where [Follower]=@1))";
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                ActionLogList al = new ActionLogList(petapoco.Fetch<ActionLog>(sql,accountID,accountID).Skip(start).Take(numberOfItems));
                petapoco.CloseSharedConnection();
                return new ActionLogList(al.Distinct(new ActionLogComparer()));
            }

        }
        public static ActionLogList SelectByLatestAllActions(int numberOfItems, int start, int type)
        {
            //using (f8roomDataContext context = new f8roomDataContext())
            {
                //var query = (from item in context.ActionLogs
                //             where item.ObjectTypeID == type
                //             orderby item.ID descending
                //             select item).Skip(start).Take(numberOfItems);
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                ActionLogList al = new ActionLogList(petapoco.Fetch<ActionLog>("select * from ActionLog where [ObjectTypeID]=@0 order by ID desc",type).Skip(start).Take(numberOfItems));
                petapoco.CloseSharedConnection();
                return new ActionLogList(al.Distinct(new ActionLogComparer()));
            }

        }

        public static ActionLogList SelectByLatestAllActions(int numberOfItems, int start)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            {
                //var query = (from item in context.ActionLogs

                //             orderby item.ID descending
                //             select item).Skip(start).Take(numberOfItems);
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                ActionLogList al = new ActionLogList(petapoco.Fetch<ActionLog>("Select * from ActionLog order by ID desc").Skip(start).Take(numberOfItems));
                petapoco.CloseSharedConnection();
                return new ActionLogList(al.Distinct(new ActionLogComparer()));
            }

        }
        public static ActionLogList SelectByLatestActions(long accountID, int numberOfItems)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            {
                //var query = (from item in context.ActionLogs
                //             where item.AccountID == accountID
                //             orderby item.ID descending
                //             select item).Take(numberOfItems);
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                ActionLogList al = new ActionLogList(petapoco.Fetch<ActionLog>("select * from ActionLog where [AccountID]=@0",accountID).Take(numberOfItems));
                petapoco.CloseSharedConnection();
                return new ActionLogList(al.Distinct(new ActionLogComparer()));
            }

        }
        //public static ActionLogList SelectByLatestActionOfFriendsOfAccount(long accountID)
        //{
        //    try
        //    {
        //        using (f8roomDataContext context = new f8roomDataContext())
        //        {
        //            var query = from item in context.FriendShips
        //                        where item.AserID == accountID // watch thoi cung xem dc log
        //                        from actionLog in item..ActionLogs // friendship1
        //                        where
        //                        actionLog.AccountID == item.AserFriendID
        //                        && actionLog.ID == item.Account1.ActionLogs.Max(a => a.ID)
        //                        orderby actionLog.Date descending
        //                        select actionLog;


        //            return new ActionLogList(query);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLogDA.Insert(ex);
        //        return new ActionLogList();
        //    }
        //}
        public static long Add(long accountID, string username, int actionTypeID, int objectTypeID, long objectFakeIDorID, string htmlDescription)
        {
            ActionLog item = new ActionLog();
            item.AuthorID = accountID;
            //item.ActionTypeID = actionTypeID;
            item.Date = DateTime.Now;
            item.ObjectTypeID = objectTypeID;
            item.PerformOnObjectID = objectFakeIDorID;
            item.ShortDescription = htmlDescription;
            item.Username = username;
            return Insert(item);
        }

        public static long Add(long accountID, string username, int objectTypeID, long objectID, string htmlDescription, long? targetAccount, string media, string properties, string href, string xcomment)
        {
            ActionLog item = new ActionLog();
            item.AuthorID = accountID;
            item.TargetAccount = targetAccount.Value;
            //item.ActionTypeID = actionTypeID;
            item.Date = DateTime.Now;
            item.ObjectTypeID = objectTypeID;
            item.PerformOnObjectID = objectID;
            item.ShortDescription = htmlDescription;
            item.Username = username;
            //item.Properties = properties;
            //item.Media = media;
            item.Href = href;
            if (xcomment == "")
                item.XCommentID = item.PerformOnObjectID.ToString() + "+" + item.ObjectTypeID.ToString();
            else
                item.XCommentID = xcomment;
            long actionID = Insert(item);


            if (targetAccount != null && (accountID != targetAccount))
            {
                //Notification n = new Notification();
                //n.ActionID = actionID;
                //n.isViewed = false;
                //n.ToUser = targetAccount.Value;
                //NotificationDA.Insert(n);
            }
            return actionID;


        }


        public static float CountAll_untilExcute(DateTime startDay, DateTime endDay)
        {
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                {
                    //return context.ExecuteQuery<int>(@"select count(*) from actionlog where Date<={0} and Date >={1} ", endDay, startDay).FirstOrDefault();
                    petapoco = ConnectionPP.getConnection();
                    petapoco.EnableAutoSelect = false;
                    petapoco.ForceDateTimesToUtc = false;
                    float c =  petapoco.ExecuteScalar<float>("Select count(*) from ActionLog where [Date] <= @0 and [Date] >= @1", endDay, startDay);
                    petapoco.CloseSharedConnection();
                    return c;
                }
            }
        }

        public static float CountAll_until(DateTime time, DateTime time1)
        {
            {
                //using (f8roomDataContext context = new f8roomDataContext())
                //{
                //    return (from o in context.ActionLogs
                //            where o.Date >= time && o.Date <= time1
                //            select o).Count();
                //}
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                float c = petapoco.ExecuteScalar<float>("Select count(*) from ActionLog where [Date] <= @0 and [Date] >= @1", time1, time);
                petapoco.CloseSharedConnection();
                return c;
            }
        }

        //count typeobjectid by accountid
        public static long CountTypeObjectID(int typeobjectid, long accountid)
        {

            //using (f8roomDataContext context = new f8roomDataContext())
            //{

            //    return (from o in context.ActionLogs
            //            where o.AccountID == accountid && o.ObjectTypeID == typeobjectid
            //            select o).Count();
            //}
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            long c = petapoco.ExecuteScalar<long>("Select count(*) from ActionLog where [AccountID] <= @0 and [ObjectTypeID] >= @1", accountid, typeobjectid);
            petapoco.CloseSharedConnection();
            return c;

        }

        // count type object by count 
        public static int CountTypeObjectByTime(int id, long accountid, DateTime date)
        {
            //using (f8roomDataContext context = new f8roomDataContext())
            //{
            //    return (from o in context.ActionLogs
            //            where o.ObjectTypeID == id && o.AccountID == accountid && o.Date.Day == date.Day && o.Date.Month == date.Month && o.Date.Year == date.Year
            //            select o).Count();

            //}
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            int c = petapoco.ExecuteScalar<int>("Select count(*) from ActionLog where [Date] <= @0 and [Date] >= @1", date.AddDays(1), date.AddDays(-1));
            petapoco.CloseSharedConnection();
            return c;
        }
        ///loi 

        // Select distinct by date 
        public static List<DateTime> SelectTypeObjectByTime(int id, long accountid)
        {
            //using (f8roomDataContext context = new f8roomDataContext())
            {
                //var query = (from o in context.ActionLogs
                //             where o.ObjectTypeID == id && o.AccountID == accountid
                //             select o.Date);
                //return new List<DateTime>(query);
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                List<DateTime> ld =  petapoco.Fetch<DateTime>("Select Date from ActionLog where [ObjectTypeID]=@0 and [AccountID]=@1",id,accountid);
                petapoco.CloseSharedConnection();

                return ld;
            }

        }

        // Select action log by object_type , accountid
        public static List<long> SelectActionLogByAccountID(int object_id, long accountid)
        {
            //using (f8roomDataContext context = new f8roomDataContext())
            {
                //var query = (from o in context.ActionLogs
                //             where o.ObjectTypeID == object_id && o.AccountID == accountid
                //             select o.PerformOnObjectID);
                //return new List<long>(query);
                petapoco = ConnectionPP.getConnection();
                petapoco.EnableAutoSelect = false;
                petapoco.ForceDateTimesToUtc = false;
                List<long> ll = petapoco.Fetch<long>("Select PerformOnObjectID from ActionLog  where [ObjectTypeID]=@0 and [AccountID]=@1",object_id,accountid);
                petapoco.CloseSharedConnection();
                return ll;
            }

        }




    }
}
