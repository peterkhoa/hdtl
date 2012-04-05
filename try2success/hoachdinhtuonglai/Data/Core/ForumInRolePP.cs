using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace hoachdinhtuonglai.Data.Core
{
  

    public class ForumInRoleCollection : List<ForumInRole>
    {
        public ForumInRoleCollection() { }

        public ForumInRoleCollection(IEnumerable<ForumInRole> list) : base(list) { }
    }

    [DataObject]
    public static class ForumInRoleDA
    {
        static Database petapoco = ConnectionPP.getConnection();
        public static ForumInRoleCollection SelectAll()
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumInRoleCollection acc = new ForumInRoleCollection(petapoco.Fetch<ForumInRole>(@"Select * from ForumInRole"));
            petapoco.CloseSharedConnection();
            return acc;
        }

        public static ForumInRoleCollection SelectAllNonActive()
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumInRoleCollection acc = new ForumInRoleCollection(petapoco.Fetch<ForumInRole>(@"Select * from ForumInRole where Activate=0")); petapoco.CloseSharedConnection();
            return acc;
        }

       

        public static ForumInRole SelectByID(long id)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumInRole ac = petapoco.FirstOrDefault<ForumInRole>(@"Select * from ForumInRole where id=@0", id);
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static ForumInRoleCollection SelectByUsername(string username)
        {
            username = username.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumInRoleCollection ac = new ForumInRoleCollection(petapoco.Fetch<ForumInRole>(@"Select * from ForumInRole where Username=@0", username));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static ForumInRole SelectByAccount(long accountID)
        {
            
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumInRole ac = (petapoco.FirstOrDefault<ForumInRole>(@"Select * from ForumInRole where AccountID=@0", accountID));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static ForumInRole SelectByAccountnForum(long accountID,int forumid)
        {

            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumInRole ac = (petapoco.FirstOrDefault<ForumInRole>(@"Select * from ForumInRole where [AccountID]=@0 and [ForumID]=@1", accountID,forumid));
            petapoco.CloseSharedConnection();
            return ac;
        }

        public static ForumInRole SelectByUsernamenForum(string username, int forumid)
        {
            username = username.Trim().ToLower();
            petapoco = ConnectionPP.getConnection();

            petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumInRole ac = (petapoco.FirstOrDefault<ForumInRole>(@"Select * from ForumInRole where [Username]=@0 and [ForumID]=@1", username, forumid));
            petapoco.CloseSharedConnection();
            return ac;
        }

       
        public static ForumInRoleCollection SelectRoleSByPageSize(int page, int pagesize)
        {
            if (page == 0)
                page = 1;
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            ForumInRoleCollection acc = new ForumInRoleCollection(petapoco.Fetch<ForumInRole>(@"Select * from ForumInRole where Activate=1").Skip((page - 1) * pagesize).Take(page * pagesize));
            petapoco.CloseSharedConnection();
            return acc;
        }



        public static long Update(ForumInRole ForumInRole)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            long x = petapoco.Update("ForumInRole", "ID", ForumInRole, ForumInRole.ID); petapoco.CloseSharedConnection();          
            return x;
        }

        public static long Insert(ForumInRole ForumInRole)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            
            petapoco.ForceDateTimesToUtc = false;

            var x = petapoco.Insert("ForumInRole", "ID", true, ForumInRole);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }

        public static long Delete(ForumInRole ForumInRole)
        {
            petapoco = ConnectionPP.getConnection();
            
            petapoco.EnableAutoSelect = false;
            petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            long x = petapoco.Delete("ForumInRole", "ID", ForumInRole, ForumInRole.ID);
            petapoco.CloseSharedConnection();
            return x;
        }
    }
}
