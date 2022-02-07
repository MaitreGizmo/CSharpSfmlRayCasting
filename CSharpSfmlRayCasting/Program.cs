using System;
using System.IO;
using System.Diagnostics;

using CSharpSfmlRayCasting.Core;
using CSharpSfmlRayCasting.Logs;
using CSharpSfmlRayCasting.Enums;

namespace CSharpSfmlRayCasting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game game = new Game();

                game.Run();
            }
            catch(Exception ex)
            {
                Logger.WriteLog(LogLevel.ERROR, "A fatal error occured", $"{nameof(Program)} - {nameof(Main)}", ex);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n--------------------------------------------------------------");
                Console.WriteLine("> A FATAL ERROR OCCURED. CHECK LOGS FILES FOR MORE INFOS");
                Console.WriteLine("--------------------------------------------------------------");
            }

            if (Logger.LogsWritten)
            {
                DirectoryInfo dir = new DirectoryInfo("logs");

                Process.Start("explorer.exe", dir.FullName);
            }
        }
    }
}
