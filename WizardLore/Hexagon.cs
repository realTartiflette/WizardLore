using System;

namespace WizardLore
{
    public class Hexagon
    {
        public Unit Unit { get; set; }
        public Obstacle Obstacle { get; set; }
        public Position Position;

        public Hexagon(Unit unit, Obstacle obstacle, Position position)
        {
            
            this.Obstacle = obstacle;
            this.Unit = unit;
            this.Position = position;
        }
        

        public override string ToString()
        {
            if (Unit != null)
            {
                string a = "I";
                if (Unit.type == UnitType.advancedSorcerer)
                    a = "A";
                else if (Unit.type == UnitType.broomWizard)
                    a = "B";

                string b = "B";
                if (Unit.flag == ConsoleColor.Green)
                    b = "G";
                else if (Unit.flag == ConsoleColor.Red)
                    b = "R";
            
                return a + b + Unit.hp;
            }

            return "   ";
        }
    }
}