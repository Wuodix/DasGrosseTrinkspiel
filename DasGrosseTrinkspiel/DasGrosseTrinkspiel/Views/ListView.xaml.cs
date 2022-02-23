﻿using DasGrosseTrinkspiel.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DasGrosseTrinkspiel.Views
{
    public partial class ListView : ContentPage
    {
        static ViewModels.ListViewViewModel m_viewModel;
        
        public ListView()
        {
            InitializeComponent();

            BindingContext = m_viewModel = SpielerMenu.ListviewviewModel;

            m_loadingView.IsVisible = true;
            m_viewModel.SpielerlistenListe.Clear();

            InitSpielerLists();
        }

        private void OnSwipe(object sender, SwipedEventArgs e)
        {
            switch (e.Direction)
            {
                case SwipeDirection.Right:
                    SpielerMenu.SpielerviewModel.Gamers.Clear();
                    App.Current.MainPage = new SpielerMenu();
                    break;
            }
        }

        private async void InitSpielerLists()
        {
            var temp = await ClsDatabase.GetAllSpielerlisten();

            foreach (Spielerliste spl in temp)
            {
                m_viewModel.SpielerlistenListe.Add(spl);
            }
            m_loadingView.IsVisible = false;
        }

        private void m_btnBack_Clicked(object sender, EventArgs e)
        {
            SpielerMenu.SpielerviewModel.Gamers.Clear();
            App.Current.MainPage = new SpielerMenu();
        }

        private async void m_btnChoose_Clicked(object sender, EventArgs e)
        {
            if(m_lbxListen.SelectedItem != null)
            {
                m_loadingView.IsVisible = true;
                var getspieler = await ClsDatabase.GetSpieler((m_lbxListen.SelectedItem as Spielerliste).Id);
                ClsDatabase.m_listprimarykey = (m_lbxListen.SelectedItem as Spielerliste).Id;
                Debug.WriteLine("primary Key aus btn Choose: " + ClsDatabase.m_listprimarykey);

                SpielerMenu.SpielerviewModel.Gamers.Clear();

                Debug.WriteLine("Spieleranzahl btn Choose: " + getspieler.Count);
                foreach (Spieler spieler in getspieler)
                {
                    SpielerMenu.SpielerviewModel.Gamers.Add(spieler);
                    Debug.WriteLine("2"+spieler.ToString());
                }

                m_loadingView.IsVisible = false;
                App.Current.MainPage = new SpielerMenu();
            }
        }

        private async void m_btnDelete_Clicked(object sender, EventArgs e)
        {
            if(m_lbxListen.SelectedItem != null)
            {
                m_loadingView.IsVisible = true;

                await ClsDatabase.DeleteSpielervonListe(FindList(m_lbxListen.SelectedItem.ToString()).Id);
                await ClsDatabase.DeleteSpielerliste(FindList(m_lbxListen.SelectedItem.ToString()).Id);

                await Refresh();

                m_loadingView.IsVisible = false;
            }
        }

        private async void m_btnDeleteAll_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Verifizierung", "Willst du wirklich alle Einträge aus der Datenbank löschen?", "Ja", "Nein");
            if (answer)
            {
                m_loadingView.IsVisible = true;

                await ClsDatabase.DeleteEverything();

                await Refresh();

                m_loadingView.IsVisible = false;
            }
        }

        private Spielerliste FindList(string name)
        {
            foreach (Spielerliste liste in m_viewModel.SpielerlistenListe)
            {
                if (liste.Name == name)
                {
                    return liste;
                }
            }

            return null;
        }

        private async Task Refresh()
        {
            SpielerMenu.SpielerviewModel.Gamers.Clear();
            List<Spieler> sps = await ClsDatabase.GetSpieler(ClsDatabase.m_listprimarykey);

            foreach(Spieler sp in sps)
            {
                SpielerMenu.SpielerviewModel.Gamers.Add(sp);
            }

            SpielerMenu.ListviewviewModel.SpielerlistenListe.Clear();
            List<Spielerliste> spls = await ClsDatabase.GetAllSpielerlisten();

            foreach(Spielerliste spl in spls)
            {
                SpielerMenu.ListviewviewModel.SpielerlistenListe.Add(spl);
            }
        }
    }
}