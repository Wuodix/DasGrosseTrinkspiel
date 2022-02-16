using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DasGrosseTrinkspiel.ViewModels
{
    public class SpielerMenuViewModel
    {
        public ObservableCollection<Classes.Spieler> Gamers { get; }
        public ObservableCollection<string> Genders { get;}

        public SpielerMenuViewModel()
        {
            Gamers = new ObservableCollection<Classes.Spieler>();
            Genders = new ObservableCollection<string>
            {
                "Männlich",
                "Weiblich",
                "Divers"
            };
        }
    }
}
