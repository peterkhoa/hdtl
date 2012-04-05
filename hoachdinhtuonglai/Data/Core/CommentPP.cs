using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Web.Caching;
using System.Web;

namespace hoachdinhtuonglai.Data.Core
{
    public partial class Comment
    {        

        public int comment_level = 0;
        
    }

    public class CommentCollection : List<Comment>
    {
        public CommentCollection() { }
        public CommentCollection(IEnumerable<Comment> list) : base(list) { }
    }

    [DataObject]
    public static class CommentDA
    {
        static Database petapoco;
        public static Cache cache = HttpContext.Current.Cache;

        public static CommentCollection SelectAll()
        {
            petapoco = ConnectionPP.getConnection();
            //petapoco.Connection.Open();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection acc = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment"));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static CommentCollection SelectAllActivateNotSpam()
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection acc = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [Activate]=1 and [Spam]=0"));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static CommentCollection getAllComment(CommentCollection listComment)
        {
            CommentCollection comments = new CommentCollection();
            CommentCollection temp = new CommentCollection();
            
            foreach (Comment c in listComment)
            {


                temp = (CommentDA.SelectByParentID(c.ID));
                if (temp != null)
                {
                    if (c.ParentID == 0)
                        comments.Add(c);
                    foreach (Comment co in temp)
                    {
                        co.comment_level += 1;
                        //if(co.ID =
                    }

                    comments.AddRange(temp);

                }
                else
                    comments.Add(c);
            }

            return comments;
        }

        public static CommentCollection SelectAllSpam()
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection acc = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [Spam]=1"));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static Comment SelectByID(long id)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Comment ac = petapoco.FirstOrDefault<Comment>(@"Select * from Comment where [ID]=@0", id);
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByUsername(string username)
        {
            username = username.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection( petapoco.Fetch<Comment>(@"Select * from Comment where [Username]=@0", username));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByUsername(long accountID)
        {
            
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [AccountID]=@0", accountID));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByObjectType(string type)
        {
            type = type.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            
            //petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection( petapoco.Fetch<Comment>(@"Select * from Comment where [ObjectType] = @0", type));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByObjectTypeObjID(string type, long objID)
        {
            type = type.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [ObjectType] = @0 and [ObjID]=@1", type,objID));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByObjectTypeObjID(string type, long objID,int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            type = type.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection(petapoco.Fetch<Comment>(page,pagesize,@"Select * from Comment where [ObjectType] = @0 and [ObjID]=@1 Order By Date DESC", type, objID));

            petapoco.CloseSharedConnection();
            return ac;
        }


        public static CommentCollection SelectByObjectID(long objid)
        {
            
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [ObjID] = @0", objid));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByPerformOnObjectID(long objid)
        {

            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [ObjID] = @0", objid));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByParentID(long parentID)
        {
            
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [ParentID] = @0", parentID));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByObjectID(long objid, int page,int pagesize)
        {
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection ac = new CommentCollection(petapoco.Fetch<Comment>(page,pagesize,@"Select * from Comment where [ObjID] = @0", objid));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static CommentCollection SelectByDate(DateTime date)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection acc = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [Date] like @0", date));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static CommentCollection SelectByPageSize(int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            CommentCollection acc = new CommentCollection(petapoco.Fetch<Comment>(@"Select * from Comment where [Activate=1").Skip((page-1)*pagesize).Take(page*pagesize));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static long Update(Comment Comment)
        {
            petapoco = ConnectionPP.getConnection();
           
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

           
            long x = petapoco.Update("Comment", "ID", Comment, Comment.ID);
            petapoco.CloseSharedConnection();
            return x;
        }

        public static long Insert(Comment Comment)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;
            Comment.Date = DateTime.Now;
            Comment.Activate = true;
            

            var x = petapoco.Insert("Comment", "ID", true, Comment);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }

        public static long Delete(Comment Comment)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;           

            long x = petapoco.Delete("Comment", "ID", Comment, Comment.ID);
            petapoco.CloseSharedConnection();
            return x;
        }

        public static int countAll()
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;
            int c = petapoco.ExecuteScalar<int>(@"Select count(*) from Comment");
            petapoco.CloseSharedConnection();
            return c;
        }

        public static int countAll(long objID, string objecttype)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            
            petapoco.ForceDateTimesToUtc = false;
            int c = petapoco.ExecuteScalar<int>(@"Select count(*) from Comment where [ObjID]=@0 and [ObjectType]=@1",objID,objecttype);
            petapoco.CloseSharedConnection();
            return c;
        }
    }
}
