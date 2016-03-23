using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobLazyLoad.Domain
{
	public class JobItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsSelected { get; set; }
	}
}
