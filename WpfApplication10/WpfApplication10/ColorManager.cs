using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.ComponentModel;

namespace WpfApplication10
{
   public class ColorManager:DynamicObject, INotifyPropertyChanged
    {
       private Dictionary<string, object> _colors = new Dictionary<string, object>();
        public event PropertyChangedEventHandler PropertyChanged;
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _colors.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _colors[binder.Name] = value;
            string propertyName = string.Format("{0}", binder.Name);
            RaisePropertyChanged(propertyName);
            return true;
        }

        public void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public object this[string index]
        {
            get
            {
                object result;
                _colors.TryGetValue(index, out result);
                return result;
            }
            set
            {
                _colors[index] = value;
                string propertyName = string.Format("Item[{0}]", index);
                RaisePropertyChanged(propertyName);
            }
        }
    }
}
