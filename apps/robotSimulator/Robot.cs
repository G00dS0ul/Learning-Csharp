using System.Security.Cryptography.X509Certificates;

namespace robotSimulator
{
    public enum Direction { North, East, South, West }
    public class Robot
    {
        public int XAxis { get; private set; }
        public int YAxis { get; private set; }
        public Direction Facing { get; private set; }

     
        public Robot(int x, int y, Direction facing)
        {
            this.XAxis = x;
            this.YAxis = y;
            this.Facing = facing;
        }

        public static bool IntialPlacement(string input, out int x, out int y, out Direction facing, Direction defaultFacing = Direction.North)
        {
            x = y = 0;
            facing = defaultFacing;
            if (string.IsNullOrWhiteSpace(input))
                return false;
            var parts = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length > 3 || parts.Length < 2 )
            {
                return false;
            }
            if (!int.TryParse(parts[0], out x) || !int.TryParse(parts[1], out y))
            {
                return false;
            }
            if(parts.Length == 3)
            {
                if (!Enum.TryParse(parts[2], true, out facing))
                {
                    return false;
                }
            }
            return true;
        }

        public void MoveForward(Environment env)
        {
            int newX = XAxis;
            int newY = YAxis;
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
                XAxis = newX;
                YAxis = newY;
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
