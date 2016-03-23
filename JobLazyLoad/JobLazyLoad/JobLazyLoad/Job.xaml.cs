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

namespace JobLazyLoad
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Job : Window
	{
		private JobViewModel _viewModel;
		public Job()
		{
			InitializeComponent();
			InitializeViewModel();
		}

		private void InitializeViewModel()
		{
			_viewModel = new JobViewModel();
			this.DataContext = _viewModel;
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
		}

		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
		}
	}
}
