namespace robotSimulator
{
    public class Environment
    {
        public int Width { get; }
        public int Height { get; }

        public Environment(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}
