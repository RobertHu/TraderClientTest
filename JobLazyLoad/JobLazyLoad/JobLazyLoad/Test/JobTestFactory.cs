using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using JobLazyLoad.Domain;

namespace JobLazyLoad.Test
{
	public static class JobTestFactory
	{

		public static IList<Domain.Job> CreateLargeLazyJobCollection()
		{
			return CreateLargeJobCollection(false);
		}

		public static IList<Domain.Job> CreateLargeJobCollection()
		{
			return CreateLargeJobCollection(true);
		}

		private static IList<Domain.Job> CreateLargeJobCollection(bool loadJobItemCollection)
		{
			IList<Domain.Job> jobCollection = new List<Domain.Job>();
			for (int jobIndex = 0; jobIndex < 100; jobIndex++)
			{
				Domain.Job job = new Domain.Job()
				          {
				          	Name = string.Format("Job {0}", jobIndex),
				          };
				if (loadJobItemCollection)
					job.JobItemCollection = CreateJobItemCollection();
				jobCollection.Add(job);
			}
			return jobCollection;
		}

		public static IList<JobItem> CreateJobItemCollection()
		{
			Thread.Sleep(50);
			IList<JobItem> jobItemCollection = new List<JobItem>();
			for (int jobItemIndex = 0; jobItemIndex < 100; jobItemIndex++)
			{
				JobItem item = new JobItem() {Name = string.Format("item {0}", jobItemIndex)};
				jobItemCollection.Add(item);
			}
			return jobItemCollection;
		}
	}
}
