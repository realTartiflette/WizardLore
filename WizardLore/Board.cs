using System;

namespace WizardLore
{
    public class Board
    {
        public Hexagon[,,] board;
        public int dimension;

        public Board(int dimension)
        {
            board = new Hexagon[dimension, dimension, dimension];
            this.dimension = dimension;
        }
        
        public Hexagon GetHexagon(int x, int y, int z)
        {
            throw new NotImplementedException();
        }
        
        

        public Hexagon this[int x, int y, int z] => GetHexagon(x, y, z);
        public Hexagon this[Position posX, Position posY, Position posZ] => 
            GetHexagon(posX.X, posY.Y, posZ.Z);
        
        public Hexagon this[Position pos] => GetHexagon(pos.X, pos.Y, pos.Z);

    }
}