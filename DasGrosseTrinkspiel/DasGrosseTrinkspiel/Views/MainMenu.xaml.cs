using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using DasGrosseTrinkspiel.Extentions;
using System.IO;
using System.Reflection;

namespace DasGrosseTrinkspiel.Views
{
    public partial class MainMenu : ContentPage
    {
        public MainMenu()
        {
            InitializeComponent();

            InitDatabase();
        }

        private async void InitDatabase()
        {
            Debug.WriteLine((await DataProvider.GetFrage(1)).Count);
            if((await DataProvider.GetFrage(1)).Count == 0)
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainMenu)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("DasGrosseTrinkspiel.Sonstiges.Fragen.csv");
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] temp = line.Split(';');

                        await DataProvider.AddFrage(temp[0].ToString(), Convert.ToInt32(temp[1]));
                    }
                }

                Stream stream2 = assembly.GetManifestResourceStream("DasGrosseTrinkspiel.Sonstiges.Kategorien.csv");
                using (StreamReader reader = new StreamReader(stream2))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] teile = line.Split(';');

                        await DataProvider.AddKategorie(teile[0], Convert.ToInt32(teile[1]));
                    }
                }

                Stream stream3 = assembly.GetManifestResourceStream("DasGrosseTrinkspiel.Sonstiges.Spiele.csv");
                using (StreamReader reader = new StreamReader(stream3))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        await DataProvider.AddSpiel(line);
                    }
                }
            }
        }

        private void m_btnSpiele_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GamesMenu());
        }

        private void m_btnRandom_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RandomMenu());
        }

        private void m_btnSpieler_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SpielerMenu());
        }

        private void m_btnFragen_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FragenPage());
        }
    }
}
