using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobLazyLoad.Domain;

namespace JobLazyLoad
{
	public class JobViewModel
	{
		public int CurrentJobId { get; set; }
		public IList<Domain.Job> JobCollection
		{
			get { return new JobService().GetJobCollection(); }
		}

		public IList<Domain.Job> LazyJobCollection
		{
			get { return new JobService().GetLazyJobCollection(); }
		}

		public IList<JobItem> JobItemCollection
		{
			get { return new JobService().GetJobItemCollection(CurrentJobId); }
		}
	}
}
