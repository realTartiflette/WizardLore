namespace WizardLore
{
    public class Position
    {
        public int X;
        public int Y;
        public int Z;

        public Position(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Position operator +(Position pos1, Position pos2)
        {
            return new Position(pos1.X + pos2.X, pos1.Y + pos2.Y, pos1.Z + pos2.Z);
        }
    }
}