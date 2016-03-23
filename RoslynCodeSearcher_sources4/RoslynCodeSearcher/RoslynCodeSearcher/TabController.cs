using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using KRBTabControl;
using FastColoredTextBoxNS;
using System.Drawing;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;

namespace RoslynCodeSearcher
{
    public static class TabController
    {
        private static string lang = "CSharp";
        private static List<FastColoredTextBoxNS.FastColoredTextBox> _fastColoredTextBoxes = new List<FastColoredTextBoxNS.FastColoredTextBox>();
        private static KRBTabControl.KRBTabControl _krbTabControl;
        private static ImageList _iconsList = new ImageList();

        private static Object _lockobj = new Object();

        #region Styles

        //styles
        private static TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        private static TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        private static TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        private static TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        private static TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        private static TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        private static TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        private static MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
        private static MarkerStyle YellowStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(180, Color.Yellow)));

        #endregion Styles

        /// <summary>
        /// A list of FastColoredTextBoxes so other parts of the application can access them.
        /// </summary>
        public static List<FastColoredTextBoxNS.FastColoredTextBox> FastColoredTextBoxes
        {
            get
            {
                return _fastColoredTextBoxes;
            }
            set
            {
                _fastColoredTextBoxes = value;
            }
        }

        private static void InitStylesPriority(FastColoredTextBox fctb)
        {
            fctb.ClearStylesBuffer();

            //add this style explicitly for drawing under other styles
            fctb.AddStyle(SameWordsStyle);
            fctb.ShowLineNumbers = false;
        }

        /// <summary>
        /// Set a few properties for the provided FastColoredTextBox and add the FastColoredTextBox to the tabPage.
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="fastColoredTextBox"></param>
        public static void AddFastColoredTextBoxToTabPage(TabPageEx tabPage, FastColoredTextBox fastColoredTextBox)
        {
            fastColoredTextBox.TextChanged += new EventHandler<TextChangedEventArgs>(fastColoredTextBox_TextChanged);
            fastColoredTextBox.Enabled = true;
            fastColoredTextBox.ReadOnly = false;
            fastColoredTextBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            fastColoredTextBox.AutoSize = true;
            fastColoredTextBox.AcceptsTab = true;
            fastColoredTextBox.AcceptsReturn = true;
            fastColoredTextBox.Paddings = new System.Windows.Forms.Padding(10);
            fastColoredTextBox.VerticalScroll.Enabled = true;
            fastColoredTextBox.ShowScrollBars = true;
            fastColoredTextBox.AutoScrollMinSize = new System.Drawing.Size(45, 35);
            InitStylesPriority(fastColoredTextBox);

            tabPage.Controls.Add(fastColoredTextBox);
        }

        /// <summary>
        /// Adds a tab and adds a FastColoredTextBox to that tab.
        /// </summary>
        /// <param name="tabPage"></param>
        public static void AddTab()
        {
            Guid guid = Guid.NewGuid();

            KRBTabControl.TabPageEx newTabPage = new KRBTabControl.TabPageEx();
            newTabPage.Guid = guid;
            _krbTabControl.TabPages.Add(newTabPage);
            _krbTabControl.SelectedTab = newTabPage;

            FastColoredTextBox fastColoredTextBox = new FastColoredTextBox();
            fastColoredTextBox.Guid = guid;

            AddFastColoredTextBoxToTabPage(newTabPage, fastColoredTextBox);
            _fastColoredTextBoxes.Add(fastColoredTextBox);
        }

        /// <summary>
        /// The first tab is already there when we have a KRBTabControl.
        /// </summary>
        /// <param name="krbTabControl1"></param>
        public static void InitFirstTab()
        {
            //Initially a tab is automatically created when we create a KRBTabControl.
            //We have to:
            //- Set the guid of the tab
            //- Add a FastColoredTextBox to the tabpage
            //- Set the guid of the FastColoredTextBox
            //- Add it to the FastColoredTextBoxes collection

            Guid guid = Guid.NewGuid();

            FastColoredTextBox fastColoredTextBox = new FastColoredTextBox();
            fastColoredTextBox.Guid = guid;

            TabPageEx tabPage = (TabPageEx)_krbTabControl.SelectedTab;
            tabPage.Guid = guid;
            tabPage.Focus();

            TabController.AddFastColoredTextBoxToTabPage(tabPage, fastColoredTextBox);
            _fastColoredTextBoxes.Add(fastColoredTextBox);
        }

        /// <summary>
        /// If the text is changed, the text will get colors so that it has the colors of sourcecode like in an IDE.
        /// Also, the text that was searched for will be highlighted in yellow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void fastColoredTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (lang)
            {
                case "CSharp":
                    //For sample, we will highlight the syntax of C# manually, although could use built-in highlighter
                    CSharpSyntaxHighlight(e, (FastColoredTextBox)sender);//custom highlighting
                    break;
                default:
                    break;//for highlighting of other languages, we using built-in FastColoredTextBox highlighter
            }
        }

        /// <summary>
        /// Syntax highlighting logic using regular expressions
        /// </summary>
        /// <param name="e"></param>
        private static void CSharpSyntaxHighlight(TextChangedEventArgs e, FastColoredTextBox fctb)
        {
            fctb.LeftBracket = '(';
            fctb.RightBracket = ')';
            fctb.LeftBracket2 = '\x0';
            fctb.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);

            //string highlighting
            e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            e.ChangedRange.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(class|struct|enum|interface)\s+(?<range>\w+?)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(BlueStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");
            //The text that is searched will be highlighted with a yellow color
            e.ChangedRange.SetStyle(YellowStyle, SelectedTab.Text, RegexOptions.IgnoreCase);

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        public static TabPageEx SelectedTab
        {
            get
            {
                return (TabPageEx)_krbTabControl.SelectedTab;
            }
        }

        /// <summary>
        /// Remove the currently selected tab
        /// and remove the worker from the workerlist
        /// </summary>
        /// <param name="index"></param>
        internal static void RemoveTab(int index)
        {
            _fastColoredTextBoxes.RemoveAt(index);

            Guid tabGuid = SelectedTab.Guid;

            WorkerFactory.Delete(tabGuid);
        }

        /// <summary>
        /// The results of the search will be written to the tab specified with the guid
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="text"></param>
        public static void WriteResults(Guid guid, string text)
        {
            //If another thread comes here, block it temporarily until this thread is finished.
            lock (_lockobj)
            {
                var selectFastColoredTextBox = from fctb in _fastColoredTextBoxes
                                               where fctb.Guid == guid
                                               select fctb;

                if (selectFastColoredTextBox != null && selectFastColoredTextBox.Count()==1)
                {
                    FastColoredTextBox currentTextBox = (FastColoredTextBox)selectFastColoredTextBox.First();

                    currentTextBox.Text = text;

                    if (text == "") currentTextBox.Text = "Nothing found.";

                    //move caret to start text
                    currentTextBox.Selection.Start = Place.Empty;
                    currentTextBox.DoCaretVisible();
                }
            }
        }

        /// <summary>
        /// Searchtext on a tab is only set once:
        /// at the moment a tab is selected (the "current tab")
        /// and [Search] is pressed. Therefore, we don't have to use
        /// the Guid of the tab.
        /// </summary>
        /// <param name="text"></param>
        internal static void UpdateSearchTextOnTab(string text)
        {   
            SelectedTab.Text = text;
        }

        /// <summary>
        /// Show the Hourglass icon on the selected tab
        /// </summary>
        internal static void ShowHourGlass()
        {
            SelectedTab.ImageIndex = 0;
        }

        /// <summary>
        /// Search the tab by Guid and hide the icon
        /// of the tab by setting the ImageIndex to -1.
        /// </summary>
        /// <param name="guid"></param>
        internal static void HideHourGlass(Guid guid)
        {
            TabPageEx tab = GetTabByGuid(guid);
            tab.ImageIndex = -1;
        }

        /// <summary>
        /// Search the tab by GUid in the TabPages collection of the KRBTabControl.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        internal static TabPageEx GetTabByGuid(Guid guid)
        {
            var selectTab = from TabPageEx t in _krbTabControl.TabPages
                            where t.Guid == guid
                            select t;

            return (TabPageEx)selectTab.First();
        }

        public static KRBTabControl.KRBTabControl TabControl
        {
            get
            {
                return _krbTabControl;
            }
            set 
            {
                _krbTabControl = value;
            }
        }

        /// <summary>
        /// Initialize the ImageList for the Tabcontrol.
        /// This will contain a Hourglass icon that we will use to indicate a tab is "busy".
        /// </summary>
        internal static void InitImages()
        {
            Icon icon1 = RoslynCodeSearcher.Properties.Resources.Hourglass;
            _iconsList.ColorDepth = ColorDepth.Depth32Bit;
            _iconsList.ImageSize = new Size(16, 16);
            _iconsList.Images.Add(icon1);
            _krbTabControl.ImageList = _iconsList;
        }
    }
}