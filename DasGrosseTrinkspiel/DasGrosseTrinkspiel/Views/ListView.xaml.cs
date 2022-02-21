using DasGrosseTrinkspiel.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DasGrosseTrinkspiel.Views
{
    public partial class ListView : ContentPage
    {
        static ViewModels.ListViewViewModel ViewModel;
        public ListView()
        {
            InitializeComponent();

            BindingContext = ViewModel = SpielerMenu.ListviewviewModel;

            InitSpielerLists();
        }

        private void OnSwipe(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    App.Current.MainPage = new SpielerMenu();
                    break;
            }
        }

        private static async Task<bool> InitSpielerLists()
        {
            var temp = await ClsDatabase.GetAllSpielerlisten();
            ViewModel.SpielerlistenListe.Clear();

            foreach (Spielerliste spl in temp)
            {
                ViewModel.SpielerlistenListe.Add(spl);
                Debug.WriteLine(spl.ToString());
            }

            return true;
        }

        private void m_btnBack_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new SpielerMenu();
        }

        private async void m_btnChoose_Clicked(object sender, EventArgs e)
        {
            // nach dem Auswählen wird ClsDatabase.Listprimarykey nicht wieder auf die Id der letzten Liste gestellt
            if(m_lbxListen.SelectedItem != null)
            {
                var getspieler = await ClsDatabase.GetSpieler((m_lbxListen.SelectedItem as Spielerliste).Id);
                ClsDatabase.Listprimarykey = (m_lbxListen.SelectedItem as Spielerliste).Id;
                Debug.WriteLine("primary Key aus btn Choose: " + ClsDatabase.Listprimarykey);

                SpielerMenu.SpielerviewModel.Gamers.Clear();

                Debug.WriteLine("Spieleranzahl btn Choose: " + getspieler.Count);
                foreach (Spieler spieler in getspieler)
                {
                    SpielerMenu.SpielerviewModel.Gamers.Add(spieler);
                    Debug.WriteLine("2"+spieler.ToString());
                }

                App.Current.MainPage = new SpielerMenu();
            }
        }

        private async void m_btnDelete_Clicked(object sender, EventArgs e)
        {
            if(m_lbxListen.SelectedItem != null)
            {
                await ClsDatabase.DeleteSpielerliste(FindList(m_lbxListen.SelectedItem.ToString()).Id);

                await Refresh();
            }
        }

        private async void m_btnDeleteAll_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Verifizierung", "Willst du wirklich alle Einträge aus der Datenbank löschen?", "Ja", "Nein");
            if (answer)
            {
                await ClsDatabase.DeleteEverything();

                await Refresh();
            }
        }
        private Spielerliste FindList(string name)
        {
            foreach (Spielerliste liste in ViewModel.SpielerlistenListe)
            {
                if (liste.Name == name)
                {
                    return liste;
                }
            }

            return null;
        }

        private async Task Refresh()
        {
            SpielerMenu.SpielerviewModel.Gamers.Clear();
            List<Spieler> sps = await ClsDatabase.GetSpieler(ClsDatabase.Listprimarykey);

            foreach(Spieler sp in sps)
            {
                SpielerMenu.SpielerviewModel.Gamers.Add(sp);
            }

            SpielerMenu.ListviewviewModel.SpielerlistenListe.Clear();
            List<Spielerliste> spls = await ClsDatabase.GetAllSpielerlisten();

            foreach(Spielerliste spl in spls)
            {
                SpielerMenu.ListviewviewModel.SpielerlistenListe.Add(spl);
            }
        }
    }
}