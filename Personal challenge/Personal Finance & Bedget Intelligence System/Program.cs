namespace Personal_Finance___Budget_Intelligence_System;
using System;
using System.Globalization;
using System.Threading;

class Program
{
    static void Main(String[] args)
    {
        var culture = new CultureInfo("en-NG");

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ConsoleUI app = new ConsoleUI();
        app.Run();
    }
}