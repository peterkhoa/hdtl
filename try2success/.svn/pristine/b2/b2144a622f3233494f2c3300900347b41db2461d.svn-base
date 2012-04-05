using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Web.Caching;
using System.Web;

namespace hoachdinhtuonglai.Data.Core
{
    public partial class Post
    {
       

        public string getLinkAvatar()
        {
            Account a = AccountDA.SelectByID(_AccountID);
            if (a != null)
            {
                return a.getAvatar();
            }
            return "/images/main/noavatar.png";
        }
    }

    public class PostCollection : List<Post>
    {
        public PostCollection() { }
        public PostCollection(IEnumerable<Post> list) : base(list) { }
    }

    [DataObject]
    public static class PostDA
    {
        static Database petapoco;
        public static Cache cache = HttpContext.Current.Cache;

        public static PostCollection SelectAll()
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post"));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static PostCollection SelectAllActiveNotSpam()
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where Active=1 and Spam=0"));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static PostCollection SelectAllSpam()
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where Spam=1"));

            petapoco.CloseSharedConnection();
            return acc;
        }

        public static Post SelectByID(long id)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post ac = petapoco.FirstOrDefault<Post>(@"Select * from Post where id=@0", id);
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static Post SelectByUrl(string url)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post ac = petapoco.FirstOrDefault<Post>(@"Select * from Post where [Url]=@0", url);
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static Post SelectByUrlID(long urlid)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post ac = petapoco.FirstOrDefault<Post>(@"Select * from Post where [UrlID]=@0", urlid);
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByUsername(string username)
        {
            username = username.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where Username=@0", username));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByUsername(long accountID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where AccountID=@0", accountID));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByObjectID(string type)
        {
            type = type.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where ObjectID = @0", type));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByCateID(int CateID)
        {

            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where [CateID] = @0 and [Active]=1 order by ID desc", CateID));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByCateID(int CateID,int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(page, pagesize, @"Select * from Post where [CateID] = @0 order by [CreatedDate] desc", CateID));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByCateNameSlug(string cateNameSlug)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where [CateNameSlug] = @0 order by [CreatedDate] desc", cateNameSlug));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectPostRelation(int cateID, int objID,long oldPostID)
        {

            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where [CateID] = @0 and [ObjectID]=@1 and [ID]!=@2 order by [CreatedDate] desc", cateID, objID, oldPostID));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectPostRelation(int cateID, int objID, long oldPostID, int num)
        {

            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(@"Select top (@3) * from Post where [CateID] = @0 and [ObjectID]=@1 and [ID]!=@2 order by [CreatedDate] desc", cateID, objID, oldPostID, num));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByCateNameSlug(string cateNameSlug, int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(page, pagesize, @"Select * from Post where [CateNameSlug] = @0 order by [CreatedDate] desc", cateNameSlug));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByCateNameSlugRandom(string cateNameSlug, int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection ac = new PostCollection(petapoco.Fetch<Post>(page, pagesize, @"Select * from Post where [CateNameSlug] = @0 order by [CreatedDate] desc", cateNameSlug));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static PostCollection SelectByDate(DateTime date)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where CreatedDate like @0", date));
            petapoco.CloseSharedConnection();
            return acc;
        }

         public static Post SelectPostNewest()
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post acc = (petapoco.FirstOrDefault<Post>(@"Select * from Post order by CreatedDate desc"));
            petapoco.CloseSharedConnection();
            return acc;
        }

         public static PostCollection SelectListPostNewest(int num)
         {
             petapoco = ConnectionPP.getConnection();
             petapoco.EnableAutoSelect = false;
             petapoco.ForceDateTimesToUtc = false;

             PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select top (@0) * from Post where [Active]=1  order by CreatedDate desc, ID desc",num));
             petapoco.CloseSharedConnection();
             return acc;
         }

         public static PostCollection SelectListPostNewest()
         {
             petapoco = ConnectionPP.getConnection();
             petapoco.EnableAutoSelect = false;
             petapoco.ForceDateTimesToUtc = false;

             PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post order by CreatedDate desc"));
             petapoco.CloseSharedConnection();
             return acc;
         }

        public static Post SelectPostNewest(long CateID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post acc = (petapoco.FirstOrDefault<Post>(@"Select * from Post where [CateID]=@0 order by CreatedDate desc", CateID));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static PostCollection SelectPostNewest(long CateID, long pagesize, long page)
        {
            if(page==0)
                page=1;
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(page,pagesize,"Select * from Post where [CateID]=@0 order by CreatedDate desc",CateID));
            
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static Post SelectHotPostVoting(int CateID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post acc = (petapoco.FirstOrDefault<Post>(@"Select * from Post where [CateID]=@0 order by [Vote] desc", CateID));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static Post SelectHotPostVotingObjectID(int objID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post acc = (petapoco.FirstOrDefault<Post>(@"Select * from Post where [ObjectID]=@0 order by [Vote] desc", objID));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static Post SelectHotPostVotingObjectNameSlug(string objectname)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post acc = (petapoco.FirstOrDefault<Post>(@"Select * from Post where [ObjectNameSlug]=@0 order by [Vote] desc", objectname));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static Post SelectHotPostView(long CateID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post acc = (petapoco.FirstOrDefault<Post>(@"Select * from Post where [CateID]=@0 order by [View] desc", CateID));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static Post SelectHotPostViewObjectID(int objID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post acc = (petapoco.FirstOrDefault<Post>(@"Select * from Post where [ObjectID]=@0 order by [View] desc", objID));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static Post SelectHotPostViewObjectNameSlug(string objectname)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Post acc = (petapoco.FirstOrDefault<Post>(@"Select * from Post where [ObjectNameSlug]=@0 order by [View] desc", objectname));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static PostCollection SelectByPageSize(int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select * from Post where [Active]=1").Skip((page - 1) * pagesize).Take(page * pagesize));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static PostCollection TopPostHotView(int num)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Query<Post>(@"Select top (@0) * from Post where [Active]=1 order by [View] desc",num));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static PostCollection TopPostHotView(int num, int CateID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select top @0 * from Post where [Active]=1 and [CateID]=@1 order by [View] desc",num, CateID));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static PostCollection TopPostHotVote(int num)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select top @0 * from Post where [Active]=1 order by [Vote] desc", num));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static PostCollection TopPostHotVote(int num, int CateID)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            PostCollection acc = new PostCollection(petapoco.Fetch<Post>(@"Select top @0 * from Post where [Active]=1 and [CateID]=@1 order by [Vote] desc", num,CateID));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static long Update(Post Post)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            
            Post.ModifiedDate = DateTime.Now;
            long x = petapoco.Update("Post", "ID", Post, Post.ID);
            petapoco.CloseSharedConnection();
            return x;
        }

        public static long Insert(Post Post)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;
            Post.CreatedDate = DateTime.Now;
            Post.ModifiedDate = DateTime.Now;
            

            var x = petapoco.Insert("Post", "ID", true, Post);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }

        public static long Delete(Post Post)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            
            Post.Active = false;
            long x = Update(Post);
            petapoco.CloseSharedConnection();
            return x;
        }

        public static long DeletePermament(Post Post)
        {
            petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            
            long x = petapoco.Delete("Post", "ID", Post, Post.ID);
            petapoco.CloseSharedConnection();
            return x;
        }

        public static int countAll()
        {
            petapoco = ConnectionPP.getConnection();
            int c =  petapoco.ExecuteScalar<int>(@"Select count(*) from Post");
            petapoco.CloseSharedConnection();
            return c; 
        }

        public static int countAll(int cateID)
        {
            int c = petapoco.ExecuteScalar<int>(@"Select count(*) from Post where [CateID]=@0",cateID);
            petapoco.CloseSharedConnection();
            return c;
        }

        public static int countAll(string cateNameSlug)
        {
            int c = petapoco.ExecuteScalar<int>(@"Select count(*) from Post where [CateNameSlug]=@0", cateNameSlug);
            petapoco.CloseSharedConnection();
            return c;
        }
    }
}
