using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace PigDice
{
    public class Game
    {
        private List<Player> Players { get; }
        private readonly Random _random = new Random();
        private static readonly string[][] _diceDrawings;
        private bool GameComplete { get; set; }
        private Player WinningPlayer { get; set; }

        private Game(List<Player> players)
        {
            Players = players;
        }

        static Game()
        {
            _diceDrawings = new string[6][];

            _diceDrawings[0] = new[]
            {
                "┏───────┑",
                "┃       ┃",
                "┃   O   ┃",
                "┃       ┃",
                "┖───────┙"
            };

            _diceDrawings[1] = new[]
            {
                "┏───────┑",
                "┃ O     ┃",
                "┃       ┃",
                "┃     O ┃",
                "┖───────┙"
            };

            _diceDrawings[2] = new[]
            {
                "┏───────┑",
                "┃ O     ┃",
                "┃   O   ┃",
                "┃     O ┃",
                "┖───────┙"
            };

            _diceDrawings[3] = new[]
            {
                "┏───────┑",
                "┃ O   O ┃",
                "┃       ┃",
                "┃ O   O ┃",
                "┖───────┙"
            };

            _diceDrawings[4] = new[]
            {
                "┏───────┑",
                "┃ O   O ┃",
                "┃   O   ┃",
                "┃ O   O ┃",
                "┖───────┙"
            };

            _diceDrawings[5] = new[]
            {
                "┏───────┑",
                "┃ O   O ┃",
                "┃ O   O ┃",
                "┃ O   O ┃",
                "┖───────┙"
            };
        }

        private void PrintTurnStart(int currentPlayer)
        {
            for (var index = 0; index < Players.Count; index++)
            {
                var player = Players[index];
                Console.ForegroundColor = White;
                Console.Write(currentPlayer == index ? "-->" : "   ");
                Console.ForegroundColor = player.Color;
                Console.WriteLine("{0,-15}{1}", player.Name, player.TotalScore.ToString());
            }

            Console.ForegroundColor = White;
        }

        private static void PrintDice(int value1, int value2)
        {
            Console.ForegroundColor = White;
            for (var row = 0; row < 5; row++)
            {
                Console.WriteLine($"{_diceDrawings[value1 - 1][row].ToString()}    {_diceDrawings[value2 - 1][row].ToString()}");
            }
        }

        private static void PrintPigMessage()
        {
            Console.ForegroundColor = Magenta;
            Console.WriteLine(@"        .-~~~~-. |\\_");
            Console.WriteLine(@"     @_/        /  oo\_");
            Console.WriteLine("       |    \\   \\   _(\")");
            Console.WriteLine(@"        \   /-| ||'--'");
            Console.WriteLine(@"         \_\  \_\\");
            Console.WriteLine("PIG!  You lose all points from this turn.");
            Console.ReadKey(false);
        }

        private static void PrintRoundInformation(Player player)
        {
            Console.ForegroundColor = player.Color;
            Console.WriteLine($"{player.Name} has {player.RoundScore.ToString()} this round.");
            Console.ForegroundColor = White;
            Console.WriteLine("Press <Space> to keep going or <Enter> to hold.");
        }

        private static bool HasWon(Player player)
        {
            return player.TotalScore >= 100;
        }

        public void PlayGame()
        {
            var playerIndex = 0;
            GameComplete = false;
            WinningPlayer = null;

            while (!GameComplete)
            {
                Console.Clear();
                PrintTurnStart(playerIndex);
                var currentPlayer = Players[playerIndex];
                Console.WriteLine("Press any key to start your turn.");
                Console.ReadKey(false);
                playerIndex = SwitchPlayer(playerIndex, currentPlayer);
            }
            Console.WriteLine($"{WinningPlayer.Name} has won!");
            Console.ReadKey();
        }

        private int SwitchPlayer(int playerIndex, Player currentPlayer)
        {
            while (true)
            {
                Console.Clear();
                PrintTurnStart(playerIndex);
                var die1 = _random.Next(6) + 1;
                var die2 = _random.Next(6) + 1;
                PrintDice(die1, die2);

                if (die1 == 1 || die2 == 1)
                {
                    currentPlayer.RoundScore = 0;
                    playerIndex = (playerIndex + 1) % Players.Count;
                    PrintPigMessage();
                    break;
                }

                currentPlayer.RoundScore += die1 + die2;
                PrintRoundInformation(currentPlayer);
                var keyInfo = Console.ReadKey(false);

                if (keyInfo.Key != ConsoleKey.Enter)
                {
                    continue;
                }

                currentPlayer.TotalScore += currentPlayer.RoundScore;
                Players[playerIndex].RoundScore = 0;

                if (HasWon(currentPlayer))
                {
                    WinningPlayer = currentPlayer;
                    GameComplete = true;
                }

                playerIndex = (playerIndex + 1) % Players.Count;
                break;
            }

            return playerIndex;
        }

        public static Game CreateGame(int numberOfPlayers)
        {
            var allPlayers = new[] {Red, Blue, Yellow, Green}
               .Take(Math.Max(2, Math.Min(4, numberOfPlayers)))
               .Select((color, index) => new Player($"Player {(index + 1).ToString()}", color))
               .ToList();

            return new Game(allPlayers);
        }
    }
}