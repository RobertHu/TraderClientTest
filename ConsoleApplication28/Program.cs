using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication28
{
    public interface ISay
    {
        string Speak(string msg);
    }

    public abstract class Person : ISay
    {
        public virtual string Speak(string msg)
        {
            return "Person " + msg;
        }
    }

    public class Employee : Person
    {
        public override string Speak(string msg)
        {
            return base.Speak(msg) + "  Employee";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee();
            ISay say = employee;
            Console.WriteLine(say.Speak("Robert"));
        }
    }
}
