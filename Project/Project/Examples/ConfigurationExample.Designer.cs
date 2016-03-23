namespace Examples
{
	partial class ConfigurationExample
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dgvSeriesArticles = new System.Windows.Forms.DataGridView();
			this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colState = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.colSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSeriesName = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.dgvSeriesArticles)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvSeriesArticles
			// 
			this.dgvSeriesArticles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dgvSeriesArticles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSeriesArticles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colDesc,
            this.colState,
            this.colSequence});
			this.dgvSeriesArticles.Location = new System.Drawing.Point(12, 52);
			this.dgvSeriesArticles.Name = "dgvSeriesArticles";
			this.dgvSeriesArticles.Size = new System.Drawing.Size(677, 222);
			this.dgvSeriesArticles.TabIndex = 0;
			this.dgvSeriesArticles.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvSeriesArticles_DataBindingComplete);
			this.dgvSeriesArticles.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSeriesArticles_CellValueChanged);
			this.dgvSeriesArticles.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSeriesArticles_DataError);
			// 
			// colName
			// 
			this.colName.DataPropertyName = "Name";
			this.colName.HeaderText = "Name";
			this.colName.Name = "colName";
			this.colName.Width = 150;
			// 
			// colDesc
			// 
			this.colDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colDesc.DataPropertyName = "Description";
			this.colDesc.HeaderText = "Description";
			this.colDesc.Name = "colDesc";
			// 
			// colState
			// 
			this.colState.DataPropertyName = "State";
			this.colState.HeaderText = "State";
			this.colState.Items.AddRange(new object[] {
            "Completed",
            "InProgress",
            "Pending"});
			this.colState.Name = "colState";
			// 
			// colSequence
			// 
			this.colSequence.DataPropertyName = "Sequence";
			this.colSequence.HeaderText = "Sequence";
			this.colSequence.Name = "colSequence";
			this.colSequence.Width = 60;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Article Series:";
			// 
			// txtSeriesName
			// 
			this.txtSeriesName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSeriesName.Location = new System.Drawing.Point(13, 26);
			this.txtSeriesName.Name = "txtSeriesName";
			this.txtSeriesName.Size = new System.Drawing.Size(676, 20);
			this.txtSeriesName.TabIndex = 2;
			this.txtSeriesName.TextChanged += new System.EventHandler(this.txtSeriesName_TextChanged);
			// 
			// ConfigurationExample
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(701, 286);
			this.Controls.Add(this.txtSeriesName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dgvSeriesArticles);
			this.Name = "ConfigurationExample";
			this.Text = "Configuration Example";
			this.Load += new System.EventHandler(this.ConfigurationExample_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgvSeriesArticles)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgvSeriesArticles;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSeriesName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
		private System.Windows.Forms.DataGridViewComboBoxColumn colState;
		private System.Windows.Forms.DataGridViewTextBoxColumn colSequence;
	}
}

