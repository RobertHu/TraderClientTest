using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Examples
{
	public enum ArticleState
	{
		Completed,
		InProgress,
		Pending
	}

	public class ArticleElement: ConfigurationElement
	{
		#region Constructors
		static ArticleElement()
		{
			s_propName = new ConfigurationProperty(
				"name",
				typeof(string),
				null,
				ConfigurationPropertyOptions.IsRequired
				);

			s_propDesc = new ConfigurationProperty(
				"desc",
				typeof(string),
				null,
				ConfigurationPropertyOptions.None
				);

			s_propState = new ConfigurationProperty(
				"state",
				typeof(ArticleState),
				ArticleState.Pending,
				ConfigurationPropertyOptions.None
				);

			s_propSequence = new ConfigurationProperty(
				"sequence",
				typeof(int),
				0,
				ConfigurationPropertyOptions.IsRequired
				);

			s_properties = new ConfigurationPropertyCollection();

			s_properties.Add(s_propName);
			s_properties.Add(s_propDesc);
			s_properties.Add(s_propState);
			s_properties.Add(s_propSequence);
		}
		#endregion

		#region Fields
		private static ConfigurationPropertyCollection s_properties;
		private static ConfigurationProperty s_propName;
		private static ConfigurationProperty s_propDesc;
		private static ConfigurationProperty s_propState;
		private static ConfigurationProperty s_propSequence;
		#endregion

		#region Properties
		public string Name
		{
			get { return (string)base[s_propName]; }
			set { base[s_propName] = value; }
		}

		public string Description
		{
			get { return (string)base[s_propDesc]; }
			set { base[s_propDesc] = value; }
		}

		public ArticleState State
		{
			get { return (ArticleState)base[s_propState]; }
			set { base[s_propState] = value; }
		}

		public int Sequence
		{
			get { return (int)base[s_propSequence]; }
			set { base[s_propSequence] = value; }
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return s_properties;
			}
		}
		#endregion
	}
}
