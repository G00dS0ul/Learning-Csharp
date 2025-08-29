namespace robotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //create an instance grid environment
            var env = new Environment();

            //create an instance of the robot
            var robot = new Robot();
            //allows user to create Boundary for the environment
            Console.Write("Set the Boundary: ");
            var inputBound = Console.ReadLine();
            if (env.setBoundary(inputBound))
            {
                Console.WriteLine($"Boundary set to {env.Width} x {env.Height}");
            }

            else
            {
                Console.WriteLine("Invalid input. Please enter two positive integers seperated by a comma.");
                return;
            }
            //allow user to set the starting point
            Console.WriteLine("Enter starting Position (format: x,y,direction): ");

            while (true)
            {
                Console.Write("Start Movement: ");
                var inputMove = Console.ReadLine();
                
                if (robot.robotMovement(inputMove!, env))
                {
                    Console.WriteLine($"Stating X-Postion is: {robot.XAxis}, Starting Y-Position is: {robot.YAxis} And Facing: {robot.Facing}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Correct Format");
                }

            }
            //Allows user to enter command for easy movement on the console
            Console.WriteLine("Commands: F = forward, L = turn left, R = turn right, P = print Location, Q = quit");

            while (true)
            {
                Console.Write("Move: ");
                var command = Console.ReadLine();
                if (string.IsNullOrEmpty(command))
                {
                    continue;
                }

                switch (command.Trim().ToUpperInvariant())
                {
                    case "F":
                        robot.MoveForward(env);
                        break;
                    case "L":
                        robot.TurnLeft();
                        break;
                    case "R":
                        robot.TurnRight();
                        break;
                    case "P":
                        Console.WriteLine($"X-Postion is: {robot.XAxis}, Starting Y-Position is: {robot.YAxis} And Facing: {robot.Facing}");
                        break;
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Unknown command");
                        continue;

                        
                }

                //Allow the location of robot to be printed after each command
                if (command.ToUpper() == "F" || command.ToUpper() == "L" || command.ToUpper() == "R")
                {
                    Console.WriteLine($"X-Postion is: {robot.XAxis}, Y-Position is: {robot.YAxis} And Facing: {robot.Facing}");
                }
            }
                
        }
    }
}
