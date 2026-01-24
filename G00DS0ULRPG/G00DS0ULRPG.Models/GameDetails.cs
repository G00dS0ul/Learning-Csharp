namespace G00DS0ULRPG.Models
{
    public class GameDetails
    {
        public string Title { get; }
        public string Version { get; }
        public string SubTitle { get; }
        public List<PlayerAttribute> PlayerAttributes { get; } = [];

        public List<Race> Races { get; } = [];
        public GameDetails(string title, string subtitle, string version)
        {
            Title = title;
            SubTitle = subtitle;
            Version = version;
        }
    }
}

