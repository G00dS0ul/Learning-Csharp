namespace G00DS0ULRPG.Core
{
    public class GameMessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public GameMessageEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
