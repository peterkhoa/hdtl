using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Caching;

namespace hoachdinhtuonglai.Data.Core
{
    public partial class AccountRole
    {
        
    }

       
    //}

    public class AccountRoleCollection : List<AccountRole>
    {
        public AccountRoleCollection() { }

        public AccountRoleCollection(IEnumerable<AccountRole> list) : base(list) { }
    }

    [DataObject]
    public static class AccountRoleDA
    {
        static Database petapoco = ConnectionPP.getConnection();
        static Cache Cache = HttpContext.Current.Cache;

        public static AccountRoleCollection SelectAll()
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountRoleCollection acc = new AccountRoleCollection(petapoco.Fetch<AccountRole>(@"Select * from AccountRole")); petapoco.CloseSharedConnection();
            return acc;
        }



        public static AccountRole SelectByID(long id)
        {
            petapoco = ConnectionPP.getConnection();
            string cacheName = "SelectByAccountRoleID" + id;
            int cacheTimeOut = 360;
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountRole ac = new AccountRole();
            if (Cache[cacheName] == null)
            {
                ac = petapoco.FirstOrDefault<AccountRole>(@"Select * from AccountRole where id=@0", id);
                if (ac != null)
                {
                    Cache.Add(cacheName, ac, null, Cache.NoAbsoluteExpiration, TimeSpan.FromDays(cacheTimeOut), CacheItemPriority.High, null);
                }
            }
            petapoco.CloseSharedConnection();
            return (AccountRole)Cache[cacheName];
        }

        public static AccountRole SelectByRoleName(string name)
        {
            petapoco = ConnectionPP.getConnection();
            string cacheName = "SelectByRoleName" + name;
            int cacheTimeOut = 360;
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountRole ac = new AccountRole();
            if (Cache[cacheName] == null)
            {
                ac = petapoco.FirstOrDefault<AccountRole>(@"Select * from AccountRole where [Name]=@0", name);
                if (ac != null)
                {
                    Cache.Add(cacheName, ac, null, Cache.NoAbsoluteExpiration, TimeSpan.FromDays(cacheTimeOut), CacheItemPriority.High, null);
                }
            }
            petapoco.CloseSharedConnection();
            return (AccountRole)Cache[cacheName];
        }

        public static long Update(AccountRole AccountRole)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            long x = petapoco.Update("AccountRole", "ID", AccountRole, AccountRole.ID);
            Cache.Remove("SelectByAccountRoleID" + x); petapoco.CloseSharedConnection();
            return x;
        }

        public static long Insert(AccountRole AccountRole)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;

            petapoco.ForceDateTimesToUtc = false;

            var x = petapoco.Insert("AccountRole", "ID", true, AccountRole);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }

        public static long Delete(AccountRole AccountRole)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            long x = petapoco.Delete("AccountRole", "ID", AccountRole, AccountRole.ID);
            petapoco.CloseSharedConnection();
            return x;
        }
    }
}