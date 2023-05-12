using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using Reisplanningssysteem_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class PersonenBeherenViewModel : BaseViewModel
    {
        private ObservableCollection<Gebruiker> _gebruikers;
        private Gebruiker _geselecteerdeGebruiker;
        private string _foutmelding;

        public PersonenBeherenViewModel()
        {
            AlleGebruikers = DatabaseOperations.OphalenLijstGebruikers();
            Gebruikers = new ObservableCollection<Gebruiker>(AlleGebruikers);
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public override string this[string columnName] { get { return ""; } }

        public List<Gebruiker> AlleGebruikers { get; set; }

        public ObservableCollection<Gebruiker> Gebruikers
        {
            get { return _gebruikers; }
            set { _gebruikers = value; }
        }
        public Gebruiker GeselecteerdeGebruiker
        {
            get { return _geselecteerdeGebruiker; }
            set{ _geselecteerdeGebruiker = value; }
        }

        private string _filter;

        public string Filter
        {
            get { return _filter; }
            set 
            { 
                _filter = value;
                GebruikersFilteren();
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
                case "Bewerken": Bewerken(); break;
            }
        }

        private void Bewerken()
        {
            var vm = new PersoonViewModel(GeselecteerdeGebruiker);
            var view = new PersoonView();
            view.DataContext = vm;
            view.Show();
        }

        private void Toevoegen()
        {
            var vm = new PersoonViewModel();
            var view = new PersoonView();
            view.DataContext = vm;
            vm.GebruikersUpdated += GebruikersUpdated;
            view.Show();
        }

        private void Verwijderen()
        {
            if (GeselecteerdeGebruiker != null)
            {
                int ok = DatabaseOperations.VerwijderGebruiker(GeselecteerdeGebruiker);
                if (ok > 0)
                {
                    AlleGebruikers = DatabaseOperations.OphalenLijstGebruikers();
                    Gebruikers = new ObservableCollection<Gebruiker>(AlleGebruikers);
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

        private void GebruikersFilteren()
        {
            Gebruikers = new ObservableCollection<Gebruiker>(AlleGebruikers.Where(g => g.ToString().ToLower().Contains(Filter.ToLower())).ToList());
        }

        private void GebruikersUpdated(object sender, UpdateGenericListEventArgs<Gebruiker> e)
        {
            AlleGebruikers = e.GenericList;
            Gebruikers = new ObservableCollection<Gebruiker>(e.GenericList);
        }
    }
}