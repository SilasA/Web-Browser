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
        /// 
        /// </summary>
        private void addBookmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _BookmarkForm = new Bookmark(ref _currentUrl);
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
            webBrowser.Navigate(txtURL.Text);
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
