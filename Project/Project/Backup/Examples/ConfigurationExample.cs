using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Examples
{
	public partial class ConfigurationExample : Form
	{
		public ConfigurationExample()
		{
			InitializeComponent();
		}


		private bool m_loading = true;

		private void ConfigurationExample_Load(object sender, EventArgs e)
		{
			ArticlesSection section = ConfigurationManager.GetSection("articleListing") as ArticlesSection;
			txtSeriesName.Text = section.Name;

			DataTable articles = new DataTable();
			articles.Columns.Add("Name", typeof(string));
			articles.Columns.Add("Description", typeof(string));
			articles.Columns.Add("State", typeof(string));
			articles.Columns.Add("Sequence", typeof(int));

			foreach (ArticleElement article in section.Articles)
			{
				DataRow row = articles.NewRow();
				row["Name"] = article.Name;
				row["Description"] = article.Description;
				row["State"] = article.State.ToString();
				row["Sequence"] = article.Sequence;
				articles.Rows.Add(row);
			}

			m_loading = true;
			dgvSeriesArticles.DataSource = articles;
		}

		private void dgvSeriesArticles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			m_loading = false;
		}
		
		private void dgvSeriesArticles_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			Debug.WriteLine(e.ToString());			
		}

		private void txtSeriesName_TextChanged(object sender, EventArgs e)
		{
			if (m_loading)
				return;

			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			ArticlesSection section = config.GetSection("articleListing") as ArticlesSection;
			section.Name = txtSeriesName.Text;

			config.Save();
		}

		private void dgvSeriesArticles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (m_loading)
				return;

			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			ArticlesSection section = config.GetSection("articleListing") as ArticlesSection;

			ArticleElement article = null;
			if (e.RowIndex < section.Articles.Count)
			{
				article = section.Articles[e.RowIndex];
			}
			else
			{
				article = new ArticleElement();
			}

			article.Name = Convert.ToString(dgvSeriesArticles.Rows[e.RowIndex].Cells[0].Value);
			article.Description = Convert.ToString(dgvSeriesArticles.Rows[e.RowIndex].Cells[1].Value);
			article.State = (ArticleState)Enum.Parse(typeof(ArticleState), Convert.ToString(dgvSeriesArticles.Rows[e.RowIndex].Cells[2].Value));
			article.Sequence = Convert.ToInt32(dgvSeriesArticles.Rows[e.RowIndex].Cells[3].Value);

			config.Save();
		}
	}
}