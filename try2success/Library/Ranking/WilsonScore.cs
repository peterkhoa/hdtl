/*
 * 
 *
 * 
 * 
 * Reddit ranking http://amix.dk/blog/post/19588
 * 
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Ranking
{
    public class WilsonScore
    {
        private static double pnormaldist(double qn)
        {
            double[] b = { 1.570796288, 0.03706987906, -0.8364353589e-3, -0.2250947176e-3, 
                         0.6841218299e-5, 0.5824238515e-5, -0.104527497e-5, 
                         0.8360937017e-7, -0.3231081277e-8, 0.3657763036e-10, 
                         0.6936233982e-12 };

            if (qn < 0.0 || 1.0 < qn)
                return 0.0;

            if (qn == 0.5)
                return 0.0;

            double w1 = qn;
            if (qn > 0.5)
                w1 = 1.0 - w1;
            double w3 = -Math.Log(4.0 * w1 * (1.0 - w1));
            w1 = b[0];
            int i = 1;
            for (; i < 11; i++)
                w1 += b[i] * Math.Pow(w3, i);

            if (qn > 0.5)
                return Math.Sqrt(w1 * w3);
            return -Math.Sqrt(w1 * w3);
        }

        public static double ranking_score(int positive_rate, int total_rate)
        {
            double power = 0.1;//95% chance that your lower bound is correct, 0.05 to have a 97.5% chance etc.
            return ranking_score(positive_rate, total_rate, power);
        }


        public static double reddit_ranking(int positive_vote, int down_vote, DateTime post_time)
        {
            DateTime begin_date = DateTime.Parse("3/8/2009");
            TimeSpan t = post_time - begin_date;
            int x = positive_vote - down_vote;

            double y = 0;

            if (x >= 0)
                y = 1;
            else if (x < 0)
                y= -1;


            int z = 1;
            if ( Math.Abs(x) > 1)
            {
                z = Math.Abs(x);


            }

            double ts = t.TotalSeconds;


            return Math.Log10(z)/Math.Log10(3) + y * ts / (3600*12.5);
        }

        public static double ranking_score(int positive_rate, int total_rate, double power)
        {
            if (total_rate == 0)
                return 0.0;
            double z = pnormaldist(1 - power / 2);
            double phat = 1.0 * positive_rate / total_rate;
            return (phat + z * z / (2 * total_rate) - z * Math.Sqrt((phat * (1 - phat) + z * z / (4 * total_rate)) / total_rate)) / (1 + z * z / total_rate);
        }

      
    }
}
