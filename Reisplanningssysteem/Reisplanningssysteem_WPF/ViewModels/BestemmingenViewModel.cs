using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class BestemmingenViewModel : BaseViewModel
    {
        public override string this[string columnName] => throw new NotImplementedException(); 
        
        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": 
                    if (GeselecteerdeBestemming == null)
                    {
                        return false;
                    }

                    return true;
            }

            return true;
        }
        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": Verwijderen(); break;
            }
        }

        private ObservableCollection<Bestemming> _bestemmingen;

        public ObservableCollection<Bestemming> Bestemmingen
        {
            get { return _bestemmingen; }
            set { _bestemmingen = value; }
        }

        private Bestemming _geselecteerdeBestemming;

        public Bestemming GeselecteerdeBestemming
        {
            get { return _geselecteerdeBestemming; }
            set { _geselecteerdeBestemming = value; }
        }


        public BestemmingenViewModel()
        {
            Bestemmingen = new ObservableCollection<Bestemming>(DatabaseOperations.BestemmingenOphalen());
        }

        private void Verwijderen()
        {
            if (GeselecteerdeBestemming == null)
            {
                // TODO messagebox.show() implementeren
                return;
            }

            int ok =  DatabaseOperations.BestemmingVerwijderen(GeselecteerdeBestemming);

            if (ok == 0)
            {
                // messagebox.show() implementeren
                return;
            }

            Wissen();
        }

        private void Wissen()
        {
            GeselecteerdeBestemming = null;
            Bestemmingen = new ObservableCollection<Bestemming>(DatabaseOperations.BestemmingenOphalen());
            return;
        }
    }
}
