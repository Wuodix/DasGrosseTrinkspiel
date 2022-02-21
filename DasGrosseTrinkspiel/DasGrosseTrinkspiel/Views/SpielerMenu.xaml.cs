using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using DasGrosseTrinkspiel.Classes;

namespace DasGrosseTrinkspiel.Views
{
    public partial class SpielerMenu : ContentPage
    {
        static ViewModels.ListViewViewModel ListViewModel;
        static ViewModels.SpielerMenuViewModel viewModel;
        public SpielerMenu()
        {
            InitializeComponent();

            if(viewModel == null)
            {
                BindingContext = viewModel = new ViewModels.SpielerMenuViewModel();
                ListViewModel = new ViewModels.ListViewViewModel();

                Debug.WriteLine("hiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii");
            }
            else
            {
                BindingContext = viewModel;
            }

            Debug.WriteLine("Spieleranzahl Start von Spielermenu: " + viewModel.Gamers.Count);
            foreach (Spieler sp in viewModel.Gamers)
            {
                Debug.WriteLine("1"+sp);
            }
        }

        public static ViewModels.ListViewViewModel ListviewviewModel { get { return ListViewModel; } set { ListViewModel = value; } }
        public static ViewModels.SpielerMenuViewModel SpielerviewModel { get { return viewModel; } set { viewModel = value; } }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    App.Current.MainPage = new MainMenu();
                    break;
            }
        }

        private async void m_btnAdd_Clicked(object sender, EventArgs e)
        {
            if(m_tbxName.Text != null && m_cmbxGender.SelectedItem != null)
            {
                Spieler spieler = new Spieler
                {
                    Name = m_tbxName.Text,
                    Geschlecht = m_cmbxGender.SelectedItem.ToString(),
                };

                spieler.Id = await ClsDatabase.AddSpieler(m_tbxName.Text, m_cmbxGender.SelectedItem.ToString());

                viewModel.Gamers.Add(spieler);

                m_tbxName.Text = null;
                m_cmbxGender.SelectedItem = null;
            }
        }

        private async void m_btnDelete_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Grid grid = button.Parent as Grid;
            Label label = grid.Children[0] as Label;
            string text = label.Text;

            await ClsDatabase.DeleteSpieler(FindSpieler(text).Id);

            await Refresh();
        }

        private Spieler FindSpieler(string name)
        {
            foreach(Spieler spieler in viewModel.Gamers)
            {
                if(spieler.Name == name)
                {
                    return spieler;
                }
            }

            return null;
        }

        private void m_btnBack_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainMenu();
        }

        private void m_btnListen_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new ListView();
        }

        private async void m_btnAddListe_Clicked(object sender, EventArgs e)
        {
            if(viewModel.Gamers.Count != 0)
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
                            Spielerliste spielerliste = new Spielerliste
                            {
                                Name = temp
                            };

                            await ClsDatabase.AddSpielerliste(spielerliste.Name);

                            viewModel.Gamers.Clear();
                        }
                    }
                }
                else
                {
                    viewModel.Gamers.Clear();
                }

                if (tempbool)
                {
                    ClsDatabase.Listprimarykey = (await ClsDatabase.GetAllSpielerlisten()).LastOrDefault().Id;
                    ClsDatabase.Listprimarykey++;
                }
            }
            else
            {
                await DisplayAlert("Achtung", "Bitte erstelle zuerst einen Spieler bevor du eine Liste erstellst!", "OK");
            }
        }

        private async Task<bool> ListennamenGibtsSchon(string name)
        {
            List<Spielerliste> spls = await ClsDatabase.GetAllSpielerlisten();

            foreach(Spielerliste spielerliste in spls)
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
            List<Spielerliste> temp = await ClsDatabase.GetAllSpielerlisten();

            foreach(Spielerliste spl in temp)
            {
                if(spl.Id == ClsDatabase.Listprimarykey)
                {
                    return true;
                }
            }

            return false;
        }

        private async Task Refresh()
        {
            SpielerviewModel.Gamers.Clear();
            List<Spieler> sps = await ClsDatabase.GetSpieler(ClsDatabase.Listprimarykey);

            foreach (Spieler sp in sps)
            {
                SpielerviewModel.Gamers.Add(sp);
            }

            ListviewviewModel.SpielerlistenListe.Clear();
            List<Spielerliste> spls = await ClsDatabase.GetAllSpielerlisten();

            foreach (Spielerliste spl in spls)
            {
                ListviewviewModel.SpielerlistenListe.Add(spl);
            }
        }
    }
}