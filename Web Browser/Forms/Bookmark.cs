using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Web_Browser
{
    public partial class Bookmark : Form
    {
        bool readMode = true;
        string tmpUrl;

        /// <summary>
        /// Constructs the bookmark form with a specified url or none (user provides one).
        /// </summary>
        /// <param name="url"> The url of the page or default user specified url </param>
        public Bookmark(string url = "")
        {
            InitializeComponent();

            if (url != "") tmpUrl = url;
            else readMode = false;
        }

        /// <summary>
        /// Loads the url into the label.
        /// </summary>
        private void Bookmark_Load(object sender, EventArgs e)
        {
            if (readMode) txtUrl.Text = tmpUrl;
            txtUrl.ReadOnly = readMode;
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Create a new bookmark.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || (!readMode && txtUrl.Text == "")) // If no name or url was entered
            {
                MessageBox.Show("Please give the new bokmark a name!", "Bookmark");
                return;
            }
            if (!readMode)
                tmpUrl = txtUrl.Text; 
            BookmarkHandler.NewBookmark(tmpUrl, txtName.Text);
            this.Close();
        }
    }
}
