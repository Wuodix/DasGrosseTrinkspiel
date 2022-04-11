using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using DasGrosseTrinkspiel.Classes;
using DasGrosseTrinkspiel.Extentions;

namespace DasGrosseTrinkspiel.Views
{
    public partial class SpielerMenu : ContentPage
    {
        static ViewModels.ListViewViewModel m_listViewModel;
        static ViewModels.SpielerMenuViewModel m_viewModel;
        static List<string> m_neueSpieler = new List<string>();
        static bool m_gespeichert;
        public SpielerMenu()
        {
            InitializeComponent();

            if(m_viewModel == null)
            {
                BindingContext = m_viewModel = new ViewModels.SpielerMenuViewModel();
                m_listViewModel = new ViewModels.ListViewViewModel();
            }
            else
            {
                BindingContext = m_viewModel;
            }

            m_gespeichert = true;
            m_neueSpieler.Clear();

            Debug.WriteLine("Spieleranzahl Start von Spielermenu: " + m_viewModel.Gamers.Count);
            foreach (ClsSpieler sp in m_viewModel.Gamers)
            {
                Debug.WriteLine("1"+sp);
            }
        }

        public static ViewModels.ListViewViewModel ListviewviewModel { get { return m_listViewModel; } set { m_listViewModel = value; } }
        public static ViewModels.SpielerMenuViewModel SpielerviewModel { get { return m_viewModel; } set { m_viewModel = value; } }

        private async void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    if (!m_gespeichert)
                    {
                        foreach (ClsSpieler spieler in m_viewModel.Gamers)
                        {
                            foreach (string name in m_neueSpieler)
                            {
                                if (name == spieler.Name)
                                {
                                    await DataProvider.AddSpieler(name, FindSpieler(name).Geschlecht);
                                }
                            }
                        }
                    }
                    m_viewModel.Gamers.Clear();
                    App.Current.MainPage = new MainMenu();
                    break;
            }
        }

        private async void m_btnAdd_Clicked(object sender, EventArgs e)
        {
            if(m_tbxName.Text != null && m_cmbxGender.SelectedItem != null)
            {
                if (SpielerGibtsSchon(m_tbxName.Text)) { await DisplayAlert("Achtung", "Es gibt bereits einen Spieler mit diesem Namen!", "OK"); }
                else
                {
                    ClsSpieler spieler = new ClsSpieler
                    {
                        Name = m_tbxName.Text,
                        Geschlecht = m_cmbxGender.SelectedItem.ToString(),
                    };

                    m_gespeichert = false;
                    m_viewModel.Gamers.Add(spieler);
                    m_neueSpieler.Add(spieler.Name);

                    m_tbxName.Text = null;
                    m_cmbxGender.SelectedItem = null;
                }
            }
        }

        private bool SpielerGibtsSchon(string name)
        {
            foreach(ClsSpieler spieler in m_viewModel.Gamers)
            {
                if(spieler.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        private async void m_btnDelete_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Grid grid = button.Parent as Grid;
            Label label = grid.Children[0] as Label;
            string text = label.Text;

            Debug.WriteLine("text " + text);
            foreach(string name in m_neueSpieler)
            {
                if(name == text)
                {
                    m_neueSpieler.Remove(name);
                    m_viewModel.Gamers.Remove(FindSpieler(name));
                    return;
                }

                Debug.WriteLine("name " + name);
            }
            await DataProvider.DeleteSpieler(FindSpieler(text).Id);
            m_viewModel.Gamers.Remove(FindSpieler(text));
        }

        private ClsSpieler FindSpieler(string name)
        {
            foreach(ClsSpieler spieler in m_viewModel.Gamers)
            {
                if(spieler.Name == name)
                {
                    return spieler;
                }
            }

            return null;
        }

        private async void m_btnBack_Clicked(object sender, EventArgs e)
        {
            if (!m_gespeichert)
            {
                foreach (ClsSpieler spieler in m_viewModel.Gamers)
                {
                    foreach (string name in m_neueSpieler)
                    {
                        if (name == spieler.Name)
                        {
                            await DataProvider.AddSpieler(name, FindSpieler(name).Geschlecht);
                        }
                    }
                }
            }
            m_viewModel.Gamers.Clear();
            App.Current.MainPage = new MainMenu();
        }

        private async void m_btnListen_Clicked(object sender, EventArgs e)
        {
            if (!m_gespeichert)
            {
                foreach (ClsSpieler spieler in m_viewModel.Gamers)
                {
                    foreach (string name in m_neueSpieler)
                    {
                        if (name == spieler.Name)
                        {
                            await DataProvider.AddSpieler(name, FindSpieler(name).Geschlecht);
                        }
                    }
                }
            }
            m_viewModel.Gamers.Clear();
            App.Current.MainPage = new ListView();
        }

        private async void m_btnAddListe_Clicked(object sender, EventArgs e)
        {
            if(m_viewModel.Gamers.Count != 0)
            {
                bool tempbool = true; //Ist dafür da zu schaun ob das Speichern "abgebrochen" wurde (gibt bereits eine Liste mit dem Namen) dann darf der ListPrimaryKey nicht erhöht werden
                if (!(await ListeGibtsSchon()))
                {
                    string temp = await App.Current.MainPage.DisplayPromptAsync("Listenname", "Bitte gib den Listennamen ein!");

                    if (temp != null)
                    {
                        if (await ListennamenGibtsSchon(temp)) { await DisplayAlert("Achtung", "Es gibt bereits eine Liste mit diesem Namen!", "OK"); tempbool = false; }
                        else
                        {
                            ClsSpielerliste spielerliste = new ClsSpielerliste
                            {
                                Name = temp,
                            };

                            await DataProvider.AddSpielerliste(spielerliste.Name);

                            foreach(ClsSpieler spieler in m_viewModel.Gamers)
                            {
                                await DataProvider.AddSpieler(spieler.Name,spieler.Geschlecht);
                            }

                            m_viewModel.Gamers.Clear();
                        }
                    }
                }
                else
                {
                    if (!m_gespeichert)
                    {
                        foreach(ClsSpieler spieler in m_viewModel.Gamers)
                        {
                            foreach(string name in m_neueSpieler)
                            {
                                if(name == spieler.Name)
                                {
                                    await DataProvider.AddSpieler(name, FindSpieler(name).Geschlecht);
                                }
                            }
                        }
                    }
                    m_viewModel.Gamers.Clear();
                }

                if (tempbool)
                {
                    await DataProvider.Getlistprimarykey();
                    DataProvider.m_listprimarykey++;
                }
            }
            else
            {
                await DisplayAlert("Achtung", "Bitte erstelle zuerst einen Spieler bevor du eine Liste erstellst!", "OK");
            }
        }

        private async Task<bool> ListennamenGibtsSchon(string name)
        {
            List<ClsSpielerliste> spls = await DataProvider.GetAllSpielerlisten();

            foreach(ClsSpielerliste spielerliste in spls)
            {
                if(spielerliste.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<bool> ListeGibtsSchon()
        {
            List<ClsSpielerliste> temp = await DataProvider.GetAllSpielerlisten();

            foreach(ClsSpielerliste spl in temp)
            {
                if(spl.Id == DataProvider.m_listprimarykey)
                {
                    return true;
                }
            }

            return false;
        }

        private async Task Refresh()
        {
            SpielerviewModel.Gamers.Clear();
            List<ClsSpieler> sps = await DataProvider.GetSpieler(DataProvider.m_listprimarykey);

            foreach (ClsSpieler sp in sps)
            {
                SpielerviewModel.Gamers.Add(sp);
            }

            ListviewviewModel.SpielerlistenListe.Clear();
            List<ClsSpielerliste> spls = await DataProvider.GetAllSpielerlisten();

            foreach (ClsSpielerliste spl in spls)
            {
                ListviewviewModel.SpielerlistenListe.Add(spl);
            }
        }
    }
}