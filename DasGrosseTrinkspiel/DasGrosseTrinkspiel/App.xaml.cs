using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DasGrosseTrinkspiel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new DasGrosseTrinkspiel.Views.MainMenu();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
