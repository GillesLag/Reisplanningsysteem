using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class PersoonViewModel : BaseViewModel
    {
        private Gebruiker _gebruikerRecord;
        private string _foutmelding;
        private ObservableCollection<Gemeente> _gemeentes;
        public Gemeente _geselecteerdGemeente { get; set; }
        
        public PersoonViewModel(Gebruiker gebruikerRecord)
        {
            GebruikerRecord = gebruikerRecord;
            List<Gemeente> gemeentes = DatabaseOperations.OphalenGemeentes();
            Gemeentes = new ObservableCollection<Gemeente>(gemeentes);
        }

        public Gemeente GeselecteerdGemeente
        {
            get { return _geselecteerdGemeente; }
            set
            {
                _geselecteerdGemeente = value;
            }
        }

        public PersoonViewModel()
        {
            GebruikerRecord = new Gebruiker();
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
            if (GeselecteerdGemeente != null)
            {
                GebruikerRecord.GemeenteId = GeselecteerdGemeente.Id;
                GebruikerRecord.BasisCursus = false;
                GebruikerRecord.HoofmonitorCursus = false;
                GebruikerRecord.MedischeGegevens = "";


                if (GebruikerRecord.IsGeldig())
                {
                    int ok = DatabaseOperations.VoegGebruikerToe(GebruikerRecord);
                    if (ok > 0)
                    {
                        MessageBox.Show($"{GebruikerRecord.Voornaam} {GebruikerRecord.Achternaam} is toegevoegd");
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Gebruiker is niet toegevoegd");
                    }
                }
            }
        }

        public void Aanpassen()
        {

        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Bevestig": Toevoegen(); break;
            }
        }

        public void Reset()
        {
            GebruikerRecord = null;
            Foutmelding = "";
        }

    }
}
