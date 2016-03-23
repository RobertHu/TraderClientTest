using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using JobLazyLoad.Domain;
using JobLazyLoad.Test;

namespace JobLazyLoad
{
	public class JobService
	{
		public IList<Domain.Job> GetJobCollection()
		{
			return JobTestFactory.CreateLargeJobCollection();
		}

		public IList<Domain.Job> GetLazyJobCollection()
		{
			return JobTestFactory.CreateLargeLazyJobCollection();
		}

		public IList<JobItem> GetJobItemCollection(int jobId)
		{
			return JobTestFactory.CreateJobItemCollection();
		}
	}
}
