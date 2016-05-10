using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Web_Browser
{
    class BookmarkHandler
    {
        static StreamWriter sw;
        static StreamReader sr;

        static int currentIdxInFile = -1;
        static bool foundName = false;

        // Bookmark Information
        static List<string> bookmarkNames;
        public static List<string> BookmarkNames
        {
            get { return bookmarkNames; }
            // Read only if non-menber
        }

        static List<string> bookmarkUrls;
        public static List<string> BookmarkUrls
        {
            get { return bookmarkUrls; }
            // Read only if non-menber
        }

        // Temp Bookmark Information
        static string curBMName;
        static string curBMUrl;

        #region Constructors / Destructor
        /// <summary>
        /// 
        /// </summary>
        static BookmarkHandler()
        {
            bookmarkNames = new List<string>();
            bookmarkUrls = new List<string>();
            GetData();
        }

        /// <summary>
        /// 
        /// </summary>
        ~BookmarkHandler()
        {
            SetData();
        }
        #endregion

        /// <summary>
        /// Create a new bookmark
        /// </summary>
        /// <param name="url"> Url of the new bookmark </param>
        /// <param name="name"> Name of the new bookmark </param>
        public static void NewBookmark(string url, string name)
        {
            bookmarkNames.Add(name);
            bookmarkUrls.Add(url);
            currentIdxInFile++;
            SetData();
        }

        /// <summary>
        /// Finds the bookmark whose name matches the specified name and deletes it.
        /// </summary>
        /// <param name="name"> Name of the bookmark to be deleted </param>
        public static void DeleteBookmark(string name)
        {
            int itr = 0;
            foreach (string bmName in bookmarkNames)
            {
                if (bmName == name)
                {
                    bookmarkNames.RemoveAt(itr);
                    bookmarkUrls.RemoveAt(itr);
                    foundName = true;
                    break;
                }
                itr++;
            }
            if (foundName) currentIdxInFile--;
            else Logger.WriteLog("Unable to find bookmark to delete.", "BMH");
        }

        /// <summary>
        /// Fetches the bookmark data from file.
        /// </summary>
        /// <returns> If it was successful </returns>
        public static bool GetData()
        {
            try
            {
                sr = new StreamReader("bookmarks.txt");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    "Preference file not detected!\n Make sure that all necessary files are in the correct directory",
                    "Warning");
                return false;
            }

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line[0] == currentIdxInFile.ToString()[0])
                {
                    // Get the 2 pieces of data following the indicator
                    line = sr.ReadLine();
                    curBMName = line.Split('=')[1];
                    line = sr.ReadLine();
                    curBMUrl = line.Split('=')[1];
                    SetDataForUse();
                }
            }
            sr.Close();

            return true;
        }

        /// <summary>
        /// Saves the bookmark data to the file.
        /// </summary>
        /// <returns> If it was sucessful </returns>
        public static bool SetData()
        {
            try
            {
                sw = new StreamWriter("bookmarks.txt");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    "Bookmark file not detected!\n Make sure that all necessary files are in the correct directory",
                    "Warning");
                return false;
            }

            // Save data
            foreach (string name in bookmarkNames)
            {
                sw.WriteLine(currentIdxInFile.ToString() + "------------------------------");
                sw.WriteLine("name=" + name);
                sw.WriteLine("url=" + bookmarkUrls[currentIdxInFile]);
            }

            // Close file
            sw.Close();

            return true;
        }

        /// <summary>
        /// Adds the current temporary bookmark into the list.
        /// </summary>
        private static void SetDataForUse()
        {
            bookmarkNames.Add(curBMName);
            bookmarkUrls.Add(curBMUrl);
        }
    }
}
