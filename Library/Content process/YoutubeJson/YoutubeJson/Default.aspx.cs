using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;


namespace YoutubeJson
{
    public partial class _Default : System.Web.UI.Page
    {
        private static HttpWebRequest request;
        private static int counter = 1;
        private static HttpWebResponse response;
        //private static HtmlDocument doc = new HtmlDocument();
        private static string vi = "";


        private static UTF8Encoding enc = new UTF8Encoding();


        private static WebClient wc = new WebClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["l"]))
                get();

        }

        public static string RemoveHtml(string stringConvert)
        {
            string[] stringSeparators = new string[] { " " };
            string RegExStr = "<[^>]*>";
            Regex R = new Regex(RegExStr);

            stringConvert = R.Replace(stringConvert, " ");

            return stringConvert;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            get();
        }
        protected void get()
        {

            string input = TextBox1.Text;

            if (!string.IsNullOrEmpty(Request["l"]))
                input = Request["l"];

            string code = input.Substring(input.IndexOf("v=") + 2);
            if (code.IndexOf("&") > 0)
            {

                code = code.Remove(code.IndexOf("&"));
            }

            string url = "http://www.youtube.com/api/timedtext?sparams=asr_langs%2Ccaps%2Cexpire%2Cv&asr_langs=en&caps=asr&lang=en&name=English&kind&type=track&v=" + code; // "http://www.youtube.com/watch_ajax?action_get_caption_track_all&v=" + url;



            vi = enc.GetString(wc.DownloadData(url));


            if (vi.Length < 10)
            {
                url = "http://www.youtube.com/api/timedtext?sparams=asr_langs%2Ccaps%2Cexpire%2Cv&asr_langs=en&caps=asr&hl=en&type=track&lang=en&name&kind&v=" + code;

                vi = enc.GetString(wc.DownloadData(url));
            }

            //if (vi.Length < 10)
            //{
            //    url = "http://www.youtube.com/api/timedtext?sparams=asr_langs%2Ccaps%2Cexpire%2Cv&asr_langs=en&caps=asr&hl=en&type=track&lang=en&name&kind&v=" + code;

            //    vi = enc.GetString(wc.DownloadData(url));
            //}

            //       vi = vi.Substring(vi.IndexOf("plaintext_list") - 1);
            //    vi = vi.Remove( vi.IndexOf( "is_draft")-3);
            //   vi = "{ " + vi + "}";


            //    JObject o = JObject.Parse(vi);

            //     IList<string> sentences = o.SelectToken("plaintext_list").Select(s => (string)s.SelectToken("text")).ToList();
            //     foreach (string s in sentences)
            string temp = input;
            temp = temp.Replace("watch?v=", "v/");

            string output = "<br/><object width=\"640\" height=\"390\"><param name=\"movie\" value=\"" + temp + "\"></param><param name=\"allowFullScreen\" value=\"true\"></param><param name=\"allowscriptaccess\" value=\"always\"></param><embed src=\"" + temp + "\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" width=\"640\" height=\"390\"></embed></object><br/>";
            output += "<strong>Transcript</strong>:<br/><br/>";

            {
                output += HttpUtility.HtmlDecode(HttpUtility.HtmlDecode(RemoveHtml(vi))); //s;
            }
            output = output.Replace("\n", "  ").Replace(">>", "\n\n>>");
            output = output.Replace(". ", ". \n").Replace("?", "? \n").Replace("!", "! \n");

            try
            {
                int index20000 = output.IndexOf('.', 20000);
                if (index20000 > 0)
                    output = output.Remove(index20000) + "\n<!--nextpage-->\n" + output.Substring(index20000);
            }
            catch (Exception ex)
            { }
            try
            {
                int index40000 = output.IndexOf('.', 40000);
                if (index40000 > 0)
                    output = output.Remove(index40000) + "\n<!--nextpage-->\n" + output.Substring(index40000);
            }
            catch (Exception ex)
            { }

            try
            {
                int index60000 = output.IndexOf('.', 60000);
                if (index60000 > 0)
                    output = output.Remove(index60000) + "\n<!--nextpage-->\n" + output.Substring(index60000);
            }
            catch (Exception ex)
            { }
            try
            {
                int index80000 = output.IndexOf('.', 80000);
                if (index80000 > 0)
                    output = output.Remove(index80000) + "\n<!--nextpage-->\n" + output.Substring(index80000);
            }
            catch (Exception ex)
            { }
            TextBox2.Text = output;

        }
    }
}