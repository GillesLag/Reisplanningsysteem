using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class PersoonViewModel : BaseViewModel
    {
        private Gebruiker _gebruikerRecord;
        private string _foutmelding;

        private ObservableCollection<Gemeente> _gemeentes;

        public PersoonViewModel(Gebruiker gebruikerRecord)
        {
            GebruikerRecord = gebruikerRecord;

            List<Gemeente> gemeentes = DatabaseOperations.OphalenGemeentes();
            Gemeentes = new ObservableCollection<Gemeente>(gemeentes);
        }

        public PersoonViewModel()
        {
            GebruikerRecord = null;

            List<Gemeente> gemeentes = DatabaseOperations.OphalenGemeentes();
            Gemeentes = new ObservableCollection<Gemeente>(gemeentes);
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

        public Gebruiker GebruikerRecord
        {
            get { return _gebruikerRecord; }
            set
            {
                _gebruikerRecord = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Gemeente> Gemeentes
        {
            get { return _gemeentes; }
            set
            {
                _gemeentes = value;
                NotifyPropertyChanged();
            }
        }

        public void Toevoegen()
        {

        }


        public void Aanpassen()
        {

        }

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Bewerken":
                    return GebruikerRecord != null;

                case "Toevoegen":
                    return GebruikerRecord == null;

                default:
                    return true;
            }
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Toevoegen": Toevoegen(); break;
                case "Aanpassen": Aanpassen(); break;
            }
        }
    }
}
