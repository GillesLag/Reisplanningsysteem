using GalaSoft.MvvmLight.Command;
using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Reisplanningssysteem_DAL.Data.UnitOfWork;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class CursusBeherenViewModel : BaseViewModel, IDisposable
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
            }

        }

        private void Verwijderen()
        {
            if (TeVerwijderenGebruiker==null)
            {
                Foutmelding = "Gelieven een te verwijderen gebruiker te selecteren";
            }
            else
            {

                AlleGebruikers.Add(TeVerwijderenGebruiker);
                GebruikerCursus link = _unitOfWork.GebruikersCursusRepo.Ophalen(x => x.Gebruiker == TeVerwijderenGebruiker && x.Cursus == Cursus).First();
                _unitOfWork.GebruikersCursusRepo.Verwijderen(link);
                Gebruikers.Remove(TeVerwijderenGebruiker);
                TeVerwijderenGebruiker = null;
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

        private Cursus _cursus;

        public Cursus Cursus
        {
            get { return _cursus; }
            set { _cursus = value; }
        }

        public CursusBeherenViewModel()
        {
            AlleGebruikers = new ObservableCollection<Gebruiker>(_unitOfWork.GebruikerRepo.Ophalen());
            Gebruikers = new ObservableCollection<Gebruiker>();
            Cursus = new Cursus();
            BewerkenOfToevoegen = "Cursus toevoegen";
            BewerkenOfToevoegenButton = "Toevoegen";
            LinkButton = "Linken";
            VerwijderGebruiker = "VerwijderGebruiker";
        }



        public CursusBeherenViewModel(Cursus cursus)
        {
            AlleGebruikers = new ObservableCollection<Gebruiker>(_unitOfWork.GebruikerRepo.Ophalen(g => g.GebruikerCursussen
                .Any(gc => gc.CursusId == cursus.Id) == false)
                .OrderBy(g => g.ToString()));
              
            Gebruikers = new ObservableCollection<Gebruiker>(_unitOfWork.GebruikerRepo.Ophalen(g => g.GebruikerCursussen
                .Any(gc => gc.CursusId == cursus.Id) == true, g => g.GebruikerCursussen)
                .OrderBy(g => g.ToString()));

            Cursus = cursus;
            BewerkenOfToevoegen = "Cursus bewerken";
            BewerkenOfToevoegenButton = "Bewerken";
            LinkButton = "Linken";
            VerwijderGebruiker = "VerwijderGebruiker";
        }

        private void Toevoegen()
        {
            if (Cursus == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Cursus.IsGeldig())
            {
                Foutmelding = Cursus.Error;
                return;
            }

            int ok = _unitOfWork.CursusRepo.Toevoegen(Cursus);

            if (ok == 0)
            {
                Foutmelding = "Het toevoegen van de cursus is niet gelukt!";
                return;
            }

            Cursus = new Cursus();
            Foutmelding = "";
            UpdateCursussen();
        }
        private void Bewerken()
        {
            if (Cursus == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Cursus.IsGeldig())
            {
                Foutmelding = Cursus.Error;
                return;
            }

            int ok = _unitOfWork.CursusRepo.Bewerken(Cursus);

            if (ok == 0)
            {
                Foutmelding = "Het bewerken van de cursus is niet gelukt!";
                return;
            }

            Foutmelding = "";
            UpdateCursussen();
        }

        private void Linken()
        {
            if (GeselecteerdeGebruiker == null)
            {
                Foutmelding = "Gelieven een gebruiker te selecteren";
            }
            else if(Cursus.Id<=0)
            {
                _unitOfWork.CursusRepo.Toevoegen(Cursus);
                UpdateCursussen();
                BewerkenOfToevoegenButton = "Bewerken";
            }
            if (GeselecteerdeGebruiker != null)
            {
                Gebruikers.Add(GeselecteerdeGebruiker);
                GebruikerCursus link = new GebruikerCursus();
                link.Gebruiker = GeselecteerdeGebruiker;
                link.Cursus = Cursus;
                _unitOfWork.GebruikersCursusRepo.Toevoegen(link);
                AlleGebruikers.Remove(GeselecteerdeGebruiker);
                GeselecteerdeGebruiker = null;
            }
        }

        public delegate void CursussenUpdateHandler(object sender, UpdateGenericListEventArgs<Cursus> e);
        public event CursussenUpdateHandler CursussenEventHandler;

        private void UpdateCursussen()
        {
            CursussenEventHandler?
                .Invoke(this, new UpdateGenericListEventArgs<Cursus>(_unitOfWork.CursusRepo.Ophalen().OrderBy(c => c.Naam).ToList()));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
