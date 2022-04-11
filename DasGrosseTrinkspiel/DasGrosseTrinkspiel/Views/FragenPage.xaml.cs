using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DasGrosseTrinkspiel.Extentions;
using DasGrosseTrinkspiel.Classes;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace DasGrosseTrinkspiel.Views
{
    public partial class FragenPage : ContentPage
    {
        public FragenPage()
        {
            InitializeComponent();
        }

        private async void m_btnAllesAusgeben_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Kategorie Id", "", initialValue: "1", maxLength: 2, keyboard: Keyboard.Numeric);

            List<ClsFrage> fragenliste = await DataProvider.GetFrage(Convert.ToInt32(result));

            Debug.WriteLine("IN DATABASE: ");
            foreach(ClsFrage frag in fragenliste)
            {
                Debug.WriteLine(frag);
            }

            Debug.WriteLine("IN INTERNER LISTE: ");
            foreach(ClsFrage frag in DataHolder.Fragen)
            {
                Debug.WriteLine(frag);
            }
        }

        private async void m_btnAllesLöschen_Clicked(object sender, EventArgs e)
        {
            await DataProvider.DeleteAlleFragen();
            DataHolder.Fragen.Clear();
        }

        private async void m_btnEinlesenausDB_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Kategorie Id", "", initialValue: "1", maxLength: 2, keyboard: Keyboard.Numeric);

            DataHolder.Fragen = await DataProvider.GetFrage(Convert.ToInt32(result));
        }

        private async void m_btnEinlesenausCSV_Clicked(object sender, EventArgs e)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(FragenPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("DasGrosseTrinkspiel.Sonstiges.Test.csv");
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] temp = line.Split(';');

                    await DataProvider.AddFrage(temp[0].ToString(), Convert.ToInt32(temp[1]));
                }
            }
        }
    }
}