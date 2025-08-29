using System.Security.Cryptography.X509Certificates;

namespace robotSimulator
{
    public enum Direction { North, East, South, West }
    public class Robot
    {
        public int XAxis { get; private set; }
        public int YAxis { get; private set; }
        public Direction Facing { get; private set; }

        public bool robotMovement(string input, Environment? env = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            string[] parts = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length > 3 || parts.Length < 2 )
            {
                return false;
            }
            if (!int.TryParse(parts[0], out int xAxis) || !int.TryParse(parts[1], out int yAxis))
            {
                return false;
            }
            Direction facing = Facing;
            if(parts.Length == 3)
            {
                if (!Enum.TryParse(parts[2], true, out facing))
                {
                    return false;
                }
            }

            if (env is not null && !env.IsValidPosition(xAxis, yAxis))
            {
                return false;
            }
            this.XAxis = xAxis;
            this.YAxis = yAxis;
            this.Facing = facing;
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
