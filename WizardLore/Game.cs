#nullable enable
using System;
using System.Collections.Generic;

namespace WizardLore
{
    public class Game
    {
        public Board board;
        public Team currentPlayer {get; set; }
        //public Hexagon[] units;
        //public Hexagon[] obstacles;
        
        public Game(Board board, Team currentPlayer = Team.PLAYER1)
        {
            this.board = board;
            this.currentPlayer = currentPlayer;
            
            Hexagon[] obstacle = board.ObstacleInfo();
        }

        public void Play()
        {
            Console.WriteLine("Press any key to continue. Press q to quit");
            string input = Console.ReadLine();

            if (input != "q")
            {
                Console.Clear();
                Printer.PrintBoard(board);
            }
            List<Hexagon> playerUnit = ActivateUnit();
            Movement(playerUnit);
        }

        private List<Hexagon> ActivateUnit()
        {
            string input;
            Hexagon[] units = board.UnitInfo();
            while (true)
            {
                Console.WriteLine("How many units do you want to use? (1 or 2): ");
                input = Console.ReadLine();
                if (input == "1" || input == "2")
                    break;
            }
            Console.WriteLine("Enter the units");
            int limit = Int32.Parse(input);
            List<Hexagon> playerUnit = new List<Hexagon>();
            int i = 0;
            while (limit != playerUnit.Count)
            {
                if (i < 0)
                    i = units.Length - 1;
                else if (i > units.Length - 1)
                    i = 0;

                if (units[i].Unit.Team != currentPlayer || playerUnit.Contains(units[i]))
                    i++;
                else
                {
                    Printer.PlaceCursor(board, units[i].Position, currentPlayer);
                    ConsoleKeyInfo cki = Console.ReadKey();
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        playerUnit.Add(units[i]);
                        if (playerUnit.Count != limit)
                            Console.WriteLine("Enter the units");
                    }
                    else if (cki.Key == ConsoleKey.LeftArrow)
                        i--;
                    else if (cki.Key == ConsoleKey.RightArrow)
                        i++;
                }
            }
            return playerUnit;
            
        }

        private void Movement(List<Hexagon> playerUnit)
        {
            int i = 0;
            List<Hexagon> immonde = new List<Hexagon>();
            immonde.Add(playerUnit[0]);
            if (playerUnit.Count == 2)
                immonde.Add(playerUnit[1]);
            Console.WriteLine("Where do you want it to move? (or not): ");
            while (i < playerUnit.Count)
            {
                (int x, int y) = (playerUnit[i].Position.X, playerUnit[i].Position.Y);
                
                if (immonde[i].Position.X < x - 1)
                {
                    immonde[i].Position.X = x + 1;
                    if (playerUnit[i].Unit.type == UnitType.broomWizard)
                    {
                        immonde[i].Position.X = x - 2;
                        if (immonde[i].Position.X < x - 2)
                            immonde[i].Position.X = x + 1;
                    }
                }
                if (immonde[i].Position.Y < y - 1)
                {
                    immonde[i].Position.Y = y + 1;
                    if (playerUnit[i].Unit.type == UnitType.broomWizard)
                    {
                        immonde[i].Position.Y = y - 2;
                        if (immonde[i].Position.Y < y - 2)
                            immonde[i].Position.Y = y + 1;
                    }
                }

                if (board[immonde[i].Position].Obstacle == Obstacle.RIFT)
                {
                    if (playerUnit[i].Unit.type != UnitType.broomWizard)
                        immonde[i].Position = playerUnit[i].Position;
                }
                
                Printer.PlaceCursor(board, immonde[i].Position, currentPlayer);
                ConsoleKeyInfo cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Enter)
                {
                    i++;
                    if (i < playerUnit.Count)
                        Console.WriteLine("Where do you want it to move? (or not): ");
                }
                else if (cki.Key == ConsoleKey.LeftArrow)
                    immonde[i].Position.X -= 1; 
                else if (cki.Key == ConsoleKey.RightArrow)
                    immonde[i].Position.X += 1; 
                else if (cki.Key == ConsoleKey.UpArrow)
                    immonde[i].Position.Y += 1; 
                else if (cki.Key == ConsoleKey.DownArrow)
                    immonde[i].Position.Y -= 1;
            }
        }
        
    }
}