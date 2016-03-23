using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobLazyLoad.Domain
{
	public class Job
	{
		public IList<JobItem> JobItemCollection { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsSelected { get; set; }
	}
}
