using System;
using System.Collections.Generic;
using DasGrosseTrinkspiel.Views;
using DasGrosseTrinkspiel.Classes;
using DasGrosseTrinkspiel.Extentions;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace DasGrosseTrinkspiel.Classes
{
    class ClsPicolo : ClsKartenspiel
    {
        public ClsPicolo(List<ClsKategorie> Fragen, ClsSpielerliste Spielerliste)
        {
            Start(Fragen, Spielerliste);
        }
        private async void Start(List<ClsKategorie> Fragenkategorien, ClsSpielerliste Spielerliste)
        {
            Spieltyp = Spiel.Picolo;
            m_spieler = await DataProvider.GetSpieler(Spielerliste.Id);

            foreach (ClsKategorie kategorie in Fragenkategorien)
            {
                m_fragen.AddRange(await DataProvider.GetFrage(kategorie.ID));
            }
            m_randomizedFragen = m_fragen.OrderBy(x => m_random.Next()).ToList();
            m_selectedQuestion = -1;
            NavigationPage page = new NavigationPage(NextCard());
            NavigationPage.SetHasNavigationBar(page, false);
            App.Current.MainPage = page;
        }

        new public ContentPage NextCard()
        {
            if (m_selectedQuestion + 1 > m_fragen.Count - 1)
            {
                m_selectedQuestion = 0;
                if (m_end)
                {
                    if (m_previousFragen != null)
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

            if (m_randomizedFragen[m_selectedQuestion].Text.Contains("Player1") && m_spieler.Count >= 1)
            {
                ClsSpieler Player1, Player2, Player3, Player4, Player5;
                List<ClsSpieler> tempPlayerList = m_spieler;
                string Frage1 = "", Frage2 = "", Frage3 = "", Frage4 = "", Frage5 = "";
                int i = 0;

                Player1 = tempPlayerList[m_random.Next(0, m_spieler.Count)];
                tempPlayerList.Remove(Player1);
                Frage1 = m_randomizedFragen[m_selectedQuestion].Text.Replace("Player1", Player1.Name);
                i++;

                if (Frage1.Contains("Player2") && m_spieler.Count >= 2)
                {
                    Player2 = tempPlayerList[m_random.Next(0, m_spieler.Count)];
                    tempPlayerList.Remove(Player2);
                    Frage2 = Frage1.Replace("Player2", Player2.Name);
                    i++;

                    if (Frage2.Contains("Player3") && m_spieler.Count >= 3)
                    {
                        Player3 = tempPlayerList[m_random.Next(0, m_spieler.Count)];
                        tempPlayerList.Remove(Player3);
                        Frage3 = Frage2.Replace("Player3", Player3.Name);
                        i++;

                        if (Frage3.Contains("Player4") && m_spieler.Count >= 4)
                        {
                            Player4 = tempPlayerList[m_random.Next(0, m_spieler.Count)];
                            tempPlayerList.Remove(Player4);
                            Frage4 = Frage3.Replace("Player4", Player4.Name);
                            i++;

                            if (Frage4.Contains("Player5") && m_spieler.Count >= 5)
                            {
                                Player5 = tempPlayerList[m_random.Next(0, m_spieler.Count)];
                                tempPlayerList.Remove(Player5);
                                Frage5 = Frage4.Replace("Player5", Player5.Name);
                                i++;
                            }
                        }
                    }
                }
                switch (i)
                {
                    case 1:
                        return new CardGamePage(Frage1, Spieltyp);
                    case 2:
                        return new CardGamePage(Frage2, Spieltyp);
                    case 3:
                        return new CardGamePage(Frage3, Spieltyp);
                    case 4:
                        return new CardGamePage(Frage4, Spieltyp);
                    case 5:
                        return new CardGamePage(Frage5, Spieltyp);
                }
            }
            return new CardGamePage(m_randomizedFragen[m_selectedQuestion].Text, Spieltyp);
        }
    }
}
