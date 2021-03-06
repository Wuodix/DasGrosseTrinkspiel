using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DasGrosseTrinkspiel.ViewModels
{
    public class SpielerMenuViewModel
    {
        public ObservableCollection<Classes.ClsSpieler> Gamers { get; set; }
        public ObservableCollection<string> Genders { get;}

        public SpielerMenuViewModel()
        {
            Gamers = new ObservableCollection<Classes.ClsSpieler>();
            Genders = new ObservableCollection<string>
            {
                "Männlich",
                "Weiblich",
                "Divers"
            };
        }
    }
}
