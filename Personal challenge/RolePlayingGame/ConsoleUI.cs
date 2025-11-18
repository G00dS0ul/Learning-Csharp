namespace RolePlayingGame
{
    public static class ConsoleUI
    {
        public static void PrintColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}