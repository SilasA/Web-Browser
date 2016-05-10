using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODOs
// -Add bookmark info (i.e. total bookmarks)
//

namespace Web_Browser
{
    public partial class BookmarkManager : Form
    {
        List<string> tempNames;
        List<string> tempUrls;

        Bookmark bookmark;

        int tempListIdx = -1;

        public BookmarkManager()
        {
            InitializeComponent();
            tempNames = new List<string>();
            tempUrls = new List<string>();
        }

        /// <summary>
        /// Populates the listboxes with the bookmark information.
        /// </summary>
        private void BookmarkManager_Load(object sender, EventArgs e)
        {
            lstNames.Items.Clear();
            lstUrls.Items.Clear();

            foreach (string name in BookmarkHandler.BookmarkNames)
                lstNames.Items.Add(name);
            foreach (string url in BookmarkHandler.BookmarkUrls)
                lstUrls.Items.Add(url);
        }

        /// <summary>
        /// Deletes temporarily saved bookmarks.
        /// </summary>
        private void BookmarkManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (string name in tempNames)
                BookmarkHandler.DeleteBookmark(name);
        }

        /// <summary>
        /// Clears the temporaryily saved bookmarks and closes with no changes.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            tempNames.Clear();
            this.Close();
        }

        /// <summary>
        /// Moves the deleted bookmarks into temporary storage and deletes them from the list.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (object name in lstNames.SelectedItems)
            {
                tempListIdx++;
                tempNames.Add(name as string);
                tempUrls.Add(lstUrls.Items[lstNames.Items.IndexOf(name)] as string);
                lstUrls.Items.RemoveAt(lstNames.Items.IndexOf(name));
                lstNames.Items.Remove(name);
            }
        }

        /// <summary>
        /// Creates a new bookamrk by calling the bookmark form.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bookmark = new Bookmark();
        }

        /// <summary>
        /// Undoes all of the deletions.
        /// </summary>
        private void btnUndoAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= tempListIdx; i++)
            {
                lstNames.Items.Add(tempNames[i]);
                lstUrls.Items.Add(tempUrls[i]);
            }
            tempNames.Clear();
            tempUrls.Clear();
            tempListIdx = -1;
        }
    }
}
