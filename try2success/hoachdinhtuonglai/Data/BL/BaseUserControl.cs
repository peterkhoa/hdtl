
using System.Web;
using hoachdinhtuonglai.Data.Core;
using System.Web.UI.WebControls;





namespace hoachdinhtuonglai.Data.BL
{

    public class BaseUserControl : System.Web.UI.UserControl
    {

        private static Account _current_user = null;
        private static string _comment_type = "facebook";//default
        private static bool _isAdmin = false;

        public static bool IsAdmin
        {
            get { return BaseUserControl._isAdmin; }
            set { BaseUserControl._isAdmin = value; }
        }
        private static bool _isManager = false;

        public static bool IsManager
        {
            get { return BaseUserControl._isManager; }
            set { BaseUserControl._isManager = value; }
        }

        public static string Comment_type
        {
            get
            {

                HttpContext context = HttpContext.Current;
                HttpCookie type = context.Request.Cookies.Get("comment_type");

                if (type != null && !string.IsNullOrEmpty(type.Value))
                {
                    _comment_type = type.Value;
                }


                return BaseUserControl._comment_type;
            }
            set
            {
                HttpCookie type = new HttpCookie("comment_type", value);


                type.Expires.AddDays(68);
                HttpContext context = HttpContext.Current;
                context.Request.Cookies.Add(type);
            }
        }

        public static Account Current_user
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context.User.Identity.IsAuthenticated)// && ( _current_user== null || _current_user.ID==0))
                {
                    if (context.User.IsInRole("admin"))
                    {
                        _isAdmin = true;
                    }
                    if (context.User.IsInRole("manger"))
                    {
                        _isManager = true;
                    }
                    return AccountDA.SelectByUsername(context.User.Identity.Name);

                }
                else
                {
                    _current_user = new Account();
                    _current_user.ID = 0;
                }
                return _current_user;
            }
            set { _current_user = value; }
        }

        public static void setMetaDescription(string description){
            //Literal desc = (Literal)this.FindControl("robots");
            //desc.Text = "<meta name=\"robots\" content='" + description + ", follow' />";
        }


    }
}
