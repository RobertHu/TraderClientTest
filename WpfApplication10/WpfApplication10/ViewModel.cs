using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WpfApplication10
{
   public class ViewModel
    {
       private ObservableCollection<MockData> _dataCol = new ObservableCollection<MockData>();
       public ViewModel()
       {
           this.DataCol.Add(new MockData { Name="n1",Color="Red"});
           this.DataCol.Add(new MockData { Name = "n2", Color = "Green" });
           this.DataCol.Add(new MockData { Name = "n3", Color = "Yellow" });
       }
       public ObservableCollection<MockData> DataCol
       {
           get
           {
               return _dataCol;
           }
       }
    }
}
