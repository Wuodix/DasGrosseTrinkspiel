﻿using System;
using System.Collections.Generic;
using System.Text;
using DasGrosseTrinkspiel.Classes;

namespace DasGrosseTrinkspiel.Extentions
{
    internal class DataHolder
    {
        public static ClsKartenspiel Katenspiel { get; set; }
        public static List<ClsFrage> Fragen { get; set; }
    }
}
