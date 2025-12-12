using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    class Player
    {
        public string Name { get; set; }
        public string CharacterClass { get; set; }
        public int HitPoint { get; set; }
        public int ExperiencePoint { get; set; }
        public int Level { get; set; }
        public int Gold { get;  set; }
    }
}
