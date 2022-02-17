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
            
            BindingContext = viewModel = new ViewModels.SpielerMenuViewModel();
            ListViewModel = new ViewModels.ListViewViewModel();
        }

        public static ViewModels.ListViewViewModel ListviewviewModel { get { return ListViewModel; } set { } }
        public static ViewModels.SpielerMenuViewModel SpielerviewModel { get { return viewModel; } set { } }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine("hi");
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    App.Current.MainPage = new MainMenu();
                    break;
            }
        }

        private async void m_btnAdd_Clicked(object sender, EventArgs e)
        {
            //wenn Datenbank da einführen, dass man nicht 2 gleichnamige Listen erstellen darf
            //Listen sofort in Datenbank speichern
            if(m_tbxName.Text != null && m_cmbxGender.SelectedItem != null)
            {
                Spieler spieler = new Spieler
                {
                    Name = m_tbxName.Text,
                    Geschlecht = m_cmbxGender.SelectedItem.ToString()
                };

                viewModel.Gamers.Add(spieler);
                await ClsDatabase.AddSpieler(m_tbxName.Text, m_cmbxGender.SelectedItem.ToString(), ListViewModel.SpielerlistenListe.Count);

                m_tbxName.Text = null;
                m_cmbxGender.SelectedItem = null;
            }
        }

        private void m_btnDelete_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Grid grid = button.Parent as Grid;
            Label label = grid.Children[0] as Label;
            string text = label.Text;

            viewModel.Gamers.Remove(FindSpieler(text));
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
            // Nach Dialogfeld in xamarin googeln und zum Namen bekommen nutzen

            Spielerliste spielerliste = new Spielerliste
            {
                Name = await App.Current.MainPage.DisplayPromptAsync("Listenname", "Bitte gib den Listennamen ein!")
            };

            viewModel.Gamers.Clear();
            ListviewviewModel.SpielerlistenListe.Add(spielerliste);
        }
    }
}