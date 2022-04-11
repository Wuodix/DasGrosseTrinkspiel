using System;
using System.Collections.Generic;
using System.Text;
using DasGrosseTrinkspiel.Classes;

namespace DasGrosseTrinkspiel.Extentions
{
    internal class DataHolder
    {
        public static List<ClsFrage> Fragen { get; set; } = new List<ClsFrage>();
        public static List<ClsKategorie> Kategorien { get; set; } = new List<ClsKategorie>();
        public static List<ClsSpiel> Spiele { get; set; } = new List<ClsSpiel>();
    }
}
