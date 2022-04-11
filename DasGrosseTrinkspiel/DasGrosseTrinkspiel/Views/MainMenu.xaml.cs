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

            /*
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainMenu)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("DasGrosseTrinkspiel.Sonstiges.Test.csv");
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] temp = line.Split(';');

                    DataProvider.AddFrage(temp[0].ToString(), Convert.ToInt32(temp[1]));
                }
            }

            Stream stream2 = assembly.GetManifestResourceStream("DasGrosseTrinkspiel.Sonstiges.Kategorien.csv");
            using (StreamReader reader = new StreamReader(stream2))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    DataProvider.AddKategorie(line);
                }
            }
            */
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
