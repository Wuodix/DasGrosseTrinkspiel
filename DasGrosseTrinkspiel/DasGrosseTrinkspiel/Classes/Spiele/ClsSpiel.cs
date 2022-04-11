using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using DasGrosseTrinkspiel.Extentions;
using System.Diagnostics;

namespace DasGrosseTrinkspiel.Classes
{
    abstract class ClsSpiel
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
        abstract public void Stop();
    }
    class ClsKartenspiel : ClsSpiel
    {
        private List<ClsFrage> m_fragen;
        private List<ClsSpieler> m_spieler;
        protected int m_selectedQuestion; //Nummer (in der Liste --> []) der ausgewählten Frage

        public ClsKartenspiel(List<ClsKategorie> Fragenkategorien, ClsSpielerliste Spielerliste)
        {
            Start(Fragenkategorien, Spielerliste);
            m_fragen = new List<ClsFrage>();
        }

        public async void Start(List<ClsKategorie> Fragenkategorien, ClsSpielerliste Spielerliste)
        {
            m_spieler = await DataProvider.GetSpieler(Spielerliste.Id);

            foreach(ClsKategorie kategorie in Fragenkategorien)
            {
                m_fragen.AddRange(await DataProvider.GetFrage(kategorie.ID));
            }
        }

        public void NextCard()
        {

        }
        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
