namespace WizardLore
{
    public class Hexagon
    {
        public Unit unit { get; set; }
        public Obstacle obstacle { get; set; }

        public Hexagon(Unit unit, Obstacle obstacle)
        {
            this.obstacle = obstacle;
            this.unit = unit;
        }
    }
}