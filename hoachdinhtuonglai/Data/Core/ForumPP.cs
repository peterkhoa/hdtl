using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace hoachdinhtuonglai.Data.Core
{
 

    public class ForumCollection : List<Forum>
    {
        public ForumCollection() { }
        public ForumCollection(IEnumerable<Forum> list) : base(list) { }
    }

    [DataObject]
    public static class ForumDA
    {
        static Database petapoco = ConnectionPP.getConnection();
        public static ForumCollection SelectAll()
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumCollection acc = new ForumCollection(petapoco.Fetch<Forum>(@"Select * from Forum")); petapoco.CloseSharedConnection();
            return acc;
        }

        public static ForumCollection SelectAllNonActive()
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumCollection acc = new ForumCollection(petapoco.Fetch<Forum>(@"Select * from Forum where Activate=0")); petapoco.CloseSharedConnection();
            return acc;
        }



        public static Forum SelectByID(long id)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Forum ac = petapoco.FirstOrDefault<Forum>(@"Select * from Forum where id=@0", id); petapoco.CloseSharedConnection();

            return ac;
        }

        public static ForumCollection SelectByForumNameSlug(string name)
        {
            name = name.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumCollection ac = new ForumCollection(petapoco.Fetch<Forum>(@"Select * from Forum where ForumNameSlug=@0", name)); petapoco.CloseSharedConnection();

            return ac;
        }

        public static Forum SelectByCateID(long cateid)
        {

            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            Forum ac = (petapoco.FirstOrDefault<Forum>(@"Select * from Forum where CateID=@0", cateid)); petapoco.CloseSharedConnection();

            return ac;
        }


        public static ForumCollection SelectForumSByPageSize(int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumCollection acc = new ForumCollection(petapoco.Fetch<Forum>(page,pagesize,@"Select * from Forum where Activate=1"));

            return acc;
        }

        public static long Update(Forum Forum)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            long x = petapoco.Update("Forum", "ID", Forum, Forum.ID);
            petapoco.CloseSharedConnection();
            return x;
        }

        public static long Insert(Forum Forum)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;

            petapoco.ForceDateTimesToUtc = false;

            var x = petapoco.Insert("Forum", "ID", true, Forum);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }

        public static long Delete(Forum Forum)
        {
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;
            Forum.Active = false;
            string[] cols = { "Active" };
            long x = petapoco.Update("Forum", "ID", Forum, Forum.ID, cols);
            petapoco.CloseSharedConnection();
            return x;
        }
    }
}
