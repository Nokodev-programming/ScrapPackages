using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrapPackages
{
    public static class Program
    {
        public static Logger MainLogger = new Logger("Main");
        public static Logger FileSystemLogger = new Logger("File System");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            MainLogger.Info("Enabled Visual Styles.");
            Application.SetCompatibleTextRenderingDefault(false);
            MainLogger.Info("Disabled Compatible Text Rendering.");
            MainLogger.Info("Showing .NET Framework Form.");
            Application.Run(new Main());
        }
    }
}
