using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library;
using hoachdinhtuonglai.Data.Core;
//using hoachdinhtuonglai.Data.BL;

namespace hoachdinhtuonglai.Data.BL
{
    public static class LinkBuilder
    {
        static string url_profile = "/t2s/";
        public static string getLinkProfile(long accountid)
        {
            
            Account account = AccountDA.SelectByID(accountid);
            return url_profile + account.Username;
        }

        public static string getLinkProfile(string username)
        {
            
            return url_profile + username;
        }

        public static string getLinkProfile(Account account)
        {
                  
            return url_profile + account.Username;
        }

        public static string getLinkPost(long postid, int objectid, string title)
        {
            title = LanguageConvert.RemoveDiacritics(title);
            return "/" + ForumDA.SelectByID(objectid).ForumNameSlug + "/" + title + "-" + postid + ".html";
        }

        public static string getLinkPost(long postid, string title)
        {
            title = LanguageConvert.RemoveDiacritics(title);
            return "/bai-viet/" + title + "-" + postid + ".html";
        }
    }
}