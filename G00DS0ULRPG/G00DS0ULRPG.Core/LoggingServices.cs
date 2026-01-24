namespace G00DS0ULRPG.Core
{
    public static class LoggingServices
    {
        private const string LOG_FILE_DIRECTORY = "logs";

        static LoggingServices()
        {
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOG_FILE_DIRECTORY);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public static void Log(Exception exception, bool isInnerException = false)
        {
            using var sw = new StreamWriter(LogFileName(), true);
            sw.WriteLine(isInnerException ? "INNER EXCEPTION" : $"EXCEPTION: {DateTime.Now}");
            sw.WriteLine(new string(isInnerException ? '-' : '=', 40));
            sw.WriteLine($"{exception.Message}");
            sw.WriteLine($"{exception.StackTrace}");
            sw.WriteLine();

            if (exception.InnerException != null)
            {
                Log(exception.InnerException, true);
            }
        }

        private static string LogFileName()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOG_FILE_DIRECTORY,
                $"G00dS0ul_{DateTime.Now:yyyy MMMM dd}.log");
        }

        
        


    }
}
