using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Library;
using System.IO;
using System.Xml;
using System.Security.Principal;
using System.Web.Configuration;
using System.IO.Compression;
using System.Configuration;
using System.Text.RegularExpressions;

namespace hoachdinhtuonglai
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            System.Net.IPAddress[] ip = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList;



            string strHostName = ip[0].ToString();
            if (strHostName.ToLower().Contains("112.213.84.121"))// chi co ip 112.213.84.121 moi duoc gui email
            {
                //Trigger.Start(5000);
            }

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext InRequest = HttpContext.Current;
            HttpContext contxt = HttpContext.Current;
            string rawUrl = InRequest.Request.RawUrl.ToLower();
            string comming_link = InRequest.Request.Url.ToString();
            //Create AccountID

            //Replace Url Domain

            //get current path

            //remove cookies for static http://omaralzabir.com/prevent-asp-net-cookies-from-being-sent-on-every-css-js-image-request/
            if (contxt != null && contxt.Request != null && contxt.Request.Url.ToString().StartsWith("http://static"))
            {
                List<string> cookiesToClear = new List<string>();
                foreach (string cookieName in contxt.Request.Cookies)
                {
                    HttpCookie cookie = contxt.Request.Cookies[cookieName];
                    cookiesToClear.Add(cookie.Name);
                }

                foreach (string name in cookiesToClear)
                {
                    HttpCookie cookie = new HttpCookie(name, string.Empty);
                    cookie.Expires = DateTime.Today.AddYears(-1);

                    contxt.Response.Cookies.Set(cookie);
                }
            }

            //disable view for static web 
            HttpContext context = HttpContext.Current;
            string url_tocheck = context.Request.Url.ToString();
            if (url_tocheck.StartsWith("http://static"))
            {
                if (url_tocheck.EndsWith(".css") || url_tocheck.EndsWith(".js") || url_tocheck.EndsWith(".png") || url_tocheck.EndsWith(".gif") || url_tocheck.EndsWith(".jpg"))
                { //do nothing 
                }
                else
                {
                    Response.Redirect("http://www.deltaviet.net");
                }
            }

            string OldPath = InRequest.Request.Url.PathAndQuery.ToLower();
            if (OldPath.Contains("ckeditor"))
            {
                if (Request.RawUrl.Contains("?"))
                    generate_static(Request.RawUrl.Remove(Request.RawUrl.IndexOf("?")));
                else
                    generate_static(Request.RawUrl);
                return;
            }

            


            OldPath = InRequest.Request.RawUrl.ToLower();

            RewriteModuleSectionHandler cfg = (RewriteModuleSectionHandler)ConfigurationManager.GetSection("modulesSections/rewriteModule");
            //module is turned off in web.config
            if (!cfg.RewriteOn)
                return;
            //there is nothing to process
            if (OldPath.Length == 0)
                return;

            //load rewriting rules from web.config
            // and loop through rules collection until first match

            XmlNode rules = cfg.XmlSection.SelectSingleNode("rewriteRules");
            foreach (XmlNode xml in rules.SelectNodes("rule"))
            {
                try
                {
                    Regex reg = new Regex(cfg.RewriteBase + xml.Attributes["source"].Value, RegexOptions.IgnoreCase);
                    Match match = reg.Match(OldPath);
//                    string x = xml.Attributes["source"].Value;
                    if (match.Success)
                    {
                        OldPath = reg.Replace(OldPath, xml.Attributes["destination"].InnerText);
                        
                        if (OldPath.Length > 0)
                        {
                            //Check for QueryString paramaters
                            if (InRequest.Request.QueryString.Count > 0)
                            {
                                string sign = OldPath.IndexOf("?") != -1 ? "?" : "&";
                                OldPath = OldPath + sign + HttpContext.Current.Request.QueryString.ToString();
                            }

                            // new path to rewrite to
                            string rewrite = cfg.RewriteBase + OldPath;
                            //rewrite
                            InRequest.RewritePath(rewrite);
                        }
                        return;
                    }
                }
                catch { }
            }
        }

        protected void generate_static(string url)
        {
            string filename = Server.MapPath(url);
            Stream s = GetFileStream(filename);
            if (s != null)
            {
                using (s)
                {
                    Stream2Stream(s, Response.OutputStream);
                    Response.End();
                }
            }
        }

        protected Stream GetFileStream(string filename)
        {
            try
            {
                DateTime dt = File.GetLastWriteTime(filename);
                TimeSpan ts = dt - DateTime.Now;
                if (ts.TotalHours > 1)
                    return null;
                return new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch { return null; }
        }

        static public void Stream2Stream(Stream src, Stream dst)
        {
            byte[] buf = new byte[4096];
            int c;
            while (true)
            {
                c = src.Read(buf, 0, buf.Length);
                if (c == 0)
                {
                    return;
                }
                dst.Write(buf, 0, c);
            }
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            string roles;
            FormsIdentity identity;

            if (Context.Request.IsAuthenticated)
            {

                identity = (FormsIdentity)Context.User.Identity;
                roles = identity.Ticket.UserData;

                //string rolesAr[]

                Context.User = new GenericPrincipal(identity, roles.Split(','));

            }//end if (Context.Request.IsAuthenticated)
        }

    }
}
