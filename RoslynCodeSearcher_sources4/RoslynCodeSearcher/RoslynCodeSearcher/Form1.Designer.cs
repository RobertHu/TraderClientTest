namespace RoslynCodeSearcher
{
    partial class Form1
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtTextToSearch = new System.Windows.Forms.TextBox();
            this.rbSearchTextInMethod = new System.Windows.Forms.RadioButton();
            this.rbSearchCallers = new System.Windows.Forms.RadioButton();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.rbSearchMethods = new System.Windows.Forms.RadioButton();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdateSolutionList = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtInclude = new System.Windows.Forms.TextBox();
            this.btnNewTab = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblDone = new System.Windows.Forms.Label();
            this.krbTabControl1 = new KRBTabControl.KRBTabControl();
            this.tabPageEx1 = new KRBTabControl.TabPageEx();
            this.grpSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.krbTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(289, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 30;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTextToSearch
            // 
            this.txtTextToSearch.Location = new System.Drawing.Point(4, 23);
            this.txtTextToSearch.Name = "txtTextToSearch";
            this.txtTextToSearch.Size = new System.Drawing.Size(279, 20);
            this.txtTextToSearch.TabIndex = 10;
            // 
            // rbSearchTextInMethod
            // 
            this.rbSearchTextInMethod.AutoSize = true;
            this.rbSearchTextInMethod.Location = new System.Drawing.Point(6, 17);
            this.rbSearchTextInMethod.Name = "rbSearchTextInMethod";
            this.rbSearchTextInMethod.Size = new System.Drawing.Size(129, 17);
            this.rbSearchTextInMethod.TabIndex = 21;
            this.rbSearchTextInMethod.TabStop = true;
            this.rbSearchTextInMethod.Text = "Search text in Method";
            this.rbSearchTextInMethod.UseVisualStyleBackColor = true;
            // 
            // rbSearchCallers
            // 
            this.rbSearchCallers.AutoSize = true;
            this.rbSearchCallers.Location = new System.Drawing.Point(6, 39);
            this.rbSearchCallers.Name = "rbSearchCallers";
            this.rbSearchCallers.Size = new System.Drawing.Size(83, 17);
            this.rbSearchCallers.TabIndex = 22;
            this.rbSearchCallers.TabStop = true;
            this.rbSearchCallers.Text = "Search calls";
            this.rbSearchCallers.UseVisualStyleBackColor = true;
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.rbSearchMethods);
            this.grpSearch.Controls.Add(this.rbSearchTextInMethod);
            this.grpSearch.Controls.Add(this.rbSearchCallers);
            this.grpSearch.Location = new System.Drawing.Point(374, 6);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(151, 85);
            this.grpSearch.TabIndex = 20;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search method";
            // 
            // rbSearchMethods
            // 
            this.rbSearchMethods.AutoSize = true;
            this.rbSearchMethods.Location = new System.Drawing.Point(6, 60);
            this.rbSearchMethods.Name = "rbSearchMethods";
            this.rbSearchMethods.Size = new System.Drawing.Size(102, 17);
            this.rbSearchMethods.TabIndex = 23;
            this.rbSearchMethods.TabStop = true;
            this.rbSearchMethods.Text = "Search methods";
            this.rbSearchMethods.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(16, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 31;
            this.btnBrowse.Text = "Browse ...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdateSolutionList);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Location = new System.Drawing.Point(541, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 85);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Starting Directory";
            // 
            // btnUpdateSolutionList
            // 
            this.btnUpdateSolutionList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateSolutionList.Location = new System.Drawing.Point(16, 54);
            this.btnUpdateSolutionList.Name = "btnUpdateSolutionList";
            this.btnUpdateSolutionList.Size = new System.Drawing.Size(142, 23);
            this.btnUpdateSolutionList.TabIndex = 32;
            this.btnUpdateSolutionList.Text = "Update solution List";
            this.btnUpdateSolutionList.UseVisualStyleBackColor = true;
            this.btnUpdateSolutionList.Click += new System.EventHandler(this.btnUpdateSolutionList_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFilter);
            this.groupBox2.Location = new System.Drawing.Point(725, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 85);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter on words in filename";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(6, 16);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFilter.Size = new System.Drawing.Size(255, 61);
            this.txtFilter.TabIndex = 34;
            this.txtFilter.Text = "unittest";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtInclude);
            this.groupBox3.Location = new System.Drawing.Point(1002, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 85);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Exclude files containing word in filename";
            // 
            // txtInclude
            // 
            this.txtInclude.Location = new System.Drawing.Point(9, 14);
            this.txtInclude.Multiline = true;
            this.txtInclude.Name = "txtInclude";
            this.txtInclude.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInclude.Size = new System.Drawing.Size(255, 63);
            this.txtInclude.TabIndex = 35;
            // 
            // btnNewTab
            // 
            this.btnNewTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewTab.Location = new System.Drawing.Point(289, 68);
            this.btnNewTab.Name = "btnNewTab";
            this.btnNewTab.Size = new System.Drawing.Size(75, 23);
            this.btnNewTab.TabIndex = 33;
            this.btnNewTab.Text = "New tab";
            this.btnNewTab.UseVisualStyleBackColor = true;
            this.btnNewTab.Click += new System.EventHandler(this.btnNewTab_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblDone);
            this.splitContainer1.Panel1.Controls.Add(this.txtTextToSearch);
            this.splitContainer1.Panel1.Controls.Add(this.btnNewTab);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.grpSearch);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.krbTabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1563, 454);
            this.splitContainer1.SplitterDistance = 98;
            this.splitContainer1.TabIndex = 38;
            // 
            // lblDone
            // 
            this.lblDone.AutoSize = true;
            this.lblDone.Location = new System.Drawing.Point(10, 50);
            this.lblDone.Name = "lblDone";
            this.lblDone.Size = new System.Drawing.Size(10, 13);
            this.lblDone.TabIndex = 37;
            this.lblDone.Text = " ";
            // 
            // krbTabControl1
            // 
            this.krbTabControl1.AllowDrop = true;
            this.krbTabControl1.BackgroundHatcher.HatchType = System.Drawing.Drawing2D.HatchStyle.DashedVertical;
            this.krbTabControl1.Controls.Add(this.tabPageEx1);
            this.krbTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.krbTabControl1.IsDocumentTabStyle = true;
            this.krbTabControl1.IsDrawTabSeparator = true;
            this.krbTabControl1.ItemSize = new System.Drawing.Size(0, 26);
            this.krbTabControl1.Location = new System.Drawing.Point(0, 0);
            this.krbTabControl1.Name = "krbTabControl1";
            this.krbTabControl1.SelectedIndex = 0;
            this.krbTabControl1.Size = new System.Drawing.Size(1563, 352);
            this.krbTabControl1.TabGradient.ColorEnd = System.Drawing.Color.Gainsboro;
            this.krbTabControl1.TabIndex = 38;
            this.krbTabControl1.UpDownStyle = KRBTabControl.KRBTabControl.UpDown32Style.Default;
            this.krbTabControl1.TabPageClosing += new System.EventHandler<KRBTabControl.KRBTabControl.SelectedIndexChangingEventArgs>(this.krbTabControl1_TabPageClosing);
            // 
            // tabPageEx1
            // 
            this.tabPageEx1.BackColor = System.Drawing.Color.White;
            this.tabPageEx1.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPageEx1.Guid = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.tabPageEx1.Location = new System.Drawing.Point(1, 32);
            this.tabPageEx1.Name = "tabPageEx1";
            this.tabPageEx1.Size = new System.Drawing.Size(1561, 298);
            this.tabPageEx1.TabIndex = 0;
            this.tabPageEx1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1563, 454);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "RoslynCodeSearcher";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.krbTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtTextToSearch;
        private System.Windows.Forms.RadioButton rbSearchTextInMethod;
        private System.Windows.Forms.RadioButton rbSearchCallers;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.RadioButton rbSearchMethods;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnUpdateSolutionList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtInclude;
        private System.Windows.Forms.Button btnNewTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private KRBTabControl.KRBTabControl krbTabControl1;
        private KRBTabControl.TabPageEx tabPageEx1;
        private System.Windows.Forms.Label lblDone;
    }
}

