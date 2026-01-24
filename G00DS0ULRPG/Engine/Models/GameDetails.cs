namespace Engine.Models
{
    public class GameDetails
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string SubTitle { get; set; }
        public List<PlayerAttribute> PlayerAttributes { get; set; } = [];

        public List<Race> Races { get; } = [];
        public GameDetails(string title, string subtitle, string version)
        {
            Title = title;
            SubTitle = subtitle;
            Version = version;
        }
    }
}

