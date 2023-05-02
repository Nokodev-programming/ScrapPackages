using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapPackages
{
    /// <summary>
    /// Like Console, but has more style.
    /// </summary>
    public class Logger
    {
        private string name;

        /// <summary>
        /// Creates a new logger.
        /// </summary>
        /// <param name="name">The name of the logger</param>
        public Logger(string name)
        {
            this.name = name;
        }
        public void Info(string message, params object[] arg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[ INFO ][ {name} ]: ");
            Console.ResetColor();
            Console.WriteLine(message, arg);
        }
        public void Warn(string message, params object[] arg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[ WARNING ][ {name} ]: ");
            Console.ResetColor();
            Console.WriteLine(message, arg);
        }
        public void Error(string message, params object[] arg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[ ERROR ][ {name} ]: ");
            Console.ResetColor();
            Console.WriteLine(message, arg);
        }
    }
}
