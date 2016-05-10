using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODOs:
//  -Forward & back buttons
//  -More preferences
//


namespace Web_Browser
{
    public partial class Browser : Form
    {
        FileHandler _persistance;
        BookmarkHandler _bookmarkhandler;
        HistoryTracker _historytracker;
        Logger _logger;

        string _currentUrl;

        bool _onPrevUrl = false;

        #region Additional Forms
        Preferences _PreferencesForm;
        Bookmark _BookmarkForm;
        BookmarkManager _BookmarkManager;
        #endregion

        /// <summary>
        /// Initializes the program and all utilities and handlers.
        /// </summary>
        public Browser()
        {
            InitializeComponent();
            _persistance = new FileHandler();
            _PreferencesForm = new Preferences();
            _logger = new Logger("");
        }

        /// <summary>
        /// When the form loads navigate the browser to the homepage
        /// </summary>
        private void Browser_Load(object sender, EventArgs e)
        {
            webBrowser.Navigate(FileHandler.HomepageURL);
            _currentUrl = FileHandler.HomepageURL;
        }

        /// <summary>
        /// Actions for when the browser is closed.
        /// </summary>
        private void Browser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.WriteFinalLog();
        }
        
        /// <summary>
        /// When any key is presses in the browser window.
        /// </summary>
        private void Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsInputKey(Keys.Back))
                btnBackAction();
        }

        #region ToolStrip Menu
        /// <summary>
        /// Display information about app.
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This was made by Silas Agnew.", "About");
        }

        /// <summary>
        /// Parent Container: Options; Creates form for the user to access thier preferences.
        /// </summary>
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _PreferencesForm.Show();
        }

        /// <summary>
        /// Handles closing of the program.
        /// </summary>
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Are you sure you want to exit this application?", "Exit",
                MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
                this.Close();
            else return;
        }

        /// <summary>
        /// Navigates the browser to the user's homepage.
        /// </summary>
        private void homePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(FileHandler.HomepageURL);
        }

        /// <summary>
        /// Opens the new bookmark form.
        /// </summary>
        private void addBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _BookmarkForm = new Bookmark(_currentUrl);
            _BookmarkForm.Show();
        }

        /// <summary>
        /// Opens the bookmark manager form.
        /// </summary>
        private void bookmarkManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _BookmarkManager = new BookmarkManager();
            _BookmarkManager.Show();
        }
        #endregion

        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        private void btnNavigate_Click(object sender, EventArgs e)
        {
            slLoaded.Text = "Loading...";
            btnNavigate.Enabled = false;
            txtURL.Enabled = false;
            Navigate(txtURL.Text);
        }

        /// <summary>
        /// Wrapper for browser navigation.
        /// </summary>
        /// <param name="url"></param>
        private void Navigate(string url)
        {
            webBrowser.Navigate(url);
        }
        
        /// <summary>
        /// Wrapper for back button presses.
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            btnBackAction();
        }

        /// <summary>
        /// 
        /// </summary>
        private void btnForward_Click(object sender, EventArgs e)
        {
            HistoryTracker.StepForward();
            Navigate(HistoryTracker.UrlHistory[HistoryTracker.CurrentIdx]);

            btnBack.Enabled = HistoryTracker.CheckLowerBound();
            btnForward.Enabled = HistoryTracker.CheckUpperBound();
        }

        /// <summary>
        /// Takes the browser back one url.
        /// </summary>
        private void btnBackAction()
        {
            HistoryTracker.StepBack();
            Navigate(HistoryTracker.UrlHistory[HistoryTracker.CurrentIdx]);

            btnBack.Enabled = HistoryTracker.CheckLowerBound();
            btnForward.Enabled = HistoryTracker.CheckUpperBound();
        }

        #region Web Browser Events
        /// <summary>
        /// Enables writing to URL box and navigate when document is finished loading
        /// </summary>
        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            btnNavigate.Enabled = true;
            txtURL.Enabled = true;
        }

        /// <summary>
        /// Maintains the status bar
        /// </summary>
        private void webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0)
            {
                int percentage = (int)(e.CurrentProgress * 100 / e.MaximumProgress);
                if (percentage <= 100) pbLoaded.ProgressBar.Value = percentage;
            }
            slLoaded.Text = "Done.";
        }

        /// <summary>
        /// Changes the URL box to the current URL
        /// </summary>
        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            txtURL.Text = e.Url.ToString();
        }

        /// <summary>
        /// When the document is loaded put the current url in the url textbox
        /// </summary>
        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            _currentUrl = webBrowser.Url.ToString();
            txtURL.Text = _currentUrl;
        }

        #endregion
    }
}
