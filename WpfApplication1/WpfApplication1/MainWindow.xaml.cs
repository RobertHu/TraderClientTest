using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<People> _RecordCol = new ObservableCollection<People>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            //Property p1 = new Property("Name", "Hu");
            //Property P2 = new Property("Country", "China");
            //Record r1 = new Record(p1, P2);
         
            //_RecordCol.Add(r1);
            //Property P3 = new Property("From", true);
            //Record r2 = new Record(p1, P2, P3);
            //_RecordCol.Add(r2);
            //r2.CanEdit = true;
            _RecordCol.Add(new People{ Name="Hu1",Men=true});
            _RecordCol.Add(new People{ Name="Hu2", Men=null});
        }
        public ObservableCollection<People> RecordCol
        {
            get
            {
                return _RecordCol;
            }
        }
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
           
        //        var columns =RecordCol.Last().Properties.Select((x, i) => new { Name = x.Name, Index = i }).ToArray();
        //        foreach (var column in columns)
        //        {
        //            var binding = new Binding(string.Format("Properties[{0}].Value", column.Index));
           
        //            dataGrid1.Columns.Add(new DataGridTextColumn() { Header = column.Name, Binding = binding });

        //        }
            
        //}

        private void dataGrid1_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Record r = e.Row.DataContext as Record;
            if (r.CanEdit == false)
            {
                e.Cancel = true;
            }
        }






    }

    public class People
    {
        public string Name { get; set; }
        public bool? Men { get; set; }
    }

    public class Property : INotifyPropertyChanged
    {
        public Property(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Record
    {
        private readonly ObservableCollection<Property> properties = new ObservableCollection<Property>();

        public Record(params Property[] properties)
        {
            foreach (var property in properties)
                Properties.Add(property);
        }

        public ObservableCollection<Property> Properties
        {
            get { return properties; }
        }
        public bool CanEdit { get; set; }
    }


}
