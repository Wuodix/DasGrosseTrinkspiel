using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DasGrosseTrinkspiel.Extentions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DasGrosseTrinkspiel.Views
{
    public partial class CardGamePage : ContentPage
    {
        public CardGamePage(string Frage)
        {
            InitializeComponent();

            Fragenlabel.Text = Frage;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "ForceLandscape");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //MessagingCenter.Send(this, "ForcePortrait");
        }

        private async void NextPageTap(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(DataHolder.Kartenspiel.NextCard());
        }

        private async void PreviousPageTap(object sender, EventArgs e)
        {
            if (DataHolder.Kartenspiel.PreviousCard())
            {
                await DisplayAlert("Achtung", "Du kannst nicht weiter zurück!", "OK");
                return;
            }
            await Navigation.PopModalAsync();
        }
    }
}