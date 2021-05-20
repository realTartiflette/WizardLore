using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace WizardLore
{
    public enum UnitType
    {
        wandInfantry,
        broomWizard,
        advancedSorcerer
        
    }

    public enum Team
    {
        Player1,
        Player2
    }

    public abstract class Unit
    {
        public int hp;
        public ConsoleColor flag { get; }
        public Team team;
        public Position position;
        public UnitType type { get; }

        public Unit(int hp, ConsoleColor flag, Team team, Position position, UnitType type)
        {
            this.hp = hp;
            this.flag = flag;
            this.team = team;
            this.position = position;
            this.type = type;
        }
        
        public string Print()
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

    public class Sorcier : Unit
    {
        public Sorcier(int hp, ConsoleColor flag, Team team, Position position, UnitType type) : base(hp, flag, team, position, type)
        {
        }
    }
    
}