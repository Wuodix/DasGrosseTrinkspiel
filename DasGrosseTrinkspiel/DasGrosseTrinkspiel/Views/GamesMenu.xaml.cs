using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DasGrosseTrinkspiel.Classes;
using DasGrosseTrinkspiel.Extentions;
using System.Diagnostics;

namespace DasGrosseTrinkspiel.Views
{
    public enum Spielart
    {
        Kartenspiel,
        Sonstiges,
    }
    public enum Spiel
    {
        WerWürdeEher,
        IchHabNochNie,
        Picolo,
        WWP, //Wahrheit, Wahl, Pflicht
        SiebenMinIH,
        Betrunken,
        Cocktail,
        Ausnüchtern,
        Brettspiel,
        Horoskop,
        WerWeißWas,
        WerKenntWen,
        IstDasNormal,
        Allgemeinwissen,
    }
    public partial class GamesMenu : ContentPage
    {
        //Spielnamen ändern:    in CSV Datei
        //                      in GamesMenu.xaml.cs (Bei Wer würde eher und Wer weiß was und Ausnüchtern 2x)
        //                      in ChoseKategorie bei WWE, WWS und Ausnüchtern
        public GamesMenu()
        {
            InitializeComponent();

            int i = 0;
            foreach (Button button in m_abslButtonLayout.Children)
            {
                Debug.WriteLine(i);
                if(DataHolder.Spiele[i].Name == "Wer Wuerde Eher...?")
                {
                    button.Text = "Wer Würde Eher...?";
                }
                else if(DataHolder.Spiele[i].Name == "Wer weiss was?")
                {
                    button.Text = "Wer weiß was?";
                }
                else if (DataHolder.Spiele[i].Name == "Ausnuechtern")
                {
                    button.Text = "Ausnüchtern?";
                }
                else
                {
                    button.Text = DataHolder.Spiele[i].Name;
                }
                i++;
            }
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
        private void m_btnGame_Clicked(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "Wer Würde Eher...?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.WerWürdeEher, (sender as Button).Text));
                    break;
                case "Ich hab noch nie...":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.IchHabNochNie, (sender as Button).Text));
                    break;
                case "Picolo Klon":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Picolo, (sender as Button).Text));
                    break;
                case "Wahrheit, Wahl oder Pflicht":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.WWP, (sender as Button).Text));
                    break;
                case "7 Minutes in Heaven":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.SiebenMinIH, (sender as Button).Text));
                    break;
                case "Bin ich betrunken?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Betrunken, (sender as Button).Text));
                    break;
                case "Cocktail Mischen":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Cocktail, (sender as Button).Text));
                    break;
                case "Ausnüchtern":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Ausnüchtern, (sender as Button).Text));
                    break;
                case "Brettspiel":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Brettspiel, (sender as Button).Text));
                    break;
                case "Horoskop":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Horoskop, (sender as Button).Text));
                    break;
                case "Wer weiß was?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.WerWeißWas, (sender as Button).Text));
                    break;
                case "Wer kennt wen am Besten?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.WerKenntWen, (sender as Button).Text));
                    break;
                case "Ist das Normal?":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.IstDasNormal, (sender as Button).Text));
                    break;
                case "Allgemeinwissensfragen":
                    Navigation.PushAsync(new ChoseList(Spielart.Kartenspiel, Spiel.Allgemeinwissen, (sender as Button).Text));
                    break;
            }
        }
    }
}