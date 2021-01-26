using System;

namespace PigDice
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Write("How many players are there? ");
            var amount = Convert.ToInt32(Console.ReadLine());

            var game = Game.CreateGame(amount);

            game.PlayGame();
        }
    }
}
