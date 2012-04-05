using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Caching;

namespace hoachdinhtuonglai.Data.Core
{
    public partial class AccountInRole
    {
        [ResultColumn]
        public string Username
        {
            get;
            set;
        }
    }

    public class AccountInRoleCollection : List<AccountInRole>
    {
        public AccountInRoleCollection() { }

        public AccountInRoleCollection(IEnumerable<AccountInRole> list) : base(list) { }
    }

    [DataObject]
    public static class AccountInRoleDA
    {
        static Database petapoco;

        static Cache Cache = HttpContext.Current.Cache;

        public static AccountInRoleCollection SelectAll()
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountInRoleCollection acc = new AccountInRoleCollection(petapoco.Fetch<AccountInRole>(@"Select * from AccountInRole")); petapoco.CloseSharedConnection();
            return acc;
        }



        public static AccountInRole SelectByID(long id)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountInRole ac = petapoco.FirstOrDefault<AccountInRole>(@"Select * from AccountInRole where id=@0", id);
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static AccountInRole SelectByRoleIDnAccountID(int roleid, long accountid)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountInRole ac = petapoco.FirstOrDefault<AccountInRole>(@"Select * from AccountInRole where [AccountID]=@0 and [RoleID]=@1", accountid, roleid);
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static AccountInRoleCollection SelectByRoleName(string name)
        {
            name = name.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;


            string cacheName = "SelectByRoleName_AccountInRole" + name;
            int cacheTimeOut = 5;
            AccountInRoleCollection ac = new AccountInRoleCollection();
            if (Cache[cacheName] == null)
            {
                ac = new AccountInRoleCollection(petapoco.Fetch<AccountInRole>(@"Select * from AccountInRole where RoleName=@0", name));
                Cache.Add(cacheName, ac, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);
            }
            petapoco.CloseSharedConnection();
            return (AccountInRoleCollection)Cache[cacheName];
        }


        public static AccountInRoleCollection SelectByRoleID(int roleID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountInRoleCollection ac = new AccountInRoleCollection(petapoco.Fetch<AccountInRole>(@"Select [AccountInRole].*, Account.Username as Username from AccountInRole inner join Account on (AccountInRole.AccountID = Account.ID) where [RoleID]=@0", roleID));
            petapoco.CloseSharedConnection();
            return ac;
        }


        public static AccountInRoleCollection SelectByAccountID(long accountID)
        {

            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            string cacheName = "SelectByAccountID_AccountInRole" + accountID;
            int cacheTimeOut = 5;

            AccountInRoleCollection ac = new AccountInRoleCollection();
            if (Cache[cacheName] == null)
            {
                ac = new AccountInRoleCollection(petapoco.Fetch<AccountInRole>(@"Select * from AccountInRole where [AccountID]=@0", accountID));
                Cache.Add(cacheName, ac, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);
            }
            petapoco.CloseSharedConnection();
            return (AccountInRoleCollection)Cache[cacheName];
        }


        public static AccountInRoleCollection SelectRoleByPageSize(int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountInRoleCollection acc = new AccountInRoleCollection(petapoco.Fetch<AccountInRole>(page,pagesize,@"Select * from AccountInRole where Activate=1"));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static long Update(AccountInRole AccountInRole)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            long x = petapoco.Update("AccountInRole", "ID", AccountInRole, AccountInRole.ID); petapoco.CloseSharedConnection();
            return x;
        }

        public static long Insert(AccountInRole AccountInRole)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;

            petapoco.ForceDateTimesToUtc = false;

            var x = petapoco.Insert("AccountInRole", "ID", true, AccountInRole);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }

        public static long Delete(AccountInRole AccountInRole)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            long x = petapoco.Delete("AccountInRole", "ID", AccountInRole, AccountInRole.ID);
            petapoco.CloseSharedConnection();

            return x;
        }
    }
}