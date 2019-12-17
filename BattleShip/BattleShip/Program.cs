using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    /*
 * 
 * Your challenge is to implement this simplified game of Battleships using text input and output.

The computer randomly chooses the location of two single-cell "ships" on a board of 8 by 8 cells.The user then has 20 guesses to find the two ships.

The user enters a co-ordinate, for example 3,5, and the computer locates the nearest ship to that co-ordinate and tells them they're "hot" if they're 1 to 2 cells away, "warm" if they're 3 to 4 cells away, or "cold" if they're further away.

As an example, 3,5 is three cells away from 2,7 because (3 - 2) + (7 - 5) = 3, so they'd be told they were "warm".

If the user correctly guesses a ship's location, they're told they've got a hit and that ship is removed from the board. The game ends when both ships have been hit by the user, or the user has used up their 20 guesses.

Write your code in a style that you consider to be production quality.

Remember: please do your working on this page and ensure all code is your own, no copying and pasting. It doesn't matter if your code doesn't run, we're more interested in your logical thinking, process and coding style. Select the language you'd like to use below to get

=============================


*/
    public class BattleGround
    {
        // Properties
        const int battleFieldSize = 8;
        const int ships = 2;
        int guesses = 20;

        Dictionary<string, int> shipLocation = new Dictionary<string, int>();

        int[,] battleGround = new int[battleFieldSize, battleFieldSize];

        // 2D array of 8X8 => Battle Field
        static void createEmptyBattleField()
        {
            for (int i = 0; i < battleFieldSize; i++)
            {
                for (int j = 0; j < battleFieldSize; j++)
                {
                    battleGround[i, j] = 0;
                }
            }
            //getBattleStatus(); // For debugging
        }
        // Place Ships at random indexes
        static void placeShips()
        {
            for (int ship = 0; ship < ships; ship++)
            {
                Random rnd = new Random();
                int i = rnd.Next(8);
                int j = rnd.Next(8);
                battleGround[i, j] = 1;
                shipLocation.Add("row", i);
                shipLocation.Add("column", j);
            }
            //getBattleStatus(); // For debugging
        }

        // Check game status
        static bool getBattleStatus()
        {
            bool isBattleFinished = true;
            for (int i = 0; i < battleFieldSize; i++)
            {
                for (int j = 0; j < battleFieldSize; j++)
                {
                    if (battleGround[i, j] == 1)
                    {
                        isBattleFinished = false;
                    }
                    //Console.Write(battleGround[i,j]); // For debugging
                }
                //Console.WriteLine(""); // For debugging
            }

            return isBattleFinished;
        }

        // Show result when user enter the ship location
        static void getUserGuessAndShowResult()
        {
            if (guesses > 0)
            {
                guesses--;

                Console.WriteLine("Enter column position");
                int column = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter row position");
                int row = Convert.ToInt32(Console.ReadLine());

                if (battleGround[row, column] == 1)
                {
                    battleGround[row, column] = 0;
                }
                else
                {
                    var guessedCoOrdinate = row + column;
                    foreach (var location in shipLocation)
                    {
                        var shipCoOrdinate = location["row"] + location["column"];

                        if (Math.Abs(shipCoOrdinate - guessedCoOrdinate) <= 2)
                        {
                            Console.WriteLine("Hot");
                            getUserGuessAndShowResult();
                            return;
                        }
                        else if (Math.Abs(shipCoOrdinate - guessedCoOrdinate) <= 4)
                        {
                            Console.WriteLine("Warm");
                            getUserGuessAndShowResult();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Cold");
                            getUserGuessAndShowResult();
                            return;
                        }
                    }
                }

                if (getBattleStatus() == false)
                {
                    getUserGuessAndShowResult();
                }
                else
                {
                    Console.Log("Game Over");
                    ContinueOrExitGame();
                }
            }
            else
            {
                Console.Log("No more chance left: Game Over");
                ContinueOrExitGame();
            }
        }

        // Show Alert on finishng game
        static void ContinueOrExitGame()
        {
            Console.WriteLine("Type 1 to Replay");
            Console.WriteLine("Type 2 to Exit");
            int option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {
                gameStart();
            }
            else if (option == 2)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid inpur, try again!");
            }
        }

        // Game starts here
        static void gameStart()
        {
            reset();
            createEmptyBattleField();
            placeShips();
            getUserGuessAndShowResult();
        }

        // Reset Properties
        static void reset()
        {
            guesses = 20;

            shipLocation = new Dictionary<string, int>();

            battleGround = new int[battleFieldSize, battleFieldSize];
        }

        static void Main(string[], args)
        {
            gameStart();
        }
    }
}


