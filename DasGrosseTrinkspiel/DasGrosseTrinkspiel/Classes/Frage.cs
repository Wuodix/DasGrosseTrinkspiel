using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DasGrosseTrinkspiel.Classes
{
    internal class Frage
    {
        public string Text { get; set; }
        [Indexed]
        public int Kategorie { get; set; }
        [Indexed]
        public int Spiel { get; set; }
    }
}
