using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_DAL;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class ReisBeherenViewModel : BaseViewModel
    {
        public override string this[string columnName]
        {
            get { return ""; }
        } 
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Toevoegen": Toevoegen(); break;
                case "Bewerken": Bewerken(); break;
            }
        }

        private string _foutmelding;

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        private string _bewerkengOfToevoegen;

        public string BewerkenOfToevoegen
        {
            get { return _bewerkengOfToevoegen; }
            set { _bewerkengOfToevoegen = value; }
        }

        private string _bewerkenOfToevoegenButton;

        public string BewerkenOfToevoegenButton
        {
            get { return _bewerkenOfToevoegenButton; }
            set { _bewerkenOfToevoegenButton = value; }
        }

        private Reis _reis;

        public Reis Reis
        {
            get { return _reis; }
            set { _reis = value; }
        }

        private List<Thema> _themas;

        public List<Thema> Themas
        {
            get { return _themas; }
            set { _themas = value; }
        }

        private Thema _geselecteerdeThema;

        public Thema GeselecteerdeThema
        {
            get { return _geselecteerdeThema; }
            set { _geselecteerdeThema = value; }
        }

        private List<Bestemming> _bestemmingen;

        public List<Bestemming> Bestemmingen
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

        private List<Gebruiker> _hoofdmonitoren;

        public List<Gebruiker> Hoofdmonitoren
        {
            get { return _hoofdmonitoren; }
            set { _hoofdmonitoren = value; }
        }

        private Gebruiker _geselecteerdeHoofmonitor;

        public Gebruiker GeselecteerdeHoofmonitor
        {
            get { return _geselecteerdeHoofmonitor; }
            set { _geselecteerdeHoofmonitor = value; }
        }


        private List<LeeftijdsCategorie> _leeftijdsCategorieën;

        public List<LeeftijdsCategorie> LeeftijdsCategorieën
        {
            get { return _leeftijdsCategorieën; }
            set { _leeftijdsCategorieën = value; }
        }

        private LeeftijdsCategorie _geselecteerdeLeeftijdsCategorie;

        public LeeftijdsCategorie GeselecteerdeLeeftijdsCategorie
        {
            get { return _geselecteerdeLeeftijdsCategorie; }
            set { _geselecteerdeLeeftijdsCategorie = value; }
        }

        public ReisBeherenViewModel()
        {
            Reis = new Reis();
            BewerkenOfToevoegen = "Reis toevoegen";
            BewerkenOfToevoegenButton = "Toevoegen";
            GeselecteerdeBestemming = null;
            GeselecteerdeHoofmonitor = null;
            GeselecteerdeLeeftijdsCategorie = null;
            GeselecteerdeThema = null;
            Reis.BeginDatum = DateTime.Now;
            Reis.EindDatum = Reis.BeginDatum;

            ViewOpvullen();
        }

        public ReisBeherenViewModel(Reis reis)
        {
            Reis = reis;
            BewerkenOfToevoegen = "Reis bewerken";
            BewerkenOfToevoegenButton = "Bewerken";

            ViewOpvullen();
        }
        private void ViewOpvullen()
        {
            Themas = DatabaseOperations.ThemasOphalen();
            Hoofdmonitoren = DatabaseOperations.HoogdmonitorenOphalen();
            Bestemmingen = DatabaseOperations.BestemmingenOphalen();
            LeeftijdsCategorieën = DatabaseOperations.LeeftijdsCategorieënOphalen();
        }

        private void Bewerken()
        {
            if (Reis == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Reis.IsGeldig())
            {
                Foutmelding = Reis.Error;
                return;
            }

            int ok = DatabaseOperations.ReisBewerken(Reis);

            if (ok == 0)
            {
                Foutmelding = "Het bewerken van de bestemming is niet gelukt!";
                return;
            }

            Foutmelding = "";
            UpdateReizen();
        }

        private void Toevoegen()
        {
            if (Reis == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Reis.IsGeldig())
            {
                Foutmelding = Reis.Error;
                return;
            }

            int ok = DatabaseOperations.ReisToevoegen(Reis);

            if (ok == 0)
            {
                Foutmelding = "Het toevoegen van de bestemming is niet gelukt!";
                return;
            }

            Reis = new Reis();
            GeselecteerdeBestemming = null;
            GeselecteerdeHoofmonitor = null;
            GeselecteerdeLeeftijdsCategorie = null;
            GeselecteerdeThema = null;
            Foutmelding = "";
            UpdateReizen();
        }

        public delegate void ReizenUpdateEventHandler(object sender, UpdateGenericListEventArgs<Reis> e);
        public event ReizenUpdateEventHandler ReizenUpdatedEvent;

        private void UpdateReizen()
        {
            ReizenUpdatedEvent?.Invoke(this, new UpdateGenericListEventArgs<Reis>(DatabaseOperations.ReizenOphalen()));
        }
    }
}
