using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaViet.Utility
{
    public class LoaderUtility
    {
        public static void SetTitle(string title)
        {
          //  HttpContext.Current.S-ession.Add("__page_title", title);
        }

        //public static void SetMetaData(Metadata metadata)
        //{
          
        //    /*
        //    HttpCookie __page_metadata = HttpContext.Current.Request.Cookies["__page_metadata"];

        //    if (__page_metadata == null)
        //    {
        //        __page_metadata.Expires = DateTime.Now.AddSeconds(10);

        //        HttpContext.Current.Response.Cookies.Add(__page_metadata);

        //        //HttpContext.Current.S-ession.Add("__page_metadata", metadata);
        //    }
        //    else
        //    {
        //        Metadata newData = new Metadata();
        //        Metadata data = (Metadata)HttpContext.Current.S-ession["__page_metadata"];
        //        for (int i = 0; i < metadata.GetLength(); i++)
        //        {
        //            for (int j = 0; j < data.GetLength(); j++)
        //            {
        //                if (metadata.GetName(i).ToLower() == data.GetName(j).ToLower())
        //                {
        //                    newData.Add(metadata.GetName(i), metadata.GetContent(i) + ", " + data.GetContent(j));
        //                }
        //                else
        //                {
        //                    newData.Add(metadata.GetName(i), metadata.GetContent(i));
        //                    if (i==0)
        //                        newData.Add(data.GetName(j), data.GetContent(j));
        //                }
        //            }
        //        }
        //    }
        //     * */
        //}

        public static void SetError(string error)
        {
            HttpCookie __page_error = new HttpCookie("__page_error", error);

            __page_error.Expires = DateTime.Now.AddSeconds(10);
            HttpContext.Current.Response.Cookies.Add(__page_error);


            //HttpContext.Current.S-ession.Add("__page_error", error);
        }

        public static void SetNotice(string notice)
        {
            HttpCookie __page_notice = new HttpCookie("__page_notice", notice);

            __page_notice.Expires = DateTime.Now.AddSeconds(10);
           // HttpContext.Current.S-ession.Add("__page_notice", notice);
        }
    }
}
