using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Web_Browser
{
    class FileHandler
    {
        static StreamWriter sw;
        static StreamReader sr;

        #region Defaults
        static readonly string  d_homepage = "http://www.google.com/";
        #endregion

        #region Flags
        static bool dataAcquired = false;
        public static bool DataAcquired
        {
            get { return dataAcquired; }
            // Read only for non-members
        }

        static bool dataSaved = false;
        public static bool DataSaved
        {
            get { return dataSaved; }
            // Read only for non-members
        }
        #endregion

        #region Preferences Data
        static string homepageURL;
        public static string HomepageURL
        {
            get { return homepageURL; }
            set { homepageURL = value; }
        }
        #endregion

        #region Constructors / Destructor
        /// <summary>
        /// Acquires data upon creation
        /// </summary>
        static FileHandler()
        {
            GetData();
        }

        /// <summary>
        /// Saves data before destruction
        /// </summary>
        ~FileHandler()
        {
            SetData();
        }
        #endregion

        /// <summary>
        /// Acquired data from the preferences file for the application to use.
        /// </summary>
        /// <returns> If acquisition was successful </returns>
        public static bool GetData()
        {
            if (dataAcquired) return true; // Already acquired

            // Attempt to open stream
            try
            {
                sr = new StreamReader("pref.txt");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    "Preference file not detected!\n Make sure that all necessary files are in the correct directory",
                    "Warning");
                return false; // Error
            }

            // Keyword searching
            homepageURL = FindKeyword("homepage");

            // Close and Dispose of stream to let file be wrote to
            sr.Close();
            sr.Dispose();

            dataSaved = false;
            dataAcquired = true;
            return true; // Success
        } 

        /// <summary>
        /// Writes all data to the preferences file
        /// </summary>
        /// <returns> If the write was successful </returns>
        public static bool SetData()
        {
            // Attempt to open stream
            try
            {
                sw = new StreamWriter("pref.txt");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    "Preference file not detected!\n Make sure that all necessary files are in the correct directory",
                    "Warning");
                return false;
            }

            // Write data
            sw.WriteLine("homepage=" + homepageURL);

            // Close stream
            sw.Close();
            sw.Dispose();

            dataSaved = true;
            dataAcquired = false;
            return true;
        }

        /// <summary>
        /// Finds the keyword in the file and the corresponding data paired with it.
        /// File structure: 
        ///     [keyword]=[value]
        /// </summary>
        /// <param name="keyword"> The word to search for </param>
        /// <returns> The corresponding value in string form </returns>
        private static string FindKeyword(string keyword)
        {
            string line;
            string value = "";

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                // Separate [keyword] and [value] with '='
                if (line.Split('=')[0] == keyword)
                    value = line.Split('=')[1];
            }
            return value;
        }

        /// <summary>
        /// Reset all of the preferences to their default values
        /// </summary>
        /// <returns> If successful (false if fail or user aborted action) </returns>
        public static bool ResetToDefaults()
        {
            // Prompt for confirmation
            DialogResult dr = MessageBox.Show("Are you sure you want to reset to defaults?",
                "Reset", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
                return false;

            // Set data to default
            homepageURL = d_homepage;

            // Save
            SetData();

            return true;
        }
    }
}
