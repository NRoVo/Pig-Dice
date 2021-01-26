using System;

namespace PigDice
{
    public class Player
    {
        public string Name { get; }
        public int TotalScore { get; set; }
        public int RoundScore { get; set; }
        public ConsoleColor Color { get; }

        public Player(string name, ConsoleColor color)
        {
            Name = name;
            Color = color;
            TotalScore = 0;
            RoundScore = 0;
        }
    }
}
