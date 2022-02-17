using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace DasGrosseTrinkspiel.ViewModels
{
    public class ListViewViewModel
    {
        public ObservableCollection<Classes.Spielerliste> SpielerlistenListe { get; set; }

        public ListViewViewModel()
        {
            SpielerlistenListe = new ObservableCollection<Classes.Spielerliste>();
        }
    }
}
