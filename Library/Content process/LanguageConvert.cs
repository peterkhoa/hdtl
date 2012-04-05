using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Library
{
    public class LanguageConvert
    {
        public static bool BitSet(object _o, int bitmask)
        {
            int i = (int)_o;
            return (i & bitmask) != 0;
        }

        private static readonly string[] VietnameseSigns = new string [ ]
        {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵàậạá",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễế",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡờớỗọỏ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

        };

        private static readonly string SpecialCharacters = " –~!@#$%^&*()+/\\{}`'….,<>?:;\"|[]=“”";
        private static readonly string SpecialCharactersWithoutSpace = "~!@#$%^&*()+/\\{}`'….,<>?:;\"|[]=“”";

        public static string GetValidatedInput(string str)
        {
            str = str.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");

            string RegExStr = "<[ ]*script[ ]*>";
            Regex R = new Regex(RegExStr);

            str = R.Replace(str, " ");
            return str;
           
            
             
       }

        public static string RemoveSpecialCharacters(string str)
        {
            str = TrimRight(str);
            foreach (char c in SpecialCharacters)
            {
                str = str.Replace(c.ToString(), "-");
            }
            return str;
        }
        public static string RemoveSpecialCharactersWithoutSpace(string str)
        {
            str = TrimRight(str);
            foreach (char c in SpecialCharactersWithoutSpace)
            {
                str = str.Replace(c.ToString(), "");
            }
            return str;
        }

        public static string RemoveJustDiacritics(string str)
        {
            str = TrimRight(str);
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }

            return str;

        }

        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if (html == null || html == string.Empty)
                return string.Empty;
            if (allowHarmlessTags)
                return System.Text.RegularExpressions.Regex.Replace(html, "", string.Empty);
            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
        }

        public static string RemoveDiacritics ( string str )
        {
            str = TrimRight(str);
            for ( int i = 1 ; i < VietnameseSigns.Length ; i++ )
            {
                for ( int j = 0 ; j < VietnameseSigns [ i ].Length ; j++ )

                    str = str.Replace ( VietnameseSigns [ i ] [ j ] , VietnameseSigns [ 0 ] [ i - 1 ] );
            }

            foreach ( char c in SpecialCharacters )
            {
                str = str.Replace ( c.ToString ( ) , "-" );
            }
            str = str.Replace("‘", "");
            str = str.Replace("’", "");
            str = str.Replace("«", "");
            str = str.Replace("»", "");
            return str.Replace("-----", "-").Replace("----", "-").Replace("---", "-").Replace("--", "-");

        }
        public static string TrimRight(string str)
        {
            if (str == null)
                return "";
            int lenghtWhiteSpace = 0;
            for (int i = str.Length-1; i >= 0; i--)
            {
                if (str[i] != ' ')
                    break;
                else
                    lenghtWhiteSpace++;
            }
            return str.Substring(0, str.Length - lenghtWhiteSpace);
        }
        public static string RemoveSpace ( string text )
        {
            return text.Replace ( " " , "-" );
        }

        public static string GenerateDesc ( string text )
        {
            text = text.ToLower();
            return RemoveSpace ( RemoveDiacritics ( text ) );
        }

        public static bool IsVietnamese(string str)
        {
            str = TrimRight(str);
            int counter = 0;
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    if (str.Contains(VietnameseSigns[i][j]))
                        counter++;

                if (counter > 0)
                    //this is vietnameses
                    return true;
            }
            return false;
        }

        
    }
}
