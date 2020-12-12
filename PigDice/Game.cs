using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace PigDice
{
    public class Game
    {
        private List<Player> Players { get; set; }
        private Random random = new Random();
        private static readonly string[][] diceDrawings;

        public Game(List <Player> players)
        {
            Players = players;
        }

        static Game()
        {
            diceDrawings = new string[6][];

            diceDrawings[0] = new[] {
                    "┏───────┑",
                    "┃       ┃",
                    "┃   O   ┃",
                    "┃       ┃",
                    "┖───────┙"};

            diceDrawings[1] = new[] {
                    "┏───────┑",
                    "┃ O     ┃",
                    "┃       ┃",
                    "┃     O ┃",
                    "┖───────┙"};

            diceDrawings[2] = new[] {
                    "┏───────┑",
                    "┃ O     ┃",
                    "┃   O   ┃",
                    "┃     O ┃",
                    "┖───────┙"};

            diceDrawings[3] = new[] {
                    "┏───────┑",
                    "┃ O   O ┃",
                    "┃       ┃",
                    "┃ O   O ┃",
                    "┖───────┙"};

            diceDrawings[4] = new[] {
                    "┏───────┑",
                    "┃ O   O ┃",
                    "┃   O   ┃",
                    "┃ O   O ┃",
                    "┖───────┙"};

            diceDrawings[5] = new[] {
                    "┏───────┑",
                    "┃ O   O ┃",
                    "┃ O   O ┃",
                    "┃ O   O ┃",
                    "┖───────┙"};
        }

        public void PrintTurnStart(int currentPlayer)
        {
            for(var index = 0; index < Players.Count; index++)
            {
                Player player = Players[index];
                Console.ForegroundColor = White;
                Console.Write(currentPlayer == index ? "-->" : "   ");
                Console.ForegroundColor = player.Color;
                Console.WriteLine("{0,-15}{1}", player.Name, player.TotalScore);
            }
            Console.ForegroundColor = White;
        }

        private void PrintDice(int value1, int value2)
        {
            Console.ForegroundColor = White;
            for (var row = 0; row < 5; row++)
            {
                Console.WriteLine(diceDrawings[value1 - 1][row] + "    " + diceDrawings[value2 - 1][row]);
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
            Console.WriteLine(player.Name + " has " + player.RoundScore + " this round.");
            Console.ForegroundColor = White;
            Console.WriteLine("Press <Space> to keep going or <Enter> to hold.");
        }

        private bool HasWon(Player player)
        {
            return player.TotalScore >= 100;
        }

        public void PlayGame()
        {
            var playerIndex = 0;
            var gameComplete = false;
            Player winningPlayer = null;

            while(!gameComplete)
            {
                Console.Clear();
                PrintTurnStart(playerIndex);
                Player currentPlayer = Players[playerIndex];
                Console.WriteLine("Press any key to start your turn.");
                Console.ReadKey(false);

                while(true)
                {
                    Console.Clear();
                    PrintTurnStart(playerIndex);
                    var die1 = random.Next(6) + 1;
                    var die2 = random.Next(6) + 1;
                    PrintDice(die1, die2);
                    
                    if(die1 == 1 || die2 == 1)
                    {
                        currentPlayer.RoundScore = 0;
                        playerIndex = (playerIndex + 1) % Players.Count;
                        PrintPigMessage();
                        break;
                    }

                    currentPlayer.RoundScore += die1 + die2;
                    PrintRoundInformation(currentPlayer);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(false);

                    if(keyInfo.Key == ConsoleKey.Enter)
                    {
                        currentPlayer.TotalScore += currentPlayer.RoundScore;
                        Players[playerIndex].RoundScore = 0;

                        if(HasWon(currentPlayer))
                        {
                            winningPlayer = currentPlayer;
                            gameComplete = true;
                        }

                        playerIndex = (playerIndex + 1) % Players.Count;
                        break;
                    }
                }
            }
            Console.WriteLine(winningPlayer.Name + " has won!");
            Console.ReadKey();
        }

        public static Game CreateGame(int numberOfPlayers)
        {
            var allPlayers = new[] {Red, Blue, Yellow, Green}
               .Take(Math.Max(2, Math.Min(4, numberOfPlayers)))
               .Select((color, index) => new Player($"Player {index + 1}", color))
               .ToList();

            return new Game(allPlayers);
        }
    }
}
