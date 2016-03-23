using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharptLTest
{
    struct Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point(10, 10);
            object box = p;
            p.x = 20;
            Console.WriteLine(((Point)box).x);
            Console.ReadKey();
        }
    }
}
