
using System;

namespace WizardLore
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * You can change anything in this file, but be careful.
             * You still need to implement what is currently here.
             */
            var team = (Team)1;
            Board board = Serialization.Deserialize(@"C:\Users\thoma\WizardLore\given_boards\normal_game.txt", out team);
            Game game = new Game(board);
            game.Play();
            
            
            //Board board = Serialization.Deserialize(@"C:\Users\thoma\WizardLore\given_boards\normal_game.txt", out team);
            //Printer.PrintBoard(board);
            //Console.WriteLine((int)board[0,0,4].Obstacle);
            //Serialization.Serialize(board, @"C:\Users\thoma\WizardLore\given_boards\test.txt", Team.PLAYER1);
            
            //Board board = new Board(8);
            //Printer.PrintBoard(board);
            
        }
    }
}