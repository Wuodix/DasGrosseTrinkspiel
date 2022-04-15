using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DasGrosseTrinkspiel.Classes;
using System.Diagnostics;

namespace DasGrosseTrinkspiel.Views
{
    public partial class GamesMenu : ContentPage
    {
        public GamesMenu()
        {
            InitializeComponent();
        }
        private void m_btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void OnSwipe(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    Navigation.PopAsync();
                    break;
            }
        }
        private void m_btnKartenspiel_Clicked(object sender, EventArgs e)
        {
            if((sender as Button).Text == "Wer Würde Eher...?")
            {
                Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, "Wer Wuerde Eher...?"));
            }
            else
            {
                Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, (sender as Button).Text));
            }

        }
    }
}