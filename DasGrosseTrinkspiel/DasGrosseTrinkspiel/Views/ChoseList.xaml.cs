using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using DasGrosseTrinkspiel.Classes;
using DasGrosseTrinkspiel.Extentions;
using DasGrosseTrinkspiel.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
        Mischen,
    }
    public partial class ChoseList : ContentPage
    {
        static ChoseListViewModel m_viewModel;
        Spielart m_spielart;
        string m_spiel;
        public ChoseList(Spielart spielart, string Spielname)
        {
            InitializeComponent();

            BindingContext = m_viewModel = new ChoseListViewModel();

            m_spielart = spielart;
            m_spiel = Spielname;

            m_loadingView.IsVisible = true;
            m_viewModel.SpielerlistenListe.Clear();

            InitSpielerLists();
        }
        private async void InitSpielerLists()
        {
            var temp = await DataProvider.GetAllSpielerlisten();

            foreach (ClsSpielerliste spl in temp)
            {
                m_viewModel.SpielerlistenListe.Add(spl);
            }
            m_loadingView.IsVisible = false;
        }
        private void m_btnChoose_Clicked(object sender, EventArgs e)
        {
            if (m_lbxListen.SelectedItem != null)
            {
                Navigation.PushAsync(new ChoseKategorie(m_spielart, m_lbxListen.SelectedItem as ClsSpielerliste, m_spiel));
            }
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
    }
}