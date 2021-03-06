﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Web.Caching;
using System.Web;

namespace hoachdinhtuonglai.Data.Core
{
    public class AccountCollection : List<Account>
    {
        public AccountCollection() { }
        public AccountCollection(IEnumerable<Account> list) : base(list) { }
    }

    //public class Account
    //{
    //    private string _username;
    //    private long _id;

    //    public long ID
    //    {
    //        get { return _id; }
    //        set { _id = value; }
    //    }

    //    public string Username
    //    {
    //        get { return _username; }
    //        set { _username = value; }
    //    }
    //    private string _password;

    //    public string Password
    //    {
    //        get { return _password; }
    //        set { _password = value; }
    //    }
    //    private string _fullname;

    //    public string Fullname
    //    {
    //        get { return _fullname; }
    //        set { _fullname = value; }
    //    }
    //    private string _email;

    //    public string Email
    //    {
    //        get { return _email; }
    //        set { _email = value; }
    //    }
    //    private string _skype;

    //    public string Skype
    //    {
    //        get { return _skype; }
    //        set { _skype = value; }
    //    }
    //    private string _yahoo;

    //    public string Yahoo
    //    {
    //        get { return _yahoo; }
    //        set { _yahoo = value; }
    //    }
    //    private DateTime _birthday;

    //    public DateTime Birthday
    //    {
    //        get { return _birthday; }
    //        set { _birthday = value; }
    //    }
    //    private DateTime _lastlogin;

    //    public DateTime Lastlogin
    //    {
    //        get { return _lastlogin; }
    //        set { _lastlogin = value; }
    //    }
    //    private string _address;

    //    public string Address
    //    {
    //        get { return _address; }
    //        set { _address = value; }
    //    }
    //    private string _facebook;

    //    public string Facebook
    //    {
    //        get { return _facebook; }
    //        set { _facebook = value; }
    //    }
    //    private string _school;

    //    public string School
    //    {
    //        get { return _school; }
    //        set { _school = value; }
    //    }
    //    private string _company;

    //    public string Company
    //    {
    //        get { return _company; }
    //        set { _company = value; }
    //    }
    //    private long _exp;

    //    public long Exp
    //    {
    //        get { return _exp; }
    //        set { _exp = value; }
    //    }
    //    private string _dream;

    //    public string Dream
    //    {
    //        get { return _dream; }
    //        set { _dream = value; }
    //    }
    //    private string _sothich;

    //    public string Sothich
    //    {
    //        get { return _sothich; }
    //        set { _sothich = value; }
    //    }
    //    private string _province;

    //    public string Province
    //    {
    //        get { return _province; }
    //        set { _province = value; }
    //    }
    //    private string _provinceSlug;

    //    public string ProvinceSlug
    //    {
    //        get { return _provinceSlug; }
    //        set { _provinceSlug = value; }
    //    }
    //    private string _schoolSlug;

    //    public string SchoolSlug
    //    {
    //        get { return _schoolSlug; }
    //        set { _schoolSlug = value; }
    //    }

    //    private string _avatar;

    //    public string Avatar
    //    {
    //        get {
    //            if (_avatar == null)
    //                return "/images/default.png";
    //            return _avatar; }
    //        set { _avatar = value; }
    //    }

    //    private string _slogan;

    //    public string Slogan
    //    {
    //        get { return _slogan; }
    //        set { _slogan = value; }
    //    }

    //    private string _phonenumber;

    //    public string PhoneNumber
    //    {
    //        get { return _phonenumber; }
    //        set { _phonenumber = value; }
    //    }
    //    private string _city;

    //    public string City
    //    {
    //        get { return _city; }
    //        set { _city = value; }
    //    }
    //    private string _location;

    //    public string Location
    //    {
    //        get { return _location; }
    //        set { _location = value; }
    //    }

    //    private string _gender;

    //    public string Gender
    //    {
    //        get { return _gender; }
    //        set { _gender = value; }
    //    }

    //    private string _firstName;

    //    public string FirstName
    //    {
    //        get { return _firstName; }
    //        set { _firstName = value; }
    //    }

    //    private string _lastName;

    //    public string LastName
    //    {
    //        get { return _lastName; }
    //        set { _lastName = value; }
    //    }

    //    private string _IP;

    //    public string IP
    //    {
    //        get { return _IP; }
    //        set { _IP = value; }
    //    }

    //    private bool _active=true;

    //    public bool Active
    //    {
    //        get { return _active; }
    //        set { _active = value; }
    //    }


    //}

    public partial class Account
    {
        public string getAvatar()
        {
            if (this._Avatar == null)
                return "/images/main/noavatar.png";
            return _Avatar;
        }
    }

    [DataObject]
    public static class AccountDA
    {
        static Database petapoco = ConnectionPP.getConnection();

        static Cache cache = HttpContext.Current.Cache;

        public static AccountCollection SelectAll()
        {
            //var 
            //petapoco.Connection.Open();
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            int cacheTimeOut = 10;
            string cacheName = "SelectAll_Account";

            if (cache[cacheName] == null)
            {

                AccountCollection acc = new AccountCollection(petapoco.Fetch<Account>(@"Select * from Account"));
                cache.Add(cacheName, acc, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);

            }
            petapoco.CloseSharedConnection();
            //
            return (AccountCollection)cache[cacheName];
        }

        public static Account SelectByID(long id)
        {
            
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            int cacheTimeOut = 10;
            string cacheName = "SelectByID_" + id.ToString();

            if (cache[cacheName] == null)
            {

                Account ac = petapoco.FirstOrDefault<Account>(@"Select * from Account where id=@0", id);
                if (ac != null)
                {
                    cache.Add(cacheName, ac, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);

                }

            }
            petapoco.CloseSharedConnection();
            return (Account)cache[cacheName];
        }

        public static Account SelectByUsername(string username)
        {
            username = username.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            int cacheTimeOut = 10;
            string cacheName = "SelectByUsername_" + username;

            if (cache[cacheName] == null)
            {

                Account ac = petapoco.FirstOrDefault<Account>(@"Select * from Account where Username=@0", username);
                if (ac != null)
                    cache.Add(cacheName, ac, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);

            }
            petapoco.CloseSharedConnection();
            return (Account)cache[cacheName];
        }



        public static Account SelectByEmail(string email)
        {
            email = email.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            int cacheTimeOut = 5;
            string cacheName = "SelectByEmail_" + email;

            if (cache[cacheName] == null)
            {

                Account ac = petapoco.FirstOrDefault<Account>(@"Select * from Account where Email = @0", email);
                if (ac != null)
                {
                    cache.Add(cacheName, ac, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);
                }
            }
            petapoco.CloseSharedConnection();
            return (Account)cache[cacheName];
        }

        public static AccountCollection SelectNewAccount(int num)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountCollection acc = new AccountCollection(petapoco.Query<Account>(@"Select top (@0) * from Account ORDER BY ID DESC", num));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static AccountCollection SelectByShoolSlug(string school)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountCollection acc = new AccountCollection(petapoco.Fetch<Account>(@"Select * from Account where SchoolSlug like @0 or School like @1", school));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static AccountCollection SelectByShool(string school)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            AccountCollection acc = new AccountCollection(petapoco.Fetch<Account>(@"Select * from Account where School like @1", school));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static AccountCollection selectTopExp(int num)
        {
            //var 
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;
            int cacheTimeOut = 5;
            string cacheName = "selectTopExp_" + num.ToString();

            if (cache[cacheName] == null)
            {


                AccountCollection accol = new AccountCollection(petapoco.Fetch<Account>(@"select top @0  * from Account order by Exp desc", num));
                cache.Add(cacheName, accol, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);

            }
            petapoco.CloseSharedConnection();
            return (AccountCollection)cache[cacheName];

        }

        public static long Update(Account account)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            //petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;


            long x = petapoco.Update("Account", "ID", account, account.ID);
            ////
            petapoco.CloseSharedConnection();
            cache.Remove("SelectByID_" + account.ID.ToString());
            return x;
        }

        public static long Insert(Account account)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            //petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            //Account ac = AccountDA.SelectByID(account.ID);
            //ac = 

            var x = petapoco.Insert("Account", "ID", true, account);

            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }

        public static long Delete(Account account)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            //Account ac = AccountDA.SelectByID(account.ID);

            long x = petapoco.Delete("Account", "ID", account, account.ID);
            petapoco.CloseSharedConnection();
            cache.Remove("SelectByID_" + account.ID.ToString());
            ////
            return x;
        }
    }
}
