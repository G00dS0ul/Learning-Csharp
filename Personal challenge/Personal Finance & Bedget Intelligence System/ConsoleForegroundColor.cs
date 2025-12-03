namespace Personal_Finance___Budget_Intelligence_System
{
    public static class ConsoleForegroundColor
    {
        public static void PrintColor(string message,  ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}