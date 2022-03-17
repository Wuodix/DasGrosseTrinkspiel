using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DasGrosseTrinkspiel.Classes
{
    abstract class ClsSpiel
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public void Test()
        {

        }

        abstract public void Start();
        abstract public void Stop();
    }
    abstract class ClsKartenspiele : ClsSpiel
    {
        public void NextCard()
        {

        }
    }
}
