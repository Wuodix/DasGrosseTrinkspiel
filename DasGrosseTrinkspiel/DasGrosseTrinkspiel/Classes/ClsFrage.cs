using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DasGrosseTrinkspiel.Classes
{
    internal class ClsFrage
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public int Kategorie { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
