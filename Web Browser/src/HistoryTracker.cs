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
        /// Sets the index back one url if there is one.
        /// </summary>
        /// <returns> If successful or not </returns>
        public static bool StepBack()
        {
            if (!CheckLowerBound()) return false;
            currentIdx--;
            return true;
        }

        /// <summary>
        /// Sets the index forward one url if there is one.
        /// </summary>
        /// <returns></returns>
        public static bool StepForward()
        {
            if (!CheckUpperBound()) return false;
            currentIdx++;
            return true;
        }

        /// <returns> If there is another url preceeding the current one </returns>
        public static bool CheckLowerBound()
        {
            return (currentIdx - 1 != -1);
        }

        /// <returns> If there is another url proceeding the current one </returns>
        public static bool CheckUpperBound()
        {
            return (currentIdx != urlHistory.Count - 1);
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