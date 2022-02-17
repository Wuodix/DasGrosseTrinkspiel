using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DasGrosseTrinkspiel.Classes
{
    public class Spieler
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Geschlecht { get; set; }
        public int Listennummer { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
