﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using DasGrosseTrinkspiel.Classes;
using DasGrosseTrinkspiel.Extentions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace DasGrosseTrinkspiel.Views
{
    public partial class ChoseKategorie : ContentPage
    {
        static ViewModels.KategorieViewModel m_viewModel;
        Spielart m_spielart;
        Spiel m_spiel;
        ClsSpielerliste m_spielerliste;
        public ChoseKategorie(Spielart spielart, ClsSpielerliste spielerliste, Spiel Spiel)
        {
            InitializeComponent();

            BindingContext = m_viewModel = new ViewModels.KategorieViewModel();
            m_spielart = spielart;
            m_spielerliste = spielerliste;
            m_spiel = Spiel;

            m_loadingView.IsVisible = true;

            InitKategorien();
        }
        private async void InitKategorien()
        {
            var temp = await DataProvider.GetAllSpiele();
            ClsSpiel spiel = temp.Find(x => x.Name == m_spiel.ToString());
            var temp1 = await DataProvider.GetKategorie(spiel.ID);

            foreach (ClsKategorie spl in temp1)
            {
                m_viewModel.KategorienListe.Add(spl);
            }
            m_loadingView.IsVisible = false;
        }
        private void m_btnStart_Clicked(object sender, EventArgs e)
        {
            m_loadingView.IsVisible = true;
            if (m_lbxListen.SelectedItems[0] != null)
            {
                List<ClsKategorie> list = new List<ClsKategorie>();
                foreach(ClsKategorie spl in m_lbxListen.SelectedItems)
                {
                    list.Add(spl);
                }
                switch (m_spielart)
                {
                    case Spielart.Kartenspiel:
                        if(m_spiel == Spiel.Picolo)
                        {
                            DataHolder.Kartenspiel = new ClsPicolo(list, m_spielerliste);
                        }
                        DataHolder.Kartenspiel = new ClsKartenspiel() { Spieltyp = m_spiel};
                        DataHolder.Kartenspiel.Start(list, m_spielerliste);
                        break;
                    case Spielart.Sonstiges:
                        //Andere Spiele Starten bzw. für andere Spielarten noch enum und Dataholder ding erstellen
                        break;
                }
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