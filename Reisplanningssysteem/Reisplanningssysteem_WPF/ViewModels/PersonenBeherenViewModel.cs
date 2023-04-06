using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class PersonenBeherenViewModel : BaseViewModel
    {
        private List<Gebruiker> _gebruikers;
        private Gebruiker _geselecteerdeGebruiker;
        private string _foutmelding;

        public PersonenBeherenViewModel()
        {
            Gebruikers = DatabaseOperations.OphalenLijstGebruikers();
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; NotifyPropertyChanged(); }
        }

        public override string this[string columnName]
        {
            get
            {
                return "";
            }
        }

        public List<Gebruiker> Gebruikers
        {
            get { return _gebruikers; }
            set
            {
                _gebruikers = value;
                NotifyPropertyChanged();
            }
        }
        public Gebruiker GeselecteerdeGebruiker
        {
            get { return _geselecteerdeGebruiker; }
            set
            {
                _geselecteerdeGebruiker = value;
            }
        }

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen":
                case "Bewerken":
                    return GeselecteerdeGebruiker != null;

                case "Toevoegen":
                    return true;

                default:
                    return false;
            }
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Toevoegen": Toevoegen(); break;
                case "Verwijderen": Verwijderen(); break;
                case "OpenPersoonView": OpenPersoonView(); break;
            }
        }

        private void OpenPersoonView()
        {
            var vm = new PersoonViewModel(GeselecteerdeGebruiker);
            var view = new PersoonView();
            view.DataContext = vm;
            view.Show();
        }

        public void Toevoegen()
        {
            var vm = new PersoonViewModel();
            var view = new PersoonView();
            view.DataContext = vm;
            view.Show();
        }

        public void Verwijderen()
        {
            if (GeselecteerdeGebruiker != null)
            {
                int ok = DatabaseOperations.VerwijderGebruiker(GeselecteerdeGebruiker);
                if (ok > 0)
                {
                    Gebruikers = DatabaseOperations.OphalenLijstGebruikers();
                    GeselecteerdeGebruiker = null;
                    Foutmelding = "";
                }
                else
                {
                    Foutmelding = "Gebruiker is niet verwijderd!";
                }
            }
            else
            {
                Foutmelding = "Gelieven een persoon te selecteren!";
            }
        }
    }
}
