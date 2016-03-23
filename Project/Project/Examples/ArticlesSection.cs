using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Examples
{
	public class ArticlesSection: ConfigurationSection
	{
		#region Constructors
		static ArticlesSection()
		{
			s_propName = new ConfigurationProperty(
				"name",
				typeof(string),
				null,
				ConfigurationPropertyOptions.IsRequired
				);

			s_propArticles = new ConfigurationProperty(
				"",
				typeof(ArticlesElementCollection),
				null,
				ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection
				);

			s_properties = new ConfigurationPropertyCollection();

			s_properties.Add(s_propName);
			s_properties.Add(s_propArticles);
		}
		#endregion

		#region Fields
		private static ConfigurationPropertyCollection s_properties;
		private static ConfigurationProperty s_propName;
		private static ConfigurationProperty s_propArticles;
		#endregion

		#region Properties
		public string Name
		{
			get { return (string)base[s_propName]; }
			set { base[s_propName] = value; }
		}

		public ArticlesElementCollection Articles
		{
			get { return (ArticlesElementCollection)base[s_propArticles]; }
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