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

        public ContentPage NextCard(string Frage)
        {
            return new ContentPage();
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

            if(Name == "Picolo Klon")
            {
                ClsSpieler Player1, Player2, Player3, Player4, Player5;
                string Frage1 = "", Frage2 = "", Frage3 = "", Frage4 = "", Frage5 = "";
                int i = 0;
                if (m_randomizedFragen[m_selectedQuestion].Text.Contains("Player1") && m_spieler.Count >= 1)
                {
                    Player1 = m_spieler[m_random.Next(0, m_spieler.Count)];
                    Frage1 = m_randomizedFragen[m_selectedQuestion].Text.Replace("Player1", Player1.Name);
                    i++;

                    if (Frage1.Contains("Player2") && m_spieler.Count >= 2)
                    {
                        Player2 = m_spieler[m_random.Next(0, m_spieler.Count)];
                        while(Player2 == Player1)
                        {
                            Player2 = m_spieler[m_random.Next(0, m_spieler.Count)];
                        }
                        Frage2 = Frage1.Replace("Player2", Player2.Name);
                        i++;

                        if (Frage2.Contains("Player3") && m_spieler.Count >= 3)
                        {
                            Player3 = m_spieler[m_random.Next(0, m_spieler.Count)];
                            while (Player3 == Player1 || Player3 == Player2)
                            {
                                Player3 = m_spieler[m_random.Next(0, m_spieler.Count)];
                            }
                            Frage3 = Frage2.Replace("Player3", Player3.Name);
                            i++;

                            if (Frage3.Contains("Player4") && m_spieler.Count >= 4)
                            {
                                Player4 = m_spieler[m_random.Next(0, m_spieler.Count)];
                                while (Player4 == Player1 || Player4 == Player2 || Player4 == Player3)
                                {
                                    Player4 = m_spieler[m_random.Next(0, m_spieler.Count)];
                                }
                                Frage4 = Frage3.Replace("Player4", Player4.Name);
                                i++;

                                if (Frage4.Contains("Player5") && m_spieler.Count >= 5)
                                {
                                    Player5 = m_spieler[m_random.Next(0, m_spieler.Count)];
                                    while (Player5 == Player1 || Player4 == Player2 || Player4 == Player3 || Player5 == Player4)
                                    {
                                        Player5 = m_spieler[m_random.Next(0, m_spieler.Count)];
                                    }
                                    Frage5 = Frage4.Replace("Player5", Player5.Name);
                                    i++;
                                }
                            }
                        }
                    }
                }
                switch (i)
                {
                    case 1:
                        return new CardGamePage(Frage1);
                    case 2:
                        return new CardGamePage(Frage2);
                    case 3:
                        return new CardGamePage(Frage3);
                    case 4:
                        return new CardGamePage(Frage4);
                    case 5:
                        return new CardGamePage(Frage5);
                }
            }
            return new CardGamePage(m_randomizedFragen[m_selectedQuestion].Text);
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
