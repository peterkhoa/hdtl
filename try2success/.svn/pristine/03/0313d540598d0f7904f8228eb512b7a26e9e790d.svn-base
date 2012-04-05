using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public static class DateTimeLib
    {
        public static string getDatetime(DateTime date)//get Vietnamese datetime
        {
            return date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString() + "  " + date.ToShortTimeString();
        }

        public static int GetWeekNumber ( DateTime date )
        {
            return ( ( date.DayOfYear - 1 ) / 7 ) + 1;
        }

        public static DateTime [ ] GetDateRange ( int weekNumber , int year )
        {
            DateTime[] result = new DateTime [ 2 ];

            DateTime firstDate = new DateTime ( year , 1 , 1 );

            result [ 0 ] = firstDate.AddDays ( 7 * ( weekNumber - 1 ) -1 );

            result [ 1 ] = result [ 0 ].AddDays ( 7 );

            return result;
        }
    }
}
