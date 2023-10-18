using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BasketballAcademy.Repository
{
    public class ErrorLogger
    {
        /// <summary>
        /// Logs an exception to a text file.
        /// </summary>
        /// <param name="ex">The exception to be logged.</param>
        public static void LogError(Exception ex)
        {
            try
            {
                string logPath = @"C:\Users\Admin\Desktop\ErrorLog.txt";

                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine($"Error occurred at {DateTime.Now}");
                    sw.WriteLine($"Message: {ex.Message}");
                    sw.WriteLine($"Stack Trace: {ex.StackTrace}");
                    sw.WriteLine(new string('-', 50));
                }
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"An error occurred while logging: {logEx.Message}");
            }
        }
    }

}