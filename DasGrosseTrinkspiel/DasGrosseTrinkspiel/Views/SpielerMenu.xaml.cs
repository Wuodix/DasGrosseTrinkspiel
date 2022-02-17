using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace DasGrosseTrinkspiel.Views
{
    public partial class SpielerMenu : ContentPage
    {
        static ViewModels.ListViewViewModel ListViewModel;
        readonly ViewModels.SpielerMenuViewModel viewModel;
        public SpielerMenu()
        {
            InitializeComponent();
            
            BindingContext = viewModel = new ViewModels.SpielerMenuViewModel();
            ListViewModel = new ViewModels.ListViewViewModel();
        }

        public static ViewModels.ListViewViewModel ListviewviewModel { get { return ListViewModel; } set { } }

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

        private void m_btnAdd_Clicked(object sender, EventArgs e)
        {
            //wenn Datenbank da einführen, dass man nicht 2 gleichnamige Listen erstellen darf
            //Listen sofort in Datenbank speichern
            if(m_tbxName.Text != null && m_cmbxGender.SelectedItem != null)
            {
                Classes.Spieler spieler = new Classes.Spieler
                {
                    Name = m_tbxName.Text,
                    Geschlecht = m_cmbxGender.SelectedItem.ToString()
                };

                viewModel.Gamers.Add(spieler);

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

        private Classes.Spieler FindSpieler(string name)
        {
            foreach(Classes.Spieler spieler in viewModel.Gamers)
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

        private void m_btnAddListe_Clicked(object sender, EventArgs e)
        {
            // Nach Dialogfeld in xamarin googeln und zum Namen bekommen nutzen

            Classes.Spielerliste spielerliste = new Classes.Spielerliste
            {
                SpielerListe = viewModel.Gamers.ToList(),
                Name = "test"
            };

            ListviewviewModel.SpielerlistenListe.Add(spielerliste);
        }
    }
}