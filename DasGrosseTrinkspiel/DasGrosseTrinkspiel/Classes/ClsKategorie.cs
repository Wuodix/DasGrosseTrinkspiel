using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DasGrosseTrinkspiel.Classes
{
    internal class ClsKategorie
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
