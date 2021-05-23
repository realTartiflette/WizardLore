using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace WizardLore
{
    public enum UnitType
    {
        wandInfantry = 1,
        broomWizard = 2,
        advancedSorcerer = 3
        
    }

    public enum Team
    {
        PLAYER1 = 1,
        PLAYER2 = 2,
    }
    
    public enum Obstacle
    {
        BATTLEFIELD,
        FOREST,
        MOUNTAIN,
        RIFT
    }

    public class Unit
    {
        public int hp;
        public ConsoleColor flag { get; }
        public Team Team { get; }
        //public Position position;
        public UnitType type { get; }

        public Unit(int hp, ConsoleColor flag, Team team, UnitType type)
        {
            this.hp = hp;
            this.flag = flag;
            this.Team = team;
            //this.position = position;
            this.type = type;
        }

        public override string ToString()
        {
            string a = "I";
            if (type == UnitType.advancedSorcerer)
                a = "A";
            else if (type == UnitType.broomWizard)
                a = "B";

            string b = "B";
            if (flag == ConsoleColor.Green)
                b = "G";
            else if (flag == ConsoleColor.Red)
                b = "R";
            
            return a + b + hp;
        }
        
        
    }
    
    
}