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
	public partial class LazyJob : Window
	{
		private object _dummyNode = null;
		private JobViewModel _viewModel;
		public LazyJob()
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
			JobTree.AddHandler(TreeViewItem.ExpandedEvent,
				new RoutedEventHandler(OnTreeItemExpanded));

			LoadRootNodes();
		}

		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			JobTree.RemoveHandler(TreeViewItem.ExpandedEvent,
				new RoutedEventHandler(OnTreeItemExpanded));
		}

		private void OnTreeItemExpanded(object sender, RoutedEventArgs e)
		{
			TreeViewItem item = e.OriginalSource as TreeViewItem;
			if (item != null && item.Items.Count == 1 && item.Items[0] == _dummyNode)
			{
				item.Items.Clear();
				Domain.Job job = item.Header as Domain.Job;
				foreach (var acquisitionItem in _viewModel.JobItemCollection)
				{
					TreeViewItem subItem = new TreeViewItem();
					subItem.Header = acquisitionItem;
					subItem.HeaderTemplate = FindResource(
						"JobItemTemplate") as DataTemplate;
					item.Items.Add(subItem);
				}
			}
		}

		private void LoadRootNodes()
		{
			JobTree.Items.Clear();
			foreach (Domain.Job job in _viewModel.LazyJobCollection)
			{
				TreeViewItem item = new TreeViewItem();
				item.Header = job;
				item.HeaderTemplate = FindResource("JobTemplate") as DataTemplate;
				item.Items.Add(_dummyNode);
				JobTree.Items.Add(item);
			}
		}
	}
}
