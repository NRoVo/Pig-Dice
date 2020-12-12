using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigDice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("How many players are there? ");
            var amount = Convert.ToInt32(Console.ReadLine());

            Game game = Game.CreateGame(amount);

            game.PlayGame();
        }
    }
}
