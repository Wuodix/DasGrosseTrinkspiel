using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DasGrosseTrinkspiel.Views
{
    public partial class ListView : ContentPage
    {
        ViewModels.ListViewViewModel ViewModel;
        public ListView()
        {
            InitializeComponent();

            BindingContext = ViewModel = SpielerMenu.ListviewviewModel;
        }

        private void m_btnBack_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new SpielerMenu();
        }

        private void m_btnChoose_Clicked(object sender, EventArgs e)
        {
            //Wenn database da daraus auslesen
        }

        private void m_btnDelete_Clicked(object sender, EventArgs e)
        {
            //Wenn database da daraus löschen
            ViewModel.SpielerlistenListe.Remove(FindList(m_lbxListen.SelectedItem.ToString()));
        }
        private Classes.Spielerliste FindList(string name)
        {
            foreach (Classes.Spielerliste liste in ViewModel.SpielerlistenListe)
            {
                if (liste.Name == name)
                {
                    return liste;
                }
            }

            return null;
        }
    }
}