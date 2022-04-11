using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DasGrosseTrinkspiel.ViewModels
{
    internal class KategorieViewModel
    {
        public ObservableCollection<Classes.ClsKategorie> KategorienListe { get; set; }

        public KategorieViewModel()
        {
            KategorienListe = new ObservableCollection<Classes.ClsKategorie>();
        }
    }
}
