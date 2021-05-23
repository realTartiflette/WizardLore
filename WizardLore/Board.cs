using System;
using System.Collections.Generic;

namespace WizardLore
{
    public class Board
    {
        public Hexagon[,,] board { get; set; }
        public int dimension  { get; set; }

        public Board(int dimension)
        {
            board = new Hexagon[dimension, dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    for (int k = 0; k < dimension; k++)
                    {
                        board[i, j, k] = new Hexagon(null, Obstacle.BATTLEFIELD, new Position(i,j,k));
                    }
                }
            }
            this.dimension = dimension;
        }

        public int GetDimension()
        {
            return dimension;
        }
            
        
        public Hexagon GetHexagon(int x, int y, int z)
        {
            return board[x, y, z];
        }

        public Hexagon[] ObstacleInfo()
        {
            List<Hexagon> obstacles = new List<Hexagon>();
            foreach (var hexagon in board)
            {
                if (hexagon.Obstacle != Obstacle.BATTLEFIELD)
                    obstacles.Add(hexagon);
            }
            return obstacles.ToArray();
        }
        
        public Hexagon[] UnitInfo()
        {
            List<Hexagon> units = new List<Hexagon>();
            foreach (var hexagon in board)
            {
                if (hexagon.Unit != null)
                    units.Add(hexagon);
            }
            return units.ToArray();
        }
        public Hexagon this[int x, int y, int z] => GetHexagon(x, y, z);
        
        public Hexagon this[Position pos] => GetHexagon(pos.X, pos.Y, pos.Z);

    }
}