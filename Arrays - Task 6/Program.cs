using System;
using System.Threading;

namespace Arrays___Task_6
{
    class Program
    {
        static int rows = 10, columns = 10, scorePlayer = 100, scorePC = 0;
        static char[,] battleshipGrid = new char[rows, columns];
        static char[,] tempGrid = new char[rows, columns];
        static char[,] playerGrid = new char[rows, columns];
        static char water = '~';
        static char letter = 'a';
        static char ship = '=';
        static char hitShip = 'X';
        static char missShip = '/';
        static int unicode = 65;
        static bool player2 = false;
        static int timesHit = 0;


        static int carrierNumber = 1;
        static int battleshipNumber = 1;
        static int cruiserNumber = 1;
        static int destroyerNumber = 2;
        static int submarineNumber = 2;


        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 45);
            Menu();
        }

        static void fillBoard(char[,] board)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    board[i, j] = water;
                }
            }
            tempGrid = battleshipGrid;
        }

        static void printBoard()
        {
            var board = playerGrid;
            if (player2 == false)
            {
                board = battleshipGrid;
            }
            else
            {
                board = playerGrid;
            }
            for (int f = 0; f < 11; f++)
            {
                if (f == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[ ]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[{0}]", f);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine("");
            for (int i = 0; i < columns; i++)
            {
                letter = Convert.ToChar(unicode);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[{0}]", letter);
                unicode++;
                Console.ForegroundColor = ConsoleColor.White;
                for (int j = 0; j < rows; j++)
                {
                    if (board[i, j] == water)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("[{0}]", board[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (board[i, j] == ship)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[{0}]", board[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (board[i, j] == hitShip)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[{0}]", board[i, j]);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (board[i, j] == missShip)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("[{0}]", board[i, j]);
                    }
                    else
                    {
                        Console.Write("[{0}]", board[i, j]);
                    }
                }
                Console.WriteLine("");
            }
        }

        static void Menu()
        {

            Console.Title = ("BattleShip");
            do
            {
                fillBoard(battleshipGrid);

                menuArt();
                Console.WriteLine("Would you like to play against another Player, or the PC?");
                string choice = Console.ReadLine();
                choice = choice.ToLower();
                if (choice == "player")
                {
                    Player();
                    break;
                }
                else if (choice == "pc")
                {
                    PC();
                    break;
                }
                else
                {
                    Console.WriteLine("Sorry, thats an Invalid option, Please try again.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            } while (true);
        }
        static void shipLocationChooser()
        {
            do
            {
                Console.Clear();
                printBoard();
                Console.Write("\nSHIP                    #");
                Console.Write("\n1. Aircraft Carrier    {0}x", carrierNumber);
                Console.Write("\n2. BattleShip          {0}x", battleshipNumber);
                Console.Write("\n3. Cruiser             {0}x", cruiserNumber);
                Console.Write("\n4. Destroyer           {0}x", destroyerNumber);
                Console.Write("\n5. Submarine           {0}x", submarineNumber);
                Console.Write("\n\n6. Exit\n");
                Console.WriteLine("\nWhat would you like to choose the location for?");
                string choiceStr = Console.ReadLine();
                bool isNumber = int.TryParse(choiceStr, out int choice);
                if (isNumber == false || choice > 6)
                {
                    Console.WriteLine("INVALID CHOICE");
                    Thread.Sleep(500);
                    unicode = 65;
                }
                else if (choice == 1)
                {
                    if (carrierNumber == 0)
                    {
                        Console.WriteLine("You have already chosen the location for this ship");
                        Thread.Sleep(500);
                        unicode = 65;
                    }
                    else
                    {
                        int rowChoice = row();
                        int columnChoice = column();
                        Console.WriteLine("Would you like the ship to be horizontal or vertical?");
                        string choiceH = Console.ReadLine().ToLower();
                        if (rowChoice >= 2 && columnChoice >= 2 && rowChoice <= 8 && columnChoice <= 8)
                        {
                            if (choiceH == "horizontal")
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (battleshipGrid[rowChoice, columnChoice + i] == ship || battleshipGrid[rowChoice, columnChoice - i] == ship)
                                    {
                                        Console.WriteLine("INVALID CHOICE");
                                        unicode = 65;
                                        battleshipGrid = tempGrid;
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        battleshipGrid[rowChoice, columnChoice + i] = ship;
                                        battleshipGrid[rowChoice, columnChoice - i] = ship;
                                    }
                                }
                            }
                            else if (choiceH == "vertical")
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (battleshipGrid[rowChoice + i, columnChoice] == ship || battleshipGrid[rowChoice - i, columnChoice] == ship)
                                    {
                                        Console.WriteLine("INVALID CHOICE");
                                        unicode = 65;
                                        battleshipGrid = tempGrid;
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        battleshipGrid[rowChoice + i, columnChoice] = ship;
                                        battleshipGrid[rowChoice - i, columnChoice] = ship;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("INVALID CHOICE");
                                Thread.Sleep(500);
                                unicode = 65;
                                battleshipGrid = tempGrid;
                            }
                            carrierNumber--;
                            unicode = 65;
                            tempGrid = battleshipGrid;

                        }
                        else
                        {
                            Console.WriteLine("INVALID CHOICE");
                            Thread.Sleep(500);
                            unicode = 65;
                            battleshipGrid = tempGrid;
                        }
                    }
                }
                else if (choice == 2)
                {
                    if (battleshipNumber == 0)
                    {
                        Console.WriteLine("You have already chosen the location for this ship");
                        Thread.Sleep(50);
                        unicode = 65;
                    }
                    else
                    {
                        int rowChoice = row();
                        int columnChoice = column();
                        Console.WriteLine("Would you like the ship to be horizontal or vertical?");
                        string choiceH = Console.ReadLine().ToLower();
                        if (rowChoice > 0 && columnChoice > 0 && rowChoice < 7 && columnChoice < 7)
                        {
                            if (choiceH == "horizontal")
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (battleshipGrid[rowChoice, columnChoice + i] == ship)
                                    {
                                        Console.WriteLine("INVALID CHOICE");
                                        unicode = 65;
                                        battleshipGrid = tempGrid;
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        battleshipGrid[rowChoice, columnChoice + i] = ship;
                                    }
                                }
                            }
                            else if (choiceH == "vertical")
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (battleshipGrid[rowChoice + i, columnChoice] == ship)
                                    {
                                        Console.WriteLine("INVALID CHOICE");
                                        unicode = 65;
                                        battleshipGrid = tempGrid;
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        battleshipGrid[rowChoice + i, columnChoice] = ship;

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("INVALID CHOICE");
                                Thread.Sleep(500);
                                unicode = 65;
                                battleshipGrid = tempGrid;
                            }
                            battleshipNumber--;
                            unicode = 65;
                            tempGrid = battleshipGrid;
                        }
                        else
                        {
                            Console.WriteLine("INVALID CHOICE");
                            Thread.Sleep(500);
                            unicode = 65;
                        }
                    }
                }
                else if (choice == 3)
                {
                    if (cruiserNumber == 0)
                    {
                        Console.WriteLine("You have already chosen the location for this ship");
                        Thread.Sleep(500);
                        unicode = 65;
                    }
                    else
                    {
                        int rowChoice = row();
                        int columnChoice = column();
                        Console.WriteLine("Would you like the ship to be horizontal or vertical?");
                        string choiceH = Console.ReadLine().ToLower();
                        if (rowChoice >= 1 && columnChoice >= 1 && rowChoice <= 9 && columnChoice <= 9)
                        {
                            if (choiceH == "horizontal")
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (battleshipGrid[rowChoice, columnChoice + i] == ship || battleshipGrid[rowChoice, columnChoice - i] == ship)
                                    {
                                        Console.WriteLine("INVALID CHOICE");
                                        unicode = 65;
                                        battleshipGrid = tempGrid;
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        battleshipGrid[rowChoice, columnChoice + i] = ship;
                                        battleshipGrid[rowChoice, columnChoice - i] = ship;

                                    }
                                }
                            }
                            else if (choiceH == "vertical")
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (battleshipGrid[rowChoice + i, columnChoice] == ship || battleshipGrid[rowChoice - i, columnChoice] == ship)
                                    {
                                        Console.WriteLine("INVALID CHOICE");
                                        unicode = 65;
                                        battleshipGrid = tempGrid;
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        battleshipGrid[rowChoice + i, columnChoice] = ship;
                                        battleshipGrid[rowChoice - i, columnChoice] = ship;

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("INVALID CHOICE");
                                Thread.Sleep(500);
                                unicode = 65;
                                battleshipGrid = tempGrid;
                            }
                            cruiserNumber--;
                            unicode = 65;
                            tempGrid = battleshipGrid;
                        }
                        else
                        {
                            Console.WriteLine("INVALID CHOICE");
                            Thread.Sleep(500);
                            unicode = 65;
                            battleshipGrid = tempGrid;
                        }
                    }
                }
                else if (choice == 4)
                {
                    if (destroyerNumber == 0)
                    {
                        Console.WriteLine("You have already chosen the location for this ship");
                        Thread.Sleep(500);
                        unicode = 65;
                    }
                    else
                    {
                        int rowChoice = row();
                        int columnChoice = column();
                        Console.WriteLine("Would you like the ship to be horizontal or vertical?");
                        string choiceH = Console.ReadLine().ToLower();
                        if (rowChoice >= 2 && columnChoice >= 2 && rowChoice <= 8 && columnChoice <= 8)
                        {
                            if (choiceH == "horizontal")
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (battleshipGrid[rowChoice, columnChoice + i] == ship || battleshipGrid[rowChoice, columnChoice - i] == ship)
                                    {
                                        Console.WriteLine("INVALID CHOICE");
                                        unicode = 65;
                                        battleshipGrid = tempGrid;
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        battleshipGrid[rowChoice, columnChoice + i] = ship;

                                    }
                                }
                            }
                            else if (choiceH == "vertical")
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (battleshipGrid[rowChoice + i, columnChoice] == ship || battleshipGrid[rowChoice - i, columnChoice] == ship)
                                    {
                                        Console.WriteLine("INVALID CHOICE");
                                        unicode = 65;
                                        battleshipGrid = tempGrid;
                                        Thread.Sleep(500);
                                    }
                                    else
                                    {
                                        battleshipGrid[rowChoice + i, columnChoice] = ship;

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("INVALID CHOICE");
                                Thread.Sleep(500);
                                unicode = 65;
                                battleshipGrid = tempGrid;
                            }
                            destroyerNumber--;
                            unicode = 65;
                            tempGrid = battleshipGrid;
                        }
                        else
                        {
                            Console.WriteLine("INVALID CHOICE");
                            Thread.Sleep(500);
                            unicode = 65;
                            destroyerNumber++;
                            battleshipGrid = tempGrid;
                        }
                    }
                }
                else if (choice == 5)
                {
                    if (submarineNumber == 0)
                    {
                        Console.WriteLine("You have already chosen the location for this ship");
                        Thread.Sleep(500);
                        unicode = 65;
                    }
                    else
                    {
                        int rowChoice = row();
                        int columnChoice = column();
                        Console.WriteLine("Would you like the ship to be horizontal or vertical?");
                        string choiceH = Console.ReadLine().ToLower();

                        if (choiceH == "horizontal")
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                if (battleshipGrid[rowChoice, columnChoice + i] == ship || battleshipGrid[rowChoice, columnChoice - i] == ship)
                                {
                                    Console.WriteLine("INVALID CHOICE");
                                    unicode = 65;
                                    battleshipGrid = tempGrid;
                                    Thread.Sleep(500);
                                }
                                else
                                {
                                    battleshipGrid[rowChoice, columnChoice + i] = ship;
                                    battleshipGrid[rowChoice, columnChoice - i] = ship;

                                }
                            }
                        }
                        else if (choiceH == "vertical")
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                if (battleshipGrid[rowChoice + i, columnChoice] == ship || battleshipGrid[rowChoice - i, columnChoice] == ship)
                                {
                                    Console.WriteLine("INVALID CHOICE");
                                    unicode = 65;
                                    battleshipGrid = tempGrid;
                                    Thread.Sleep(500);
                                }
                                else
                                {
                                    battleshipGrid[rowChoice + i, columnChoice] = ship;
                                    battleshipGrid[rowChoice - i, columnChoice] = ship;

                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("INVALID CHOICE");
                            Thread.Sleep(500);
                            unicode = 65;
                            battleshipGrid = tempGrid;
                        }
                        submarineNumber--;
                        unicode = 65;
                        tempGrid = battleshipGrid;
                    }
                }
                else if (choice == 6)
                {
                    if (carrierNumber == 0 && battleshipNumber == 0 && cruiserNumber == 0 && destroyerNumber == 0 && submarineNumber == 0)
                    {
                        break;
                    }
                    else
                    {
                        unicode = 65;
                        Console.WriteLine("You must choose a location for all ships first!");
                        Thread.Sleep(1000);
                    }
                }
                unicode = 65;
            } while (true);
        }
        static void Player()
        {
            Console.Title = ("BattleShip - Playing Versus a Player");
            Console.Clear();
            fillBoard(playerGrid);
            shipLocationChooser();
            do
            {
                Console.Clear();
                player2 = true;
                unicode = 65;
                printBoard();
                Console.WriteLine("");
                int rowChoice = row();
                int columnChoice = column();
                if (battleshipGrid[rowChoice, columnChoice] == ship)
                {
                    playerGrid[rowChoice, columnChoice] = hitShip;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" __    __   __  .___________. __ ");
                    Console.WriteLine("|  |  |  | |  | |           ||  |");
                    Console.WriteLine("|  |__|  | |  | `---|  |----`|  |");
                    Console.WriteLine("|   __   | |  |     |  |     |  |");
                    Console.WriteLine("|  |  |  | |  |     |  |     |__|");
                    Console.WriteLine("|__|  |__| |__|     |__|     (__)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(500);
                    Console.Clear();
                    player2 = false;
                    unicode = 65;
                    timesHit++;
                    scorePlayer--;
                }
                else
                {
                    playerGrid[rowChoice, columnChoice] = missShip;
                    Console.Clear();
                    player2 = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("ooo        ooooo ooooo  .oooooo..o  .oooooo..o");
                    Console.WriteLine("`88.       .888' `888' d8P'    `Y8 d8P'    `Y8");
                    Console.WriteLine(" 888b     d'888   888  Y88bo.      Y88bo.     ");
                    Console.WriteLine(" 8 Y88. .P  888   888   `'Y8888o.   `'Y8888o. ");
                    Console.WriteLine(" 8  `888'   888   888       `'Y88b      `'Y88b");
                    Console.WriteLine(" 8    Y     888   888  oo     .d8P oo     .d8P");
                    Console.WriteLine("o8o        o888o o888o 8'''88888P'  8''88888P' ");
                    Console.ForegroundColor = ConsoleColor.White;
                    unicode = 65;
                    Thread.Sleep(500);
                    scorePlayer--;
                }
                player2 = false;
            } while (timesHit != 18);
            GameOver(scorePlayer);
        }

        static int row()
        {
            do
            {
                Console.WriteLine("Please enter the row: ");
                char rowStr = Convert.ToChar(Console.ReadLine().ToUpper());
                int rowInt = Convert.ToInt32(rowStr);
                rowInt = rowInt - 65;
                if (rowInt > 10)
                {
                    Console.WriteLine("INVALID ROW");
                }
                else
                {
                    return rowInt;
                }
            } while (true);
        }

        static int column()
        {
            do
            {
                Console.WriteLine("Please enter the column: ");
                int columnInt = Convert.ToInt32(Console.ReadLine());
                if (columnInt > 10)
                {
                    Console.WriteLine("INVALID ROW");
                }
                else
                {
                    return columnInt - 1;
                }
            } while (true);
        }

        static void PC()
        {
            Console.Title = ("BattleShip - Playing Versus a PC");
            Console.Clear();
            fillBoard(playerGrid);
            shipLocationChooser();
            do
            {
                player2 = true;
                unicode = 65;
                Console.Clear();
                printBoard();
                Thread.Sleep(300);
                Random rand = new Random();
                int choiceRow = rand.Next(0, 10);
                int choiceColumn = rand.Next(0, 10);
                if (playerGrid[choiceRow, choiceColumn] == water)
                {
                    if (battleshipGrid[choiceRow, choiceColumn] == ship)
                    {
                        playerGrid[choiceRow, choiceColumn] = hitShip;
                        timesHit++;
                        scorePC++;
                    }
                    else
                    {
                        playerGrid[choiceRow, choiceColumn] = missShip;
                        scorePC++;
                    }
                }
                else
                {

                }
            } while (timesHit != 18);
            GameOver(scorePC);
        }

        static void GameOver(int score)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  .oooooo.          .o.       ooo        ooooo oooooooooooo");
            Console.WriteLine(" d8P'  `Y8b        .888.      `88.       .888' `888'     `8");
            Console.WriteLine("888               .8'888.      888b     d'888   888        ");
            Console.WriteLine("888              .8' `888.     8 Y88. .P  888   888oooo8   ");
            Console.WriteLine("888     ooooo   .88ooo8888.    8  `888'   888   888    '   ");
            Console.WriteLine("`88.    .88'   .8'     `888.   8    Y     888   888       o");
            Console.WriteLine(" `Y8bood8P'   o88o     o8888o o8o        o888o o888ooooood8");
            Console.WriteLine("\n\n");
            Console.WriteLine("     .oooooo.   oooooo     oooo oooooooooooo ooooooooo.  ");
            Console.WriteLine("    d8P'  `Y8b   `888.     .8'  `888'     `8 `888   `Y88.");
            Console.WriteLine("   888      888   `888.   .8'    888          888.d88'");
            Console.WriteLine("   888      888    `888. .8'     888oooo8     888ooo88P' ");
            Console.WriteLine("   888      888     `888.8'      888    '     888`88b.   ");
            Console.WriteLine("   `88b    d88'      `888'       888       o  888  `88b. ");
            Console.WriteLine("    `Y8bood8P'        `8'       o888ooooood8 o888o  o888o");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nYou scored {0}", score);
        }

        static void menuArt()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("oooooooooo.        .o.       ooooooooooooo ooooooooooooo ooooo        oooooooooooo");
            Console.WriteLine("`888'   `Y8b      .888.      8'   888   `8 8'   888   `8 `888'        `888'     `8");
            Console.WriteLine(" 888     888     .8'888.          888           888       888          888        ");
            Console.WriteLine(" 888oooo888'    .8' `888.         888           888       888          888oooo8   ");
            Console.WriteLine(" 888    `88b   .88ooo8888.        888           888       888          888    '   ");
            Console.WriteLine(" 888    .88P  .8'     `888.       888           888       888       o  888       o");
            Console.WriteLine("o888bood8P'  o88o     o8888o     o888o         o888o     o888ooooood8 o888ooooood8");
            Console.WriteLine("\n");
            Console.WriteLine("  .oooooo..o ooooo   ooooo ooooo ooooooooo.    .oooooo..o");
            Console.WriteLine(" d8P'    `Y8 `888'   `888' `888' `888   `Y88. d8P'    `Y8");
            Console.WriteLine(" Y88bo.       888     888   888   888.d88' Y88bo.     ");
            Console.WriteLine("  `'Y8888o.   888ooooo888   888   888ooo88P'   `'Y8888o. ");
            Console.WriteLine("      `'Y88b  888     888   888   888              `'Y88b");
            Console.WriteLine(" oo     .d8P  888     888   888   888         oo     .d8P");
            Console.WriteLine("8'''88888P'  o888o   o888o o888o o888o        8'''88888P' ");
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~oo~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                 o o");
            Console.WriteLine("                                 o ooo");
            Console.WriteLine("                                   o oo");
            Console.WriteLine("                                      o o      |   #)");
            Console.WriteLine("                                       oo     _|_|_#_");
            Console.WriteLine("                                         o   |       |");
            Console.WriteLine("    __                    ___________________|       |_________________");
            Console.WriteLine("   |   -_______-----------                                              | ");
            Console.WriteLine("  >|    _____                                                   --->     )");
            Console.WriteLine("   |__ -     ---------_________________________________________________ /");
            Console.WriteLine("\n\n\n");
        }
    }
}
