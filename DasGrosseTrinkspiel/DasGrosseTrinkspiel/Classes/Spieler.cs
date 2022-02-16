using System;
using System.Collections.Generic;
using System.Text;

namespace DasGrosseTrinkspiel.Classes
{
    public class Spieler
    {
        public string Name { get; set; }
        public string Geschlecht { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
