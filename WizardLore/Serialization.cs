using System;
using System.ComponentModel.Design;
using System.IO;
using System.Net;

namespace WizardLore
{
    public static class Serialization
    {
        /// <summary>
        /// Serialize the given board and player
        /// </summary>
        /// <param name="board"> The game board </param>
        /// <param name="path"> The path to the output file </param>
        /// <param name="currentPlayer"> The enum member representing the player who will play next turn </param>
        public static void Serialize(Board board, string path, Team currentPlayer)
        {
            StreamWriter sw = File.CreateText(path);
            sw.WriteLine("{0} {1}", board.dimension, (int)currentPlayer);
            sw.WriteLine();

            for (int x = 0; x < board.dimension; x++)
            {
                for (int y = 0; y < board.dimension; y++)
                {
                    for (int z = 0; z < board.dimension; z++)
                    {
                        if (board[x, y, z].Obstacle != Obstacle.BATTLEFIELD)
                            sw.WriteLine("{0},{1},{2} {3}",x,y,z,(int)board[x, y, z].Obstacle);
                    }
                }
            }
            sw.WriteLine();
            for (int x = 0; x < board.dimension; x++)
            {
                for (int y = 0; y < board.dimension; y++)
                {
                    for (int z = 0; z < board.dimension; z++)
                    {
                        if (board[x, y, z].Unit != null)
                        {
                            string unit = "A";
                            if (board[x, y, z].Unit.type == UnitType.wandInfantry)
                                unit = "I";
                            if (board[x, y, z].Unit.type == UnitType.broomWizard)
                                unit = "B";

                            string color = "R";
                            if (board[x, y, z].Unit.flag == ConsoleColor.Green)
                                color = "G";
                            if (board[x, y, z].Unit.flag == ConsoleColor.Blue)
                                color = "B";
                            
                            sw.WriteLine("{0} {1},{2},{3} {4} {5} {6}",unit,x,y,z,(int)board[x, y, z].Unit.Team,
                                color, board[x, y, z].Unit.hp);
                        }
                        
                    }
                }
            }
            sw.Close();
            
        }

        /// <summary>
        /// Deserialize a board from the given file
        /// </summary>
        /// <param name="path"> Path to the file </param>
        /// <param name="startingPlayer"> The enum member representing the player who will play next turn </param>
        public static Board Deserialize(string path, out Team startingPlayer)
        {
            Board res = null;
            startingPlayer = Team.PLAYER1;
            if (!File.Exists(path))
            {
                Console.Error.WriteLine("Error: could not open {0}", Path.GetFileName(path));
                return res;
            }
            
            StreamReader sr = new StreamReader(path);
            string firstLine = sr.ReadLine();
            string[] info = firstLine.Split(' ');
            int ninfo;
            if (info.Length == 2)
            {
                if (Int32.TryParse(info[0], out ninfo))
                    res = new Board(ninfo);
                else
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return res;
                }

                if (Int32.TryParse(info[1], out ninfo))
                {
                    if (ninfo == 1)
                        startingPlayer = Team.PLAYER2;
                }
                else
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return res;
                }
            }
            else
            {
                Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                sr.Close();
                return res;
            }

            if (sr.ReadLine() != "")
            {
                Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                sr.Close();
                return null;
            }

            string line;
            while ((line = sr.ReadLine()) != null && line != "")
            {
                
                info = line.Split(',', ' ');
                (int x, int y, int z) = (0, 0, 0);
                if (info.Length == 4)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!Int32.TryParse(info[i], out x))
                        {
                            Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                            sr.Close();
                            return null;
                        }
                    }
                    x = Int32.Parse(info[0]);
                    y = Int32.Parse(info[1]);
                    z = Int32.Parse(info[2]);
                }
                else
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return null;
                }
                
                int obstacle = 1;
                if (!Int32.TryParse(info[3], out obstacle))
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return null;
                }

                //Hexagon hexagon = new Hexagon(null, (Obstacle) obstacle);
                res.board[x, y, z].Obstacle = (Obstacle) obstacle;
            }

            while ((line = sr.ReadLine()) != null)
            {
                info = line.Split(',', ' ');
                (int x, int y, int z) = (0, 0, 0);
                if (info.Length == 7)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (!Int32.TryParse(info[i], out x))
                        {
                            Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                            sr.Close();
                            return null;
                        }
                    }
                    x = Int32.Parse(info[1]);
                    y = Int32.Parse(info[2]);
                    z = Int32.Parse(info[3]);
                }
                else
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return null;
                }
                
                (string unit, int team, string color, int hp) = ("", 0, "", 0);
                Unit nunit;
                unit = info[0];
                if (!Int32.TryParse(info[4], out team))
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return null;
                }

                color = info[5];
                if (!Int32.TryParse(info[6], out hp))
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return null;
                }

                ConsoleColor ccolor;
                if (color == "R")
                    ccolor = ConsoleColor.Red;
                else if (color == "G")
                    ccolor = ConsoleColor.Green;
                else if (color == "B")
                    ccolor = ConsoleColor.Blue;
                else
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return null;
                }

                int unitType;
                if (unit == "A")
                    unitType = 3;
                else if (unit == "B")
                    unitType = 2;
                else if (unit == "I")
                    unitType = 1;
                else
                {
                    Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                    sr.Close();
                    return null;
                }

                /*Hexagon hexa = res[x, y, z];
                Obstacle lastObstacle = res[x, y, z].GetObstacle();
                nunit = new Unit(hp, ccolor, (Team) team, (UnitType) unitType);
                Hexagon hexagon = new Hexagon(nunit, lastObstacle);*/
                
                res.board[x, y, z].Unit = new Unit(hp, ccolor, (Team) team, (UnitType) unitType);
            }

            if (sr.ReadLine() != null)
            {
                Console.Error.WriteLine("Error: could not deserialize {0}", Path.GetFileName(path));
                sr.Close();
                return null;
            }
            sr.Close();
            return res;
        }
    }
}