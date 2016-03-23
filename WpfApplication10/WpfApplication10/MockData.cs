using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfApplication10
{
   public class MockData:INotifyPropertyChanged
    {
       private string _color;
       public string Name { get; set; }

       public string Color
       {
           get { return this._color; }
           set
           {
               if (this._color != value)
               {
                   this._color = value;
                   OnChanged("Color");
               }
           }
       }

       public event PropertyChangedEventHandler PropertyChanged;
       protected void OnChanged(string name)
       {
           if (this.PropertyChanged != null)
           {
               this.PropertyChanged(this, new PropertyChangedEventArgs(name));
           }
       }
    }
}
