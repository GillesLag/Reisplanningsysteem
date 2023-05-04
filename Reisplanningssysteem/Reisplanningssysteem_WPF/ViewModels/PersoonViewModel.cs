using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using Reisplanningssysteem_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class PersoonViewModel : BaseViewModel
    {
        private Gebruiker _gebruikerRecord;
        private string _foutmelding;
        private bool _bewerkModus;
        private ObservableCollection<Gemeente> _gemeentes;
        public Gemeente _geselecteerdGemeente { get; set; }
        
        public PersoonViewModel(Gebruiker gebruikerRecord)
        {
            GebruikerRecord = gebruikerRecord;
            Bewerkmodus = true;
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
            Bewerkmodus = false;
            List<Gemeente> gemeentes = DatabaseOperations.OphalenGemeentes();
            Gemeentes = new ObservableCollection<Gemeente>(gemeentes);
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; NotifyPropertyChanged(); }
        }
        public bool Bewerkmodus
        {
            get { return _bewerkModus; }
            set { _bewerkModus = value; }
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

        public void Bevestig()
        {
            if (GebruikerRecord.GemeenteId != null)
            {
                if (GebruikerRecord.MedischeGegevens==null)
                {
                    GebruikerRecord.MedischeGegevens = "";
                }
                if (GebruikerRecord.IsGeldig())
                {
                    if (!Bewerkmodus)
                    {
                        int ok = DatabaseOperations.VoegGebruikerToe(GebruikerRecord);
                        if (ok > 0)
                        {
                            UpdateGebruikers();
                            MessageBox.Show($"{GebruikerRecord.Voornaam} {GebruikerRecord.Achternaam} is toegevoegd");
                            Reset();
                        }
                        else
                        {
                            MessageBox.Show("Gebruiker is niet toegevoegd");
                        }
                    }
                    else
                    {
                        int ok = DatabaseOperations.UpdateGebruiker(GebruikerRecord);
                        if (ok > 0)
                        {
                            MessageBox.Show($"{GebruikerRecord.Voornaam} {GebruikerRecord.Achternaam} is aangepast");
                        }
                        else
                        {
                            Foutmelding = "Gebruiker is niet aangepast!";
                        }
                    }
                }
            }
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Bevestig": Bevestig(); break;
            }
        }

        public void Reset()
        {
            GebruikerRecord = null;
            Foutmelding = "";
        }

        public delegate void GebruikersUpdateEventHandler(object sender, UpdateGenericListEventArgs<Gebruiker> e);
        public event GebruikersUpdateEventHandler GebruikersUpdated;

        private void UpdateGebruikers()
        {
            GebruikersUpdated?.Invoke(this, new UpdateGenericListEventArgs<Gebruiker>(DatabaseOperations.OphalenLijstGebruikers()));
        }
    }
}
