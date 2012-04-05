using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace Library
{
    public class ParseContent
    {
        public static string search_in_dictionary(Dictionary<string, int> d, string key)
        {
            foreach (KeyValuePair<string, int> v in d)
            {
                if (v.Key.Contains(key))
                return v.Key;

            }
            return "";
        }


        public static string ParsePostBody(string postBody, int numberString)
        {
            if (postBody != null)
            {
                string returnString = "";
                string[] stringSeparators = new string[] { "...", "..", ". ", ".", " .", " . ", "!", "?" };
                string RegExStr = "<[^>]*>";
                Regex R = new Regex(RegExStr);

                postBody = R.Replace(postBody, " ");
                string[] ListString = postBody.Split(stringSeparators, StringSplitOptions.None);
                for (int i = 0; i < Math.Min(numberString, ListString.Length); i++)
                {
                    returnString += ListString[i] + ". ";
                }
                return returnString.Replace("\"", "'");
            }
            else
                return "";
        }

        public static string ParsePostBodyWithHtml(string postBody, int numberString)
        {
            if (postBody != null)
            {
                string returnString = "";
                string[] ListString = postBody.Split(' ');
                for (int i = 0; i < Math.Min(numberString, ListString.Length); i++)
                {
                    returnString += ListString[i] + " ";
                }
                return returnString;
            }
            else
                return "";
            return postBody;
        }

        public static string ParsePostBodyToWord(string postBody, int numberWord)
        {
            string returnString = "";
            string[] stringSeparators = new string[] {" "};
            string RegExStr = "<[^>]*>";
            Regex R = new Regex(RegExStr);
            postBody = postBody.Replace("<br/>", "");
            postBody = R.Replace(postBody, " ");
            string[] ListString = postBody.Split(stringSeparators, StringSplitOptions.None);
            for (int i = 0; i < Math.Min( numberWord,ListString.Length); i++)
            {
                returnString += ListString[i] + " ";
            }
            return returnString;
        }

        public static int CountWord(string postBody)
        {            
            string[] stringSeparators = new string[] { " " };
            string RegExStr = "<[^>]*>";
            Regex R = new Regex(RegExStr);

            postBody = R.Replace(postBody, "");
            string[] ListString = postBody.Split(stringSeparators, StringSplitOptions.None);

            return ListString.Length;
            
        }

        public static string RemoveHtml(string stringConvert)
        {
            string[] stringSeparators = new string[] { " " };
            string RegExStr = "<[^>]*>";
            Regex R = new Regex(RegExStr);

            stringConvert = R.Replace(stringConvert, "");

            return stringConvert;
        }
        public static string Get1stImage(string content)
        {
            try
            {
                if (content.IndexOf("<img") > 0)
                {

                    

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(content);
                  
                    return  doc.DocumentNode.SelectNodes("//img")[0].GetAttributeValue("src", null);
                    
                    
                }
                else

                    return "";
            }
            catch ( Exception ex)
            {
                return "";
            }
        }


        public static List<string> ParseBaoMoi(string content)
        {

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            HtmlNodeCollection nc = doc.DocumentNode.SelectNodes("//*[@rel='tag']");

            List<string> result = new List<string>();

            foreach (HtmlNode n in nc)
            {
                result.Add(n.InnerHtml);
            }
            return result;
        }

        public static List<string> ParseGoogleSuggest(string content)
        {
            content = content.Replace("window.google.ac.h([", "").Replace("]]])","").Replace("\"", "").Replace(")", "");

            content = content.Split(new string[] { "[[" }, StringSplitOptions.None)[1];

            string[] list = content.Split(new string[] { "],[" }, StringSplitOptions.None);
            List<string> result = new List<string>();

            foreach (string l in list)
            {
                result.Add(l.Split(',')[0]);
            }


          //  JsonReader reader = new JsonReader(new StringReader(content));

          ///  JObject o = JObject.Parse(content);

            //while (reader.Read())
            //{
            //  //  Console.WriteLine(reader.TokenType + "\t\t" + reader.ValueType.ToString() + "\t\t" + reader.Value.ToString());
            //}


            //HtmlDocument doc = new HtmlDocument();
            //doc.LoadHtml(content);

            //HtmlNodeCollection nc = doc.DocumentNode.SelectNodes("//*[@rel='tag']");

            //List<string> result = new List<string>();

            //foreach (HtmlNode n in nc)
            //{
            //    result.Add(n.InnerHtml);
            //}
            return result;
        }

      
    }
}
