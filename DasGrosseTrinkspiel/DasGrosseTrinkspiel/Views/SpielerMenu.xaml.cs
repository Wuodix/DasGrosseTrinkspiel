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
        ViewModels.SpielerMenuViewModel viewModel;
        public SpielerMenu()
        {
            InitializeComponent();
            
            BindingContext = viewModel = new ViewModels.SpielerMenuViewModel();
        }

        private void OnSwiped(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine("hi");
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    App.Current.MainPage = new MainMenu();
                    break;
            }

            Debug.WriteLine("Fuck uuu");
        }

        private void m_btnAdd_Clicked(object sender, EventArgs e)
        {
            if(m_tbxName.Text != null && m_cmbxGender.SelectedItem != null)
            {
                Classes.Spieler spieler = new Classes.Spieler();
                spieler.Name = m_tbxName.Text;
                spieler.Geschlecht = m_cmbxGender.SelectedItem.ToString();

                viewModel.Gamers.Add(spieler);
                Debug.WriteLine("Fuck uuu");

                m_tbxName.Text = null;
                m_cmbxGender.SelectedItem = null;
            }
        }

        private void m_btnDelete_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Grid grid = button.Parent as Grid;
            Debug.WriteLine("Fuck uuu");
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
                    Debug.WriteLine("Fuck uuu");
                    return spieler;
                }
            }

            return null;
        }
    }
}