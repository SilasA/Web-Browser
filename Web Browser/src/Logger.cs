using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Web_Browser
{
    public class Logger
    {
        static StreamWriter log;

        /// <summary>
        /// Initializes the stream for logging and writes opening log.
        /// </summary>
        /// <param name="dir"> Directory of the log file </param>
        public Logger(string dir)
        {
            if (dir.Length != 0)
                log = new StreamWriter(dir);
            else
                log = new StreamWriter("log.txt");
            WriteLog("Program start", "LOG");
        }

        /// <summary>
        /// Writes the final log before the program ends.
        /// </summary>
        public static void WriteFinalLog()
        {
            WriteLog("Program End", "LOG");
        }

        /// <summary>
        /// Writes log to file with only the content provided.
        /// Method will write date, time, and tag without input.
        /// </summary>
        /// <param name="content"> Content of the log </param>
        public static void WriteLog(string content)
        {
            log.WriteLine("[" + DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm:ss") + "]" 
                + "[GEN]: " + content);
        }

        /// <summary>
        /// Writes log to file with the content and tag provided.
        /// Method will write the date and time without input.
        /// </summary>
        /// <param name="content"> Content of the log </param>
        /// <param name="tag"> Log tag format: [LOG] </param>
        public static void WriteLog(string content, string tag)
        {
            log.WriteLine("[" + DateTime.Now.Date.ToString("MM/dd/yyyy HH:mm:ss") + "]"
                + "[" + tag + "]: " + content);
        }
    }
}
