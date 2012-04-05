using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hoachdinhtuonglai.Data.Core;
using hoachdinhtuonglai.Data.BL;
using Library;
using System.Text.RegularExpressions;

namespace hoachdinhtuonglai.control.Post
{
    public partial class post : BaseUserControl
    {
        public long id;
        protected hoachdinhtuonglai.Data.Core.Post posts;//= new hoachdinhtuonglai.Post();
        protected string mess;
        protected CommentCollection listComment = new CommentCollection();
        public int page = 1;
        public int pagesize = 20;
        protected PostCollection listPost = new PostCollection();
        protected double countPage = 0;
        protected bool is_owner = false;
        protected string shortContent, content, matchString;
        protected string urlShare = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request["id"], out id);



            if (id > 0)
            {
                posts = PostDA.SelectByID(id);


                if (posts == null)
                {
                    mess = "Bài viết này không có, bạn hãy liên hệ với Admin về sự cố này.";
                    return;
                }
                else
                {
                    listPost = PostDA.SelectPostRelation(posts.CateID.Value, posts.ObjectID.Value, id, 4);
                }

                content = (posts.Content);
                shortContent = LanguageConvert.StripHtml(posts.Content, false);
                matchString = Regex.Match(content, "(?<=<img.*?src=\")[^\"]*(?=\".*?((/>)|(>.*</img)))", RegexOptions.IgnoreCase).Value;
                shortContent = shortContent.Replace("\"", "'");
                shortContent = shortContent.Length > 150 ? shortContent.Substring(0, shortContent.IndexOf(" ", 140)) : shortContent;

                urlShare = LinkBuilder.getLinkPost(posts.ID, posts.ObjectID.Value, posts.Title);
                string domain = CommentTotal.getDomain();
                urlShare = domain + urlShare;
                is_owner = (Current_user.Username == posts.UserName) ? true : false;
                Page.Title = posts.Title + " | T2S";
                Page.MetaDescription = shortContent;
                Page.MetaKeywords = posts.Tags;
                if (!IsPostBack)
                {
                    posts.View += 1;

                    PostDA.Update(posts);
                }

                //listComment = CommentDA.SelectByObjectID(id,page,pagesize);
                #region comment
                commentajax1.objectID = id;
                commentajax1.objecttype = "comment";
                commentajax1.receiverID = posts.AccountID;
                commentajax1.receiverUsername = posts.UserName;
                string url = Request.RawUrl;
                commentajax1.target_link = url;
                commentajax1.target_object_name = posts.Title.Length > 100 ? posts.Title.Substring(0, posts.Title.IndexOf(" ", 80)) : posts.Title;
                #endregion comment
            }
            else
            {
                //string cateSlug = Request["page"];
                //listPost = PostDA.SelectByCateNameSlug(cateSlug,page,pagesize);
                //countPage = PostDA.countAll(cateSlug);
            }
        }
    }
}