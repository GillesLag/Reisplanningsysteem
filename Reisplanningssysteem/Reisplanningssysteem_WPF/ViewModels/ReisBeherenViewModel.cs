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
using Reisplanningssysteem_DAL.Data.UnitOfWork;
using Microsoft.IdentityModel.Tokens;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class ReisBeherenViewModel : BaseViewModel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new ReisplanningssysteemContext());
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


        private ObservableCollection<Gebruiker> _monitors;
        public ObservableCollection<Gebruiker> Monitors
        {
            get { return _monitors; }
            set { _monitors = value; }
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
            AlleGebruikers = new ObservableCollection<Gebruiker>(_unitOfWork.GebruikerRepo.Ophalen().OrderBy(g => g.ToString()));
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
            AlleGebruikers = new ObservableCollection<Gebruiker>(_unitOfWork.GebruikerRepo.Ophalen(g => g.Boekingen
                .Any(b => b.ReisId == reis.Id) == false,
                g => g.Boekingen));

            Gebruikers = new ObservableCollection<Gebruiker>(_unitOfWork.GebruikerRepo.Ophalen(g => g.Boekingen
                .Any(b => b.ReisId == reis.Id) == true,
                g => g.Boekingen));

            Monitors = new ObservableCollection<Gebruiker>(Gebruikers.Where(b => b.Boekingen.Where(b=>b.ReisId==reis.Id).Any(b => b.IsMonitor)));

            Reis = reis;
            BewerkenOfToevoegen = "Reis bewerken";
            BewerkenOfToevoegenButton = "Bewerken";
            LinkButton = "Linken";
            VerwijderGebruiker = "VerwijderGebruiker";
            MaakHoofdmonitor = "MaakHoofdmonitor";
            Gebruiker geselecteerdeMonitor = reis.Boekingen.Where(b=>b.IsMonitor).Select(b=>b.Gebruiker).FirstOrDefault();

            ViewOpvullen();
        }

            
        private void Verwijderen()
        {
            if (TeVerwijderenGebruiker == null)
            {
                Foutmelding = "Gelieven een te verwijderen gebruiker te selecteren";
            }
            else
            {
                if (Monitors.Contains(TeVerwijderenGebruiker))
                {
                    Monitors.Remove(TeVerwijderenGebruiker);
                }
                AlleGebruikers.Add(TeVerwijderenGebruiker);
                Boeking boeking = _unitOfWork.BoekingRepo.Ophalen(x => x.Gebruiker == TeVerwijderenGebruiker && x.Reis == Reis).First();
                _unitOfWork.BoekingRepo.Verwijderen(boeking);
                Gebruikers.Remove(TeVerwijderenGebruiker);
                TeVerwijderenGebruiker = null;
            }

            UpdateReizen();
        }

        private void hoofMonitorLinken()
        {
            if (TeVerwijderenGebruiker == null)
            {
                Foutmelding = "Gelieven een gebruiker te selecteren";
            }
            else
            {

                if (!TeVerwijderenGebruiker.BasisCursus)
                {
                    Foutmelding = "Deze gebruiker heeft geen monitor cursus behaald";
                }
                else if (Monitors.Contains(TeVerwijderenGebruiker))
                {
                    Foutmelding = "Deze gebruiker is al een monitor.";
                }
                else
                {
                    Boeking boeking = _unitOfWork.BoekingRepo.Ophalen(x => x.Gebruiker == TeVerwijderenGebruiker && x.Reis == Reis).First();
                    boeking.IsMonitor= true;
                    Monitors.Add(TeVerwijderenGebruiker);
                    _unitOfWork.BoekingRepo.Bewerken(boeking);

                }
            }
        }

        private void Linken()
        {
            if (GeselecteerdeGebruiker == null)
            {
                Foutmelding = "Gelieven een gebruiker te selecteren";
            }
            else if (Reis.IsGeldig() && Reis.Id <= 0)
            {
                _unitOfWork.ReisRepo.Toevoegen(Reis);
                UpdateReizen();
                BewerkenOfToevoegenButton = "Bewerken";
            }
            if (!string.IsNullOrEmpty(Reis.Error))
            {
                Foutmelding = Reis.Error;
                return;
            }

            if (GeselecteerdeGebruiker != null)
            {
                Boeking boeking = new Boeking
                {
                    Gebruiker = GeselecteerdeGebruiker,
                    Reis = Reis,
                    Status = "",
                    IsMonitor = false,
                    InschrijvingsDatum = DateTime.Now
                };

                if (GeselecteerdeGebruiker.Boekingen==null)
                {
                    GeselecteerdeGebruiker.Boekingen=new List<Boeking>();
                }
                GeselecteerdeGebruiker.Boekingen.Add(boeking);

                Gebruikers.Add(GeselecteerdeGebruiker);
                _unitOfWork.BoekingRepo.Toevoegen(boeking);
                AlleGebruikers.Remove(GeselecteerdeGebruiker);
                GeselecteerdeGebruiker = null;
            }

        }
        private void ViewOpvullen()
        {
            Themas = _unitOfWork.ThemaRepo.Ophalen().ToList();
            Hoofdmonitoren = _unitOfWork.GebruikerRepo.Ophalen(g => g.HoofmonitorCursus, g => g.Gemeente).ToList();
            Bestemmingen = _unitOfWork.BestemmingRepo.Ophalen().ToList();
            LeeftijdsCategorieën = _unitOfWork.LeeftijdsCategorieRepo.Ophalen().ToList();
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

            int ok = _unitOfWork.ReisRepo.Bewerken(Reis);

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

            int ok = _unitOfWork.ReisRepo.Toevoegen(Reis);

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
            List<Reis> reizen = _unitOfWork.ReisRepo.Ophalen(r => r.Bestemming,
                r => r.Hoofdmonitor,
                r => r.Thema,
                r => r.LeeftijdsCategorie,
                r => r.Boekingen,
                r => r.Onkosten).OrderBy(r => r.Naam).ToList();

            IEnumerable<Gebruiker> gebruikers = _unitOfWork.GebruikerRepo.Ophalen();

            foreach (Reis reis in reizen)
            {
                foreach (Boeking boeking in reis.Boekingen)
                {
                    boeking.Gebruiker = gebruikers.First(g => g.Id == boeking.GebruikerId);
                }
            }

            ReizenUpdatedEvent?.Invoke(this, new UpdateGenericListEventArgs<Reis>(reizen));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
