namespace robotSimulator
{
    public class Environment
    {
        public int Width { get; private set; }
        public int Height { get; private set;  }

        //Allow user to input two number seperated by comma and set the environment
        public bool setBoundary(string input)
        {
            string[] parts = input.Split(",", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                return false;
            }

            bool isWidthValid = int.TryParse(parts[0].Trim(), out int width);
            bool isHeightValid = int.TryParse(parts[1].Trim(), out int height);

            if (isWidthValid && isHeightValid && width > 0 && height > 0)
            {
                Width = width;
                Height = height;
                return true;

            }

            return false;
        }


        //Checks if the robot is in the boundary set by the user
        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height; 
        }
    }
}
