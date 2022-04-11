using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DasGrosseTrinkspiel.ViewModels
{
    public class ChoseListViewModel
    {
        public ObservableCollection<Classes.ClsSpielerliste> SpielerlistenListe { get; set; }

        public ChoseListViewModel()
        {
            SpielerlistenListe = new ObservableCollection<Classes.ClsSpielerliste>();
        }
    }
}
