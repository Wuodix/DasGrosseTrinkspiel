using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DasGrosseTrinkspiel.Classes
{
    public class ClsSpieler
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Geschlecht { get; set; }
        [ManyToOne,ForeignKey(typeof(ClsSpielerliste))]
        public int Listennummer { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
