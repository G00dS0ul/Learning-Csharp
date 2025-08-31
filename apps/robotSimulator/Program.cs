namespace robotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var sim = new RobotSim();

            Console.WriteLine("Robot Simulator");
            Console.WriteLine("Step 1: Enter Boundary (e.g. 5,5)");
            Console.WriteLine("Step 2: Enter Initial Robot Position: (e.g. 2,3,East)");
            Console.WriteLine("Type Help for Guide");

            while (true)
            {
                Console.Write("->");
                var input = Console.ReadLine();
                var output = sim.HandleInput(input);
                if(!string.IsNullOrWhiteSpace(output))
                {
                    Console.WriteLine(output);
                }

                if (sim.ExitRequest)
                {
                    break;
                }    
            }
                
        }
    }
}
