using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hoachdinhtuonglai.Data.Core;

namespace hoachdinhtuonglai.Data.BL
{
    public static class Comment_order
    {
        public static string lastest_first = "lastest_first";
        public static string oldest_first = "oldest_first";
    }
    public class Enums
    {
        public static string Moderator = "Moderator";
        public static string Ad_head = "<script type=\"text/javascript\"><!-- \r\n ";
        public static string Ad_tail = " \r\n//--> </script> <script type=\"text/javascript\" src=\"http://?pagead2.googlesyndication.com/?pagead/show_ads.js\"></script>";

        public static string goals = "goals";
        public static string finishedgoals = "finishedgoal";
        public static string goal_types = "goal_types";
        public static string avatar = "http://dl.dropbox.com/u/1024821/DeltaViet/images/no_avatar.gif";
    }

    public class ObjectTypeEnums
    {
        public static string User = "user";
        public static string Goal = "goal";
        public static string Post = "post";
        public static string Status = "status";
        public static string Club = "club";
        public static string Kpi = "kpi";
        public static string Kpiteam = "kpiteam";
    }
    public class email_type
    {
        // public static string[] types = new string[] {wall,goal,reply_comment, };

        public static string goal_comment = "goal";
        public static string goal_comment_reply = "goal_reply";
        public static string story_comment = "story";
        public static string story_comment_reply = "story_comment_reply";
        public static string wall_comment = "wall";
        public static string following = "following";
        public static string comment_post = "comment_post";

    }


    public class GroupRole
    {
        public static string Leader = "leader";
        public static string Supporter = "supporter";
        public static string Member = "member";

    }
    public class ObjectTypeID
    {
        public static int User = 1;
        public static int Goal = 2;
        public static int Status = 3;
        public static int Post = 4;
        public static int Comment = 5;
        public static int Event = 6;
        public static int Club = 7;
        public static int School = 8;
        public static int Cheer = 9;
        public static int EditProfile = 10;
        public static int Following = 11;
        public static int kpi = 12;
        public static string get(int id)
        {
            if (id == 1)
                return "user";
            if (id == 2)
                return "goal";
            if (id == 3)
                return "status";
            if (id == 4)
                return "post";
            if (id == 5)
                return "comment";
            if (id == 6)
                return "event";
            if (id == 7)
                return "club";
            if (id == 8)
                return "school";
            if (id == 9)
                return "cheer";
            if (id == 10)
                return "editprofile";
            if (id == 11)
                return "following";
            if (id == 12)
                return "kpi";
            if (id == 13)
                return "kpiteam";

            return "";
        }

        public static int get(string type)
        {
            if (type == "user")
                return 1;
            if (type == "goal")
                return 2;
            if (type == "status")
                return 3;
            if (type == "post")
                return 4;
            if (type == "comment")
                return 5;
            if (type == "event")
                return 6;
            if (type == "club")
                return 7;
            if (type == "school")
                return 8;
            if (type == "cheer")
                return 9;
            if (type == "editprofile")
                return 10;
            if (type == "following")
                return 11;

            if (type == "kpi")
                return 12;

            if (type == "kpiteam")
                return 13;

            return 0;
        }


    }
    public class ActionTypeID
    {
        public static int Comment = 1;
        public static int Post = 2;
        public static int Support = 3;
        public static int Want = 4;
        public static int Finish = 5;
        public static int Edit = 6;


    }



    public class PostAttribute
    {
        public static string EventTime = "event_time";
        public static string EventPlace = "event_place";
        public static string EventOrg = "event_org";
        public static string EventTheme = "event_theme";
    }

    public class RolesAccess
    {
        public static string Admin = "Admin";
        public static string Member = "Member";
    }

    public enum AccessFlags
    {
        ReadAccess = 1,
        PostAccess = 2,
        ReplyAccess = 4,
        PriorityAccess = 8,
        PollAccess = 16,
        VoteAccess = 32,
        ModeratorAccess = 64,
        EditAccess = 128,
        DeleteAccess = 256,
        UploadAccess = 512
    }



    public enum ForumFlags : int
    {
        Locked = 1,
        Hidden = 2,
        IsTest = 4,
        Moderated = 8
    }

    public enum GroupFlags : int
    {
        IsAdmin = 1,
        IsGuest = 2,
        IsStart = 4,
        IsModerator = 8
    }

    public class PostType
    {
        
        public static string Goal = "goal"; // --> bai viet chia se, cũng l`a  nh^ật ký lu^on
        public static string GoalFinished = "goalfinished";
        public static string GoalComment = "goalcomment";
        //public static string StoryComment = "storycomment";
        public static string Connection = "connection";
        public static string ConnectionHome = "connectionhome";
        public static string ConnectionContact = "connectioncontact";
        public static string ConnectionComment = "connectioncoment";
        public static string Event = "event"; //--> su kien
        public static string EventComment = "eventcomment";
        public static string VisitorComment = "visitorcomment";
        public static string Discussion = "discussion"; //--> bai viet thao luan trong cong dong

        public static string DiscussionComment = "discussioncomment";
        public static string ClubComment = "clubcomment";
        public static string ClubFoundedDate = "clubfoundeddate";
        public static string ClubAction = "clubaction";
        public static string ClubContact = "clubcontact";
        public static string ClubLeader = "club_leader";
        public static string Announcement = "announcement";
    }

    public class CategoriesEnums
    {
        public static string Discussion = "discussion";
       
        public static string Connection = "connection";
        public static string Board = "board";
        public static string community = "community";
        public static string group = "group";
        public static string goal = "goal";
        public static string sharing = "sharing";
        public static string job = "job";
        public static string gallery = "gallery";
        public static string english = "english";

        public static int idCate_sharing = 1;
        public static int idCate_job = 2;
        public static int idCate_gallery = 3;
        public static int idCate_group = 4;
        public static int idCate_english = 5;

        public static string getCateName(int cateid)
        {
            if (cateid == 1)
                return "chia-se";
            if (cateid == 2)
                return "viec-lam";
            if (cateid == 3)
                return "hinh-anh";
            if (cateid == 4)
                return "hoi-nhom";
            if (cateid == 5)
                return "tieng-anh";
            return "";
        }
    }

    public enum TopicFlags : int
    {
        Locked = 1,
        Deleted = 8
    }

    public enum UserFlags : int
    {
        IsHostAdmin = 1,
        Approved = 2,
        IsGuest = 4
    }

}