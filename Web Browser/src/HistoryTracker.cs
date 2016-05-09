using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Browser
{
    class HistoryTracker
    {
        // List containing all uncleared history of the user
        static List<string> urlHistory;
        public static List<string> UrlHistory
        {
            get { return urlHistory; }
            // Read only to non-members
        }
        // Current index of the history list
        static int currentIdx;
        public static int CurrentIdx
        {
            get { return currentIdx; }
            // Read only to non-members
        }

        /// <summary>
        /// Adds a new url to the history and increments the current index to match.
        /// </summary>
        /// <param name="url"> The new url </param>
        public static void AddUrl(string url)
        {
            urlHistory.Add(url);
            currentIdx++;
        }

        /// <summary>
        /// Clears the current history.
        /// </summary>
        public static void ClearHistory()
        {
            urlHistory.Clear();
            currentIdx = 0;
        }
    }
}
;