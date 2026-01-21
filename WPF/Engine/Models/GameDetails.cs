namespace Engine.Models
{
    public class GameDetails
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public List<PlayerAttribute> PlayerAttributes { get; set; } = [];
        public GameDetails(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}

