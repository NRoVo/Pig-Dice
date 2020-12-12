using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigDice
{
    public class Player
    {
        public string Name { get; set; }
        public int TotalScore { get; set; }
        public int RoundScore { get; set; }
        public ConsoleColor Color { get; set; }

        public Player(string name, ConsoleColor color)
        {
            Name = name;
            Color = color;
            TotalScore = 0;
            RoundScore = 0;
        }
    }
}
