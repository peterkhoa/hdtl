﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.Data.BL
{
    public static class Unit
    {
        public static Account checkLogin(string username, string password)
        {
            username = username.ToLower();
            Account account = AccountDA.SelectByUsername(username);
            password = Encryptor.Encrypt(password);
            if (account != null)
            {
                if (account.Password == password)
                    return account;
            }
            return null;
        }


        public static Account checkLoginByEmail(string email, string password)
        {
            email = email.ToLower();
            Account account = AccountDA.SelectByEmail(email);
            password = Encryptor.Encrypt(password);
            if (account != null)
            {
                if (account.Password == password)
                    return account;
            }
            return null;
        }

        public static Account changePassword(string username, string newpassword)
        {
            username = username.ToLower();
            Account account = AccountDA.SelectByUsername(username);
            newpassword = Encryptor.Encrypt(newpassword);
            if (account != null)
            {
                account.Password = newpassword;
                AccountDA.Update(account);
                return account;
            }
            return null;
        }

        public static Account changePassword(Account account, string newpassword)
        {
            
            newpassword = Encryptor.Encrypt(newpassword);
            if (account != null)
            {
                account.Password = newpassword;
                AccountDA.Update(account);
                return account;
            }
            return null;
        }

        public static Account changeAvatar(string username, string urlAvatar)
        {
            username = username.ToLower();
            Account account = AccountDA.SelectByUsername(username);
            
            if (account != null)
            {
                
                AccountDA.Update(account);
                return account;
            }
            return null;
        }

    }
}