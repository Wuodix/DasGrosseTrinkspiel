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
            switch ((sender as Button).Text)
            {
                case "Wer Würde Eher...?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.WerWürdeEher));
                    break;
                case "Ich hab noch nie...":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.IchHabNochNie));
                    break;
                case "Picolo Klon":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Picolo));
                    break;
                case "Wahrheit, Wahl, oder Pflicht":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.WWP));
                    break;
                case "7 Minutes in Heaven":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.SiebenMinIH));
                    break;
                case "Bin ich betrunken?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Betrunken));
                    break;
                case "Cocktail Mischen":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Cocktail));
                    break;
                case "Ausnüchtern":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Ausnüchtern));
                    break;
                case "Würfel Brettspiel":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Brettspiel));
                    break;
                case "Horoskop":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Horoskop));
                    break;
                case "Wer weiß was?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.WerWeißWas));
                    break;
                case "Wer kennt wen am Besten?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.WerKenntWen));
                    break;
                case "Ist das Normal?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.IstDasNormal));
                    break;
                case "Allgemeinwissensfragen":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Allgemeinwissen));
                    break;
                case "Mischen":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Mischen));
                    break;

            }
        }
    }
}