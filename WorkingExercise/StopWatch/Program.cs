namespace StopWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my StopWatch: Input START to run and STOP to end Watch");

            var startStopWatch = new StartStopWatch();

            while (true)
            {
                Console.Write("-> ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Enter a correct command: START or STOP.");
                    continue;
                }

                var cmd = input.Trim();

                try
                {
                    if (cmd.Equals("START", StringComparison.OrdinalIgnoreCase))
                    {
                        var now = DateTime.Now;
                        startStopWatch.StartTime(now);
                        Console.WriteLine("The Start Time is: " + now);
                    }
                    else if (cmd.Equals("STOP", StringComparison.OrdinalIgnoreCase))
                    {
                        var now = DateTime.Now;
                        startStopWatch.StopTime(now);
                        Console.WriteLine("The End Time is: " + now);
                        Console.WriteLine("The time duration is: " + startStopWatch.Duration());
                    }

                    else if (cmd.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("StopWatch Program Terminated");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Enter a correct command: START or STOP.");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}