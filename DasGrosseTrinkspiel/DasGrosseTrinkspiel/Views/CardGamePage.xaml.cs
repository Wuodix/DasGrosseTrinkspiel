﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DasGrosseTrinkspiel.Extentions;
using Xamarin.CommunityToolkit.Extensions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DasGrosseTrinkspiel.Views
{
    public partial class CardGamePage : ContentPage
    {
        Task m_toast;
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

        private async void NextPage()
        {
            await Navigation.PushModalAsync(DataHolder.Kartenspiel.NextCard());
        }

        private async void PreviousPage()
        {
            if (DataHolder.Kartenspiel.PreviousCard())
            {
                await DisplayAlert("Achtung", "Du kannst nicht weiter zurück!", "OK");
                return;
            }
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            if(m_toast != null)
            {
                if (m_toast.IsCompleted == false)
                {
                    MessagingCenter.Send(this, "ForcePortrait");
                    App.Current.MainPage = new NavigationPage(new MainMenu());
                }
            }
            
            m_toast = this.DisplayToastAsync("Erneut zurück zum Beenden", 1000);

            return true;
        }
        private void OnSwipe(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    NextPage();
                    break;
                case SwipeDirection.Right:
                    PreviousPage();
                    break;
            }
        }
        private void NextPageTap(object sender, EventArgs e) { NextPage(); }
        private void PreviousPageTap(object sender, EventArgs e) { PreviousPage(); }
    }
}