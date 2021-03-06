﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library;
using hoachdinhtuonglai.Data.Core;
using hoachdinhtuonglai.Data.BL;

namespace hoachdinhtuonglai.control.Post
{
    public partial class compose : BaseUserControl
    {
        protected string mess = "";
        protected bool is_login = false;
        protected long postid;
        protected string type;
        protected bool is_owner = false;
        private int objID;
        protected ForumCollection listCate = new ForumCollection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Current_user == null || Current_user.ID == 0)
            {
                is_login = false;
                return;

            }

            string t = Editor.Text;

            is_login = true;
            
            type = Request["type"];
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(type) && type == "edit")
                {
                    long.TryParse(Request["postid"], out postid);
                    if (postid > 0)
                    {
                        hoachdinhtuonglai.Data.Core.Post p = PostDA.SelectByID(postid);
                        if (p != null)
                        {
                            Editor.Text = p.Content;
                            txttitle.Text = p.Title;
                            //RadioButtonListCate.SelectedValue = p.CateID.Value.ToString();
                            objID = p.ObjectID.Value;
                            txtKeyword.Text = p.Tags;
                        }
                    }
                }
                listCate = ForumDA.SelectAll();
                setCate(listCate);
            }

            
            

        }

        protected void setCate(ForumCollection list)
        {
            RadioButtonListCate.Items.Clear();
            ListItem rd = new ListItem();
            foreach (Forum c in list)
            {
                rd = new ListItem();
                rd.Text = c.ForumName;
                rd.Value = c.ID.ToString();
                if (c.ID == objID)
                    rd.Selected = true;
                RadioButtonListCate.Items.Add(rd);

            }

            RadioButtonListCate.DataBind();
        }

        private bool validate()
        {
            if (string.IsNullOrEmpty(txttitle.Text.Trim()))
            {
                mess = "Bạn chưa nhập tiêu đề bài viết.";
                return false;
            }
            if (string.IsNullOrEmpty(Editor.Text.Trim()))
            {
                mess = "Bạn chưa nhập nội dung bài viết.";
                return false;
            }

            return true;
        }

        protected void btnCompose_Click(object sender, EventArgs e)
        {
            string content = Editor.Text;
            
            int forumid;
            int.TryParse(RadioButtonListCate.SelectedValue, out forumid);
            if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(txttitle.Text.Trim()))
            {
                Forum f = ForumDA.SelectByID(forumid);
                if (f != null)
                {
                    Category cate = CategoryDA.SelectByID(f.CateID.Value);
                    if (cate != null && Current_user != null && Current_user.ID > 0 && !string.IsNullOrEmpty(Current_user.Username))
                    {
                        hoachdinhtuonglai.Data.Core.Post post = new hoachdinhtuonglai.Data.Core.Post();
                        post.AccountID = Current_user.ID;
                        post.UserName = Current_user.Username;
                        post.CateID = cate.ID;
                        post.CateNameSlug = LanguageConvert.GenerateDesc(cate.CateName);
                        post.ObjectID = f.ID;
                        post.ObjectNameSlug = f.ForumNameSlug;
                        post.Spam = false;
                        post.Title = txttitle.Text.Trim();
                        post.View = 1;
                        post.Vote = 0;
                        post.Active = true;
                        post.Content = content;
                        if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()))
                            post.Tags = txtKeyword.Text.Trim();
                        else
                            post.Tags = txttitle.Text.Trim();
                        long id = PostDA.Insert(post);
                        mess = "Gửi bài viết thành công.";
                        Response.Redirect(LinkBuilder.getLinkPost(id, post.Title));
                    }
                }

            }
            else
            {
                mess = "Bạn xem lại dữ liệu nhập nhé!";
            }
        }

        private hoachdinhtuonglai.Data.Core.Post updateData(long postid)
        {
            //Editor.Font.ClearDefaults();
            
            string content = Editor.Text;

            int forumid;
            int.TryParse(RadioButtonListCate.SelectedValue, out forumid);
            if (validate())
            {
                Forum f = ForumDA.SelectByID(forumid);
                if (f != null)
                {
                    Category cate = CategoryDA.SelectByID(f.CateID.Value);
                    cate = CategoryDA.SelectByID(f.CateID.Value);
                    if (cate != null)
                    {
                        hoachdinhtuonglai.Data.Core.Post post = PostDA.SelectByID(postid);
                        //post.AccountID = Current_user.ID;
                        //post.Username = Current_user.Username;
                        post.CateID = cate.ID;
                        post.CateNameSlug = LanguageConvert.GenerateDesc(cate.CateName);
                        post.ObjectID = f.ID;
                        post.ObjectNameSlug = f.ForumNameSlug;
                        //post.Spam = false;
                        post.Title = txttitle.Text.Trim();
                        post.Content = content;
                        //post.View = 1;
                        //post.Vote = 0;
                        //post.Active = true;
                        if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()))
                            post.Tags = txtKeyword.Text.Trim();
                        else
                            post.Tags = txttitle.Text.Trim();
                        long id = PostDA.Update(post);
                        mess = "Gửi bài viết thành công.";
                        return post;
                        //Response.Redirect(LinkBuilder.getLinkPost(id, post.Title));
                    }
                }

            }

            return null;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            long.TryParse(Request["postid"], out postid);
            if (postid > 0)
            {
                hoachdinhtuonglai.Data.Core.Post p = updateData(postid);
                if (p !=null)
                {
                    Response.Redirect(LinkBuilder.getLinkPost(p.ID, p.Title));
                }
            }
            mess = "Bạn hãy kiểm tra lại dữ liệu nhập";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            long.TryParse(Request["postid"], out postid);
            if (postid > 0)
            {
                hoachdinhtuonglai.Data.Core.Post p = PostDA.SelectByID(postid);
                if (p != null)
                {
                    p.Active = false;
                    PostDA.Update(p);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string content = Editor.Text;

            int forumid;
            int.TryParse(RadioButtonListCate.SelectedValue, out forumid);
            if (!string.IsNullOrEmpty(content) && !string.IsNullOrEmpty(txttitle.Text.Trim()))
            {
                Forum f = ForumDA.SelectByID(forumid);
                if (f != null)
                {
                    Category cate = CategoryDA.SelectByID(f.CateID.Value);
                    if (cate != null)
                    {
                        hoachdinhtuonglai.Data.Core.Post post = new hoachdinhtuonglai.Data.Core.Post();
                        post.AccountID = Current_user.ID;
                        post.UserName = Current_user.Username;
                        post.CateID = cate.ID;
                        post.CateNameSlug = LanguageConvert.GenerateDesc(cate.CateName);
                        post.ObjectID = f.ID;
                        post.ObjectNameSlug = f.ForumNameSlug;
                        post.Spam = false;
                        post.Title = txttitle.Text.Trim();
                        post.View = 1;
                        post.Vote = 0;
                        post.Active = true;
                        post.Content = content;
                        if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()))
                            post.Tags = txtKeyword.Text.Trim();
                        else
                            post.Tags = txttitle.Text.Trim();
                        long id = PostDA.Insert(post);
                        mess = "Gửi bài viết thành công.";
                        Response.Redirect(LinkBuilder.getLinkPost(id, post.Title));
                    }
                }

            }
            else
            {
                mess = "Bạn xem lại dữ liệu nhập nhé!";
            }
        }

        
    }
}