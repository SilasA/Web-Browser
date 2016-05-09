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
    public partial class Preferences : Form
    {
        bool changed = false;

        public Preferences()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads preference data upon creation of form
        /// </summary>
        private void Preferences_Load(object sender, EventArgs e)
        {
            FileHandler.GetData();
            txtHomePage.Text = FileHandler.HomepageURL;
        }

        /// <summary>
        /// Closes the form when cancel is clicked. Invokes form closing event.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Prompts the user for confirmation of the closing if changes were made.
        /// </summary>
        private void Preferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If changes were made to the settings, prompt
            if (changed)
            {
                DialogResult dr = MessageBox.Show("Close without saving?", "Close",
                    MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                    e.Cancel = false;
                else e.Cancel = true;
            }
            // Closes if no changes were made
        }

        /// <summary>
        /// Save preferences.
        /// </summary>
        private void btnApply_Click(object sender, EventArgs e)
        {
            FileHandler.HomepageURL = txtHomePage.Text;
            Save();
            changed = false;
            Display();
        }

        /// <summary>
        /// Save preferences and hide.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            FileHandler.HomepageURL = txtHomePage.Text;
            Save();
            changed = false;
            Display();
            this.Hide();
        }

        /// <summary>
        /// Resets preferences to default values and fetches new data.
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            FileHandler.ResetToDefaults();
            FileHandler.GetData();
            Display();
        }

        /// <summary>
        /// Wrapper function for saving preference data
        /// </summary>
        private void Save()
        {
            FileHandler.SetData();
        }

        /// <summary>
        /// Displays all settings.
        /// </summary>
        private void Display()
        {
            txtHomePage.Text = FileHandler.HomepageURL;
        }

        #region Change Detection
        /// <summary>
        /// Detects if the txtHomePage text has been changed.
        /// </summary>
        private void txtHomePage_TextChanged(object sender, EventArgs e)
        {
            changed = true;
            btnApplyEnable();
        }

        /// <summary>
        /// Enables the Apply button.
        /// </summary>
        private void btnApplyEnable() { btnApply.Enabled = true; }
        #endregion
    }
}
