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
        public Bookmark(ref string url)
        {
            InitializeComponent();
            tmpUrl = url;
        }

        string tmpUrl;

        /// <summary>
        /// 
        /// </summary>
        private void Bookmark_Load(object sender, EventArgs e)
        {
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
            if (tmpUrl == "") {} // Some error
            if (txtName.Text == "") // If no name was entered
            {
                MessageBox.Show("Please give the new bokmark a name!", "Bookmark");
                return;
            }
            BookmarkHandler.NewBookmark(ref tmpUrl, txtName.Text);
        }
    }
}
