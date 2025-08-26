using System.Security.Cryptography.X509Certificates;

namespace robotSimulator
{
    public enum Direction { North, East, South, West }
    public class Robot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction Facing { get; private set; }

        public Robot(int x, int y, Direction facing)
        {
            X = x;
            Y = y;
            Facing = facing;
        }

        public void MoveForward(Environment env)
        {
            int newX = X;
            int newY = Y;
            switch (Facing)
            {
                case Direction.North:
                    newY++;
                    break;
                case Direction.East:
                    newX++;
                    break;
                case Direction.South:
                    newY--;
                    break;
                case Direction.West:
                    ;
                    newX--;
                    break;
            }
            if (env.IsValidPosition(newX, newY))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                Console.WriteLine("Out of Bound!!!");
            }
        }    
        public void TurnLeft()
        {
             Facing = (Direction)(((int)Facing + 3) % 4);
        }
            
        public void TurnRight()
        {
            Facing = (Direction)(((int)Facing + 1) % 4);
        }
        
    }
}
