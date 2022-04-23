using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using DasGrosseTrinkspiel.Extentions;
using DasGrosseTrinkspiel.Views;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;

namespace DasGrosseTrinkspiel.Classes
{
    abstract class ClsSpielAbstract
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
    class ClsKartenspiel : ClsSpielAbstract
    {
        protected List<ClsFrage> m_fragen = new List<ClsFrage>(), m_randomizedFragen, m_previousFragen;
        protected List<ClsSpieler> m_spieler;
        protected int m_selectedQuestion; //Nummer (in der Liste --> []) der ausgewählten Frage
        protected Random m_random = new Random();
        protected bool m_end = true;

        public async void Start(List<ClsKategorie> Fragenkategorien, ClsSpielerliste Spielerliste)
        {
            m_spieler = await DataProvider.GetSpieler(Spielerliste.Id);

            foreach(ClsKategorie kategorie in Fragenkategorien)
            {
                m_fragen.AddRange(await DataProvider.GetFrage(kategorie.ID));
            }
            m_randomizedFragen = m_fragen.OrderBy(x => m_random.Next()).ToList();
            m_selectedQuestion = -1;
            NavigationPage page = new NavigationPage(NextCard());
            NavigationPage.SetHasNavigationBar(page, false);
            App.Current.MainPage = page;
        }

        public ContentPage NextCard()
        {
            if(m_selectedQuestion + 1 > m_fragen.Count-1)
            {
                m_selectedQuestion = 0;
                if (m_end)
                {
                    if(m_previousFragen != null)
                    {
                        (m_randomizedFragen, m_previousFragen) = (m_previousFragen, m_randomizedFragen);
                    }
                    else
                    {
                        m_previousFragen = m_randomizedFragen;
                        m_randomizedFragen = m_fragen.OrderBy(x => m_random.Next()).ToList();
                    }
                    m_end = false;
                }
                else
                {
                    m_previousFragen = m_randomizedFragen;
                    m_randomizedFragen = m_fragen.OrderBy(x => m_random.Next()).ToList();
                }
            }
            else
            {
                m_selectedQuestion++;
            }
            return new CardGamePage(m_randomizedFragen[m_selectedQuestion].Text, Name);
        }
        public bool PreviousCard()
        {
            if(m_selectedQuestion > 0)
            {
                m_selectedQuestion--;

                return false;
            }
            else if(!m_end)
            {
                (m_randomizedFragen, m_previousFragen) = (m_previousFragen, m_randomizedFragen);
                m_end = true;
                m_selectedQuestion = m_fragen.Count - 1;

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
