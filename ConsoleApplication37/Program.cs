using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication37
{

    internal class Subect
    {
        public event EventHandler<EventArgsA> Placed;
        public event EventHandler<EventArgsB> Cutted;

        internal void OnPlaced(EventArgsA e)
        {
            this.RaiseCommon(this.Placed, e);
        }

        internal void OnCutted(EventArgsB e)
        {
            this.RaiseCommon(this.Cutted, e);
        }


        private void RaiseCommon(Delegate handler, EventArgs e)
        {
            handler.DynamicInvoke(this, e);
        }



    }

    internal class  EventArgsA: EventArgs
    {
        public EventArgsA(string name)
        {
            this.Name = name;
        }

        public string Name{get; private set;}
    }

    internal class EventArgsB: EventArgs
    {
        public EventArgsB(int value)
        {
            this.Value = value;
        }
        public int Value{get; private set;}
    }




    class Program
    {
        static void Main(string[] args)
        {
            Subect suject = new Subect();
            suject.Placed += (o, e) => Console.WriteLine(e.Name);
            suject.Cutted += (o, e) => Console.WriteLine(e.Value);

            suject.OnPlaced(new EventArgsA("robert"));
            suject.OnCutted(new EventArgsB(5));
        }

    }
}
