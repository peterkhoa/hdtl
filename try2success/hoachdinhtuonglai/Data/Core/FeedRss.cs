using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Caching;

namespace hoachdinhtuonglai.Data.Core
{
    public class AutoFeed
    {
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Content;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
        private string _Link;//or url

        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }
        private string _PubDate;

        public string PubDate
        {
            get { return _PubDate; }
            set { _PubDate = value; }
        }
    }

    public class AutoFeedCollection : List<AutoFeed>
    {
        public AutoFeedCollection() { }
        public AutoFeedCollection(IEnumerable<AutoFeed> list) : base(list) { }
    }

    public static class AutoFeedDA
    {
        public static string url_trangchu = "http://vnexpress.net/rss/gl/trang-chu.rss";
        public static string url_cuoi = "http://vnexpress.net/rss/gl/cuoi.rss";
        public static string url_tamsu = "http://vnexpress.net/rss/gl/ban-doc-viet-tam-su.rss";
        static Cache cache = HttpContext.Current.Cache;

        public static List<string> GetlistLinkRss()
        {
            List<string> listLink = new List<string>();

            string url = "/feed.xml";
            int cacheTimeOut = 30;
            string cacheName = "GetlistLinkRss";
            if (cache[cacheName] == null)
            {
                XmlTextReader reader = new XmlTextReader(url);
                DataSet ds = new DataSet();
                ds.ReadXml(reader);
                for (int i = 0; i < ds.Tables["item"].Rows.Count; ++i)
                {
                    listLink.Add(ds.Tables["item"].Rows[i]["link"].ToString());
                }

                cache.Add(cacheName, listLink, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);

            }
            return (List<string>)cache[cacheName];

        }

        public static string GetContent(string url)
        {           
            string content = AutoFeedDA.GetHTML(url);
            if (string.IsNullOrEmpty(content))
                return "";
            int begin = 0;
            int end = 0;
            int cacheTimeOut = 30;
            string cacheName = "GetContent" + url;
            if (cache[cacheName] == null)
            {
                begin = content.IndexOf("<div class=\"content\"");
                if (begin > 0)
                {
                    content = content.Substring(begin + 21, content.Length - begin - 22);
                }

                end = content.LastIndexOf("class=\"tag-parent\"");
                content = content.Substring(0, end - 5);

                content = content.Replace("http://vnexpress.net", "/?page=feed&url=http://vnexpress.net");
                content = content.Replace("http://vnexpress.net", "/?page=feed&url=http://vnexpress.net");
                content = content.Replace("\"/gl/", "\"/?page=feed&url=http://vnexpress.net/gl/");
                if (content.Contains("http://vnexpress.net"))
                {

                }
                content = content.Replace("/Files", "http://vnexpress.net/Files");
                content = content.Replace("/Library", "http://vnexpress.net/Library");
                content = content.Replace("/Images", "http://vnexpress.net/Images");
                content = content.Replace("/Service", "http://vnexpress.net/Service");
                cache.Add(cacheName, content, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);
            }
            return (string)cache[cacheName];
        }

        public static AutoFeedCollection SelectByUrlFeed(string url)
        {
            int cacheTimeOut = 30;
            string cacheName = "SelectByUrlFeed" + url;
            if (cache[cacheName] == null)
            {
                XmlTextReader reader = new XmlTextReader(url);
                DataSet ds = new DataSet();
                ds.ReadXml(reader);
                AutoFeedCollection listFeed = new AutoFeedCollection();
                for (int i = 0; i < ds.Tables["item"].Rows.Count; ++i)
                {
                    AutoFeed feed = new AutoFeed();
                    feed.Link = "/?page=feed&url=" + ds.Tables["item"].Rows[i]["link"];
                    string desc = (ds.Tables["item"].Rows[i]["description"].ToString().Replace("href=\"", "href=\"/?page=feed&url=\""));
                    int idex = desc.IndexOf(">>");
                    if (idex > 0)
                        feed.Description = desc.Substring(0, idex);
                    else
                        feed.Description = desc;
                    feed.Title = ds.Tables["item"].Rows[i]["title"].ToString();
                    listFeed.Add(feed);
                }
                cache.Add(cacheName, listFeed, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);
            }
            return (AutoFeedCollection)cache[cacheName];
        }

        public static string GetHTML(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "";
            }
            int cacheTimeOut = 30;
            string cacheName = "GetHTML" + url;
            if (cache[cacheName] == null)
            {
                string html = "";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader readerStream = new StreamReader(responseStream, Encoding.UTF8);
                html = readerStream.ReadToEnd();
                response.Close();
                readerStream.Close();

                cache.Add(cacheName, html, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTimeOut), CacheItemPriority.High, null);
            }
            return (string)cache[cacheName];
        }

    }
}