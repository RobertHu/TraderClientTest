using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication34
{

    internal struct Point
    {
        public int x;
        public int y;
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point() { x = 1, y = 2 });
            list[0].x = 3;
        }
    }
}
