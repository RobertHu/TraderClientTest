using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpInDepthAction
{
    public class NullableTest
    {
        public static void BoxAndUnboxTest()
        {
            Nullable<int> nullable = 5;
            object boxed = nullable;
            Console.WriteLine(boxed.GetType());
            int nomal = (int)boxed;
            Console.WriteLine(nomal);

            nullable = (Nullable<int>)boxed;
            Console.WriteLine(nullable);

            nullable = new Nullable<int>();
            boxed = nullable;
            Console.WriteLine(boxed == null);
            nullable = (Nullable<int>)boxed;
            Console.WriteLine(nullable.HasValue);

        }
    }
}
