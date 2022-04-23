using System;
using System.Collections.Generic;
using System.Text;
using DasGrosseTrinkspiel.Classes;

namespace DasGrosseTrinkspiel.Extentions
{
    internal class DataHolder
    {
        //X in der Ecke überarbeiten (Größe passt nicht); den Spieltyp aus einer Liste (in Data Holder) auslesen lassen
        //und zb nur int vom Spiel weitergeben. Im Endeffekt darf der Spielname nur einmal eingetragen werden
        //und alles muss sich danach richten (eingetragen in CSV und die Data Holder Liste wird mit den Daten
        //der CSV bzw. der Database (wenn diese gefüllt) ist gefüllt.
        public static ClsKartenspiel Kartenspiel { get; set; }
        public static List<ClsFrage> Fragen { get; set; }
        public static List<ClsSpiel> Spiele { get; set; } = new List<ClsSpiel>();
    }
}
