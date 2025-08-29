namespace robotSimulator
{
    public class RobotSim
    {
        public Robot Robot { get; }
        public Environment Env { get; }

        public RobotSim(int envWidth, int envHeight, int robotX, int robotY, Direction facing)
        {
            Env = new Environment();
            Robot = new Robot();
        }

        public void MoveRobotFoward()
        {
            Robot.MoveForward(Env);
        }
        public void TurnRobotLeft()
        {
            Robot.TurnLeft();
        }
        public void TurnRobotRight()
        {
            Robot.TurnRight();
        }


    }
}
