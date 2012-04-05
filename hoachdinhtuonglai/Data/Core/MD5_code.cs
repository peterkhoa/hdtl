using System;
using System.Data;
using System.Configuration;
using System.Web;

using System.Security.Cryptography;
using System.Text;


/// <summary>
/// Summary description for MD5_code
/// </summary>

namespace hoachdinhtuonglai.Data.Core
{
    public class MD5_code
    {
        private string st;

        public string St
        {
            get { return st; }
            set { st = value; }
        }
        public MD5_code()
        {
            st = "";
        }

        public string getData()
        {
            return st;
        }
        public void setData(string s)
        {
            if (s != null)
                st = s;
        }

        public static string maHoa(string chuoi)
        {
            MD5 obj = MD5.Create();
            Byte[] mang = obj.ComputeHash(Encoding.UTF8.GetBytes(chuoi));
            StringBuilder kq = new StringBuilder();
            foreach (Byte x in mang)
                kq.Append(x.ToString());
            return kq.ToString();
        }

        public string maHoa()
        {
            MD5 obj = MD5.Create();
            Byte[] mang = obj.ComputeHash(Encoding.UTF8.GetBytes(st));
            StringBuilder kq = new StringBuilder();
            foreach (byte x in mang)
                kq.Append(x.ToString("x"));
            return kq.ToString();
        }
    }
}
