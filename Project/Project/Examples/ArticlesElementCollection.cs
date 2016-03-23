using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Examples
{
	public class ArticlesElementCollection: ConfigurationElementCollection
	{
		#region Constructor
		public ArticlesElementCollection()
		{
		}
		#endregion

		#region Properties
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}
		protected override string ElementName
		{
			get
			{
				return "article";
			}
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return new ConfigurationPropertyCollection();
			}
		}
		#endregion

		#region Indexers
		public ArticleElement this[int index]
		{
			get
			{
				return (ArticleElement)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

		public ArticleElement this[string name]
		{
			get
			{
				return (ArticleElement)base.BaseGet(name);
			}
		}
		#endregion

		#region Methods
		public void Add(ArticleElement item)
		{
			base.BaseAdd(item);
		}

		public void Remove(ArticleElement item)
		{
			base.BaseRemove(item);
		}

		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
		#endregion

		#region Overrides
		protected override ConfigurationElement CreateNewElement()
		{
			return new ArticleElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as ArticleElement).Name;
		}
		#endregion
	}
}
