using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_DAL;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Threading;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class ReisBeherenViewModel : BaseViewModel
    {
        public Gebruiker _geselecteerdGebruiker { get; set; }

        public Gebruiker GeselecteerdeGebruiker
        {
            get { return _geselecteerdGebruiker; }
            set
            {
                _geselecteerdGebruiker = value;
            }
        }
        public Gebruiker _teVerwijderenGebruiker { get; set; }

        public Gebruiker TeVerwijderenGebruiker
        {
            get { return _teVerwijderenGebruiker; }
            set
            {
                _teVerwijderenGebruiker = value;
            }
        }

        private ObservableCollection<Gebruiker> _gebruikers;
        public ObservableCollection<Gebruiker> Gebruikers
        {
            get { return _gebruikers; }
            set { _gebruikers = value; }
        }

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
                case "Linken": Linken(); break;
                case "VerwijderGebruiker": Verwijderen(); break;
                case "MaakHoofdmonitor": hoofMonitorLinken(); break;
            }
        }

        private string _linkButton;

        public string LinkButton
        {
            get { return _linkButton; }
            set { _linkButton = value; }
        }


        private string _verwijderGebruiker;

        public string VerwijderGebruiker
        {
            get { return _verwijderGebruiker; }
            set { _verwijderGebruiker = value; }
        }

        public ObservableCollection<Gebruiker> AlleGebruikers { get; set; }

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

        private string _maakHoofdmonitor;

        public string MaakHoofdmonitor
        {
            get { return _maakHoofdmonitor; }
            set { _maakHoofdmonitor = value; }
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

        private string _monitor;

        public string Monitor
        {
            get { return _monitor; }
            set { _monitor = value; }
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
            AlleGebruikers = new ObservableCollection<Gebruiker>(DatabaseOperations.OphalenLijstGebruikers());
            Gebruikers = new ObservableCollection<Gebruiker>();
            Reis = new Reis();
            BewerkenOfToevoegen = "Reis toevoegen";
            BewerkenOfToevoegenButton = "Toevoegen";
            GeselecteerdeBestemming = null;
            GeselecteerdeHoofmonitor = null;
            GeselecteerdeLeeftijdsCategorie = null;
            GeselecteerdeThema = null;
            Reis.BeginDatum = DateTime.Now;
            Reis.EindDatum = Reis.BeginDatum;
            LinkButton = "Linken";
            VerwijderGebruiker = "VerwijderGebruiker";
            MaakHoofdmonitor = "MaakHoofdmonitor";

            ViewOpvullen();
        }

        public ReisBeherenViewModel(Reis reis)
        {
            AlleGebruikers = new ObservableCollection<Gebruiker>(DatabaseOperations.OphalenLijstGebruikers()?.Where(gebruiker => gebruiker.Boekingen?.Any(gr => gr.ReisId == reis.Id) == false)?.ToList());
            Gebruikers = new ObservableCollection<Gebruiker>(DatabaseOperations.OphalenLijstGebruikers()?.Where(gebruiker => gebruiker.Boekingen?.Any(gr => gr.ReisId == reis.Id) == true)?.ToList());

            Reis = reis;
            BewerkenOfToevoegen = "Reis bewerken";
            BewerkenOfToevoegenButton = "Bewerken";
            LinkButton = "Linken";
            VerwijderGebruiker = "VerwijderGebruiker";
            MaakHoofdmonitor = "MaakHoofdmonitor";
            Gebruiker geselecteerdeMonitor = reis.Boekingen.Where(b=>b.IsMonitor).Select(b=>b.Gebruiker).FirstOrDefault();

            toonMonitor(geselecteerdeMonitor);

            ViewOpvullen();
        }

        private void toonMonitor(Gebruiker monitor)
        {
            if (monitor != null)
            {
                Monitor = $"Monitor: {monitor.Voornaam} {monitor.Achternaam}";
            }
            else
            {
                Monitor = "";
            }
        }


        private void Verwijderen()
        {
            if (TeVerwijderenGebruiker == null)
            {
                Foutmelding = "Gelieven een te verwijderen gebruiker te selecteren";
            }
            else
            {

                AlleGebruikers.Add(TeVerwijderenGebruiker);
                Boeking boeking = DatabaseOperations.ZoekBoeking(TeVerwijderenGebruiker, Reis);
                if (boeking.IsMonitor)
                {
                    toonMonitor(null);
                }
                DatabaseOperations.BoekingVerwijderen(boeking);
                Gebruikers.Remove(TeVerwijderenGebruiker);
                TeVerwijderenGebruiker = null;
            }

        }


        private void hoofMonitorLinken()
        {
            if (TeVerwijderenGebruiker == null)
            {
                Foutmelding = "Gelieven een gebruiker te selecteren";
            }
            else
            {

                if (!TeVerwijderenGebruiker.HoofmonitorCursus)
                {
                    Foutmelding = "Deze gebruiker heeft geen hoofdmonitor cursus behaald";
                }else if (Reis.Boekingen.Any(b=> b.IsMonitor))
                {
                    Foutmelding = "Er is al een monitor gelinkt aan deze reis";
                }
                else
                {
                    Boeking boeking = DatabaseOperations.ZoekBoeking(TeVerwijderenGebruiker, Reis);
                    boeking.IsMonitor= true;
                    DatabaseOperations.BoekingBewerken(boeking);
                    toonMonitor(TeVerwijderenGebruiker);
                }
            }
        }

        private void Linken()
        {
            if (GeselecteerdeGebruiker == null)
            {
                Foutmelding = "Gelieven een gebruiker te selecteren";
            }
            else if (Reis.Id <= 0)
            {
                DatabaseOperations.ReisToevoegen(Reis);
                UpdateReizen();
                BewerkenOfToevoegenButton = "Bewerken";
            }

            if (GeselecteerdeGebruiker != null)
            {

                Gebruikers.Add(GeselecteerdeGebruiker);
                Boeking boeking = new Boeking
                {
                    Gebruiker = GeselecteerdeGebruiker,
                    Reis = Reis,
                    Status = "",
                    IsMonitor = false,
                    InschrijvingsDatum = DateTime.Now
                };
                DatabaseOperations.BoekingAanmaken(boeking);
                AlleGebruikers.Remove(GeselecteerdeGebruiker);
                GeselecteerdeGebruiker = null;
            }

        }
        private void ViewOpvullen()
        {
            Themas = DatabaseOperations.ThemasOphalen();
            Hoofdmonitoren = DatabaseOperations.HoofdmonitorenOphalen();
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
