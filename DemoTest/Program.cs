using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DemoTest
{
    class Program
    {
        static void Main(string[] args)
        {

            int x = 6;
            x -= -5;
            Console.WriteLine(x);
            Console.Read();


//            string testSTr = @"<p style=''margin-left:0px;'>2.  Commission:     i) HK$60per round turn for HK Tael Gold plus HK$6.0 per day for custodian fee</p>
//                            <p style='margin-left:100px;'>ii) HK$_per round turn for Loco London Gold & Silver</p>
//                            <p style='margin-left:100px;'>iii)RMB_per round turn for Renminbi Kilobar Gold</p>";

//            //(style=(\w|\W)*?)>((\w|\W)+?)_((\w|\W)+?)
//            Regex regex = new Regex(@"<p[^</]*?_[^</]*?</p>", RegexOptions.IgnoreCase);

//            MatchCollection collections = regex.Matches(testSTr);
//            foreach (Match eachCol in collections)
//            {
//                Console.Write(eachCol.Value);
//            }
//            Console.Read();
        }
    }
}
