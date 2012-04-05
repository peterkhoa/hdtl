using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace hoachdinhtuonglai.Data.BL
{
    public static class StringFormat
    {
        public static string StripHtml(string html){
            string[] arr = { "<br />", "<br>", "<br/>", "<p>", "</p>", "<div>", "</div>" };
            string temp = html;
            foreach (string s in arr)
            {
                temp = temp.Replace(s, "{n}");
            }

            temp = Library.LanguageConvert.StripHtml(temp, false);

            html = temp.Replace("{n}", "<br />");

            return html;
        }

        public static string GetFirstImage(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var image = document.DocumentNode.Descendants("img").FirstOrDefault();
            if (image != null)
            {
                return image.GetAttributeValue("src", string.Empty);
            }

            return string.Empty;
        }

        public static string Truncate(string html, int numCharacters)
        {
            var text = StripHtml(html);
           
            text = text.Replace("<br />", " ").Trim();
            text = text.Replace("\r", "");
            text = text.Replace("\n", "");
            text = text.Replace("\t", "");
            if (text.Length <= numCharacters)
                return text;
            text = text.Substring(0, numCharacters);
            text = text.Substring(0, text.LastIndexOf(' '));

            return text + " ...";
        }
    }
}