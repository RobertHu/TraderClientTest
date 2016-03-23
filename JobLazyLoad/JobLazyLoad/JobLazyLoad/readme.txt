Purpose: 

This small jobect is to demonstrate the power of lazyload while dealing 
with large amount of data in WPF.

Usage:
In the App.xml, if set StartupUri with Job.xaml, then it will load all the job
performance will be slow. If set StartupUrl as LazyJob.xmal, you will see significant 
performance enhancement with the LazyJob. (GetJobItemCollection() will take about 300 millseconds)

Key:
  1) LazyLoad JobItem only when needed
  2) overwrite TreeViewItem's expand event (TreeViewItem.ExpandedEvent)
  3) unsubscribe the event handle during unload, otherwise, it will cause memory leak
