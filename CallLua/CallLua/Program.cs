using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;
using System.Reflection;
namespace CallLua
{
    class Program
    {
        private  bool isRunning = true;
        public  Lua luaVm = new Lua();
        public void Run()
        {
            while (isRunning)
            {
                string input = Console.ReadLine();
                if (input == "quit") isRunning = false;
                try
                {
                    Console.WriteLine();
                    luaVm.DoString(input);
                }
                catch (Exception ex)
                {
                }
            }
        }
        public void Stop()
        {
            isRunning = false;
        }

        static void Main(string[] args)
        {
            Program pro = new Program();
            MethodInfo mi =pro.GetType().GetMethod("Stop");
            pro.luaVm.RegisterFunction("sss", pro, mi);
            pro.Run();
           
        }
    }
}
