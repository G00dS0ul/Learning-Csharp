namespace robotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //create a 5x5 grid environment
            var env = new Environment(6, 6);

            //start the robot at (3, 4) facing north
            var robot = new Robot(3, 4, Direction.North);

            Console.WriteLine($"Initial Direction: {robot.Facing} & Initial Position: {robot.X}, {robot.Y}");

            //turn robot to the right
            robot.TurnRight();
            Console.WriteLine($"After turning right: {robot.Facing}");

            //turn Right Again
            robot.TurnRight();
            Console.WriteLine($"After Another Right Turn: {robot.Facing}");

            //turn Left
            robot.TurnLeft();
            Console.WriteLine($"After Left Turn: {robot.Facing}");
            //turn Right Again
            robot.TurnLeft();
            Console.WriteLine($"After Another Left Turn: {robot.Facing}");

            robot.MoveForward(env);
            robot.MoveForward(env);

            Console.WriteLine($"The position on grid: {robot.X}, {robot.Y}");


            //The use of the robotSim class for faster and convinient coding
            var sim = new RobotSim(5, 5, 0, 0, Direction.North);

            sim.MoveRobotFoward();

            sim.TurnRobotRight();
            sim.MoveRobotFoward();

            sim.TurnRobotLeft();
            sim.MoveRobotFoward();
            //Fnally position of the Robot
            Console.WriteLine($"Robot Position: ({sim.Robot.X}, {sim.Robot.Y}), Facing: {sim.Robot.Facing}");

        }
    }
}
