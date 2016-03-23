using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Roslyn.Services;
using FastColoredTextBoxNS;
using System.IO;
using KRBTabControl;

namespace RoslynCodeSearcher
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        private void Initialization()
        {
            TabController.TabControl = krbTabControl1;

            TabController.InitImages();
            TabController.InitFirstTab();

            Logger.DeleteLog();
            Logger.Log("==================================================================");

            string baseDirectorySolutionsTxtPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "solutions.txt");
            Constants.BaseDirectorySolutionsTxtPath = baseDirectorySolutionsTxtPath;

            //Initially starting path is the directory where the exe resides.
            CodeRepository.StartingPath = AppDomain.CurrentDomain.BaseDirectory;

            btnUpdateSolutionList.Enabled = false;
            this.WindowState = FormWindowState.Maximized;
            rbSearchTextInMethod.Checked = true;
            this.AcceptButton = btnSearch;
            txtTextToSearch.Focus();
        }

        #region Events

        /// <summary>
        /// - Do some checks to see if the input is correct and the solutions.txt file exists
        /// - Update the text of the tab to the text that is being searched
        /// - Show a hourglass icon on the tab during the search
        /// - Start a new worker that will do the search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtTextToSearch.Text;

            if (searchText.Contains("(") || searchText.Contains(")"))
            {
                MessageBox.Show("Please specify searchtext without parentheses or parameters.");
                return;
            }

            if (!File.Exists(Constants.BaseDirectorySolutionsTxtPath))
            {
                MessageBox.Show("There is no solutions.txt file in the directory where the .exe resides. Please click the [Browse] button to select a starting direcctory. Then click [Update solution List]");
            }
            else
            {
                SearchType searchType = new SearchType();

                TabController.UpdateSearchTextOnTab(searchText);
                TabController.ShowHourGlass();

                if (rbSearchTextInMethod.Checked)
                {
                    searchType = SearchType.SearchTextInMethod;
                }
                else if (rbSearchCallers.Checked)
                {
                    searchType = SearchType.SearchCallers;
                }
                else if (rbSearchMethods.Checked)
                {
                    searchType = SearchType.SearchMethods;
                }

                //Create and start a new worker that will do the searching for us.
                WorkerFactory.Start(searchType, searchText, txtFilter.Text, txtInclude.Text, TabController.SelectedTab.Guid);
            }
        }

        /// <summary>
        /// Show a FolderBrowserDialog where the user can select a directory.
        /// This directory will be used as the StartingPath for the coderepository
        /// to search through solutions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                btnUpdateSolutionList.Enabled = true;
                string foldername = this.folderBrowserDialog1.SelectedPath;
                CodeRepository.StartingPath = foldername;

                if (File.Exists(Constants.BaseDirectorySolutionsTxtPath))
                {
                    File.Delete(Constants.BaseDirectorySolutionsTxtPath);
                }
            }
        }

        #endregion Events

        /// <summary>
        /// Update the solutions.txt file with the found solutions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateSolutionList_Click(object sender, EventArgs e)
        {
            List<IWorkspace> workspaces = CodeRepository.RefreshWorkspaces();
            CodeRepository.Workspaces = workspaces;

            CodeRepository.CreateSolutionsTxtFile(workspaces, Constants.BaseDirectorySolutionsTxtPath);

            MessageBox.Show("solutions.txt updated.");
        }

        //Add new tab with FastColoredTextBox
        private void btnNewTab_Click(object sender, EventArgs e)
        {
            TabController.AddTab();
        }

        //Remove FastColoredTextBox and stop the worker
        private void krbTabControl1_TabPageClosing(object sender, KRBTabControl.KRBTabControl.SelectedIndexChangingEventArgs e)
        {
            TabController.RemoveTab(((KRBTabControl.KRBTabControl)sender).SelectedIndex);
        }
	}
}