using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication26
{
    class Program
    {
        static void Main(string[] args)
        {
            var comparer = EqualityComparer<string>.Default;
            comparer.GetHashCode();
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "abcdefg");
            HashSet<int> set = new HashSet<int>();
            set.Add(1);
            set.Add(1);
            set.Add(1);
            set.Add(2);
            set.Add(2);
            List<int> list = new List<int>();
            list.Contains(1);
            list.Remove(2);
            Task task = new Task(() => Console.Out.WriteLine("hell"));
            task.Start();
        }
    }
}
