using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class CursusBeherenViewModel : BaseViewModel
    {
        private List<Gebruiker> _gebruikers;
        public List<Gebruiker> Gebruikers
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

        public List<Gebruiker> AlleGebruikers { get; set; }

        private Cursus _cursus;

        public Cursus Cursus
        {
            get { return _cursus; }
            set { _cursus = value; }
        }

        public CursusBeherenViewModel()
        {
            AlleGebruikers = DatabaseOperations.OphalenLijstGebruikers();
            Gebruikers = new List<Gebruiker>();
            Cursus = new Cursus();
            BewerkenOfToevoegen = "Cursus toevoegen";
            BewerkenOfToevoegenButton = "Toevoegen";
        }

        public CursusBeherenViewModel(Cursus cursus)
        {
            AlleGebruikers = DatabaseOperations.OphalenLijstGebruikers();
            Gebruikers = AlleGebruikers?.Where(gebruiker => gebruiker.GebruikerCursussen?.Any(gc => gc.CursusId == cursus.Id) == true)?.ToList();

            Cursus = cursus;
            BewerkenOfToevoegen = "Cursus bewerken";
            BewerkenOfToevoegenButton = "Bewerken";
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

            int ok = DatabaseOperations.CursusToevoegen(Cursus);

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

            int ok = DatabaseOperations.CursusBewerken(Cursus);

            if (ok == 0)
            {
                Foutmelding = "Het bewerken van de cursus is niet gelukt!";
                return;
            }

            Foutmelding = "";
            UpdateCursussen();
        }

        public delegate void CursussenUpdateHandler(object sender, UpdateGenericListEventArgs<Cursus> e);
        public event CursussenUpdateHandler CursussenEventHandler;

        private void UpdateCursussen()
        {
            CursussenEventHandler?
                .Invoke(this, new UpdateGenericListEventArgs<Cursus>(DatabaseOperations.CursussenOphalen()));
        }
    }
}
