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
    public class OnkostBeherenViewModel : BaseViewModel
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

        private Onkost _onkost;

        public Onkost Onkost
        {
            get { return _onkost; }
            set { _onkost = value; }
        }

        private ObservableCollection<Reis> _reizen;

        public ObservableCollection<Reis> Reizen
        {
            get { return _reizen; }
            set { _reizen = value; }
        }

        public OnkostBeherenViewModel()
        {
            Onkost = new Onkost();
            Onkost.ReisId = -1;
            BewerkenOfToevoegen = "Onkost toevoegen";
            BewerkenOfToevoegenButton = "Toevoegen";

            ViewOpvullen();
        }

        public OnkostBeherenViewModel(Onkost onkost)
        {
            Onkost = onkost;
            BewerkenOfToevoegen = "Onkost bewerken";
            BewerkenOfToevoegenButton = "Bewerken";

            ViewOpvullen();
        }
        private void ViewOpvullen()
        {
            Reizen = new ObservableCollection<Reis>(DatabaseOperations.ReizenOphalen());
        }

        private void Bewerken()
        {
            if (Onkost == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Onkost.IsGeldig())
            {
                Foutmelding = Onkost.Error;
                return;
            }

            int ok = DatabaseOperations.OnkostBewerken(Onkost);

            if (ok == 0)
            {
                Foutmelding = "Het bewerken van de onkost is niet gelukt!";
                return;
            }

            Foutmelding = "";
            UpdateReizen();
        }

        private void Toevoegen()
        {
            if (Onkost == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Onkost.IsGeldig())
            {
                Foutmelding = Onkost.Error;
                return;
            }

            int ok = DatabaseOperations.OnkostToevoegen(Onkost);

            if (ok == 0)
            {
                Foutmelding = "Het toevoegen van de onkost is niet gelukt!";
                return;
            }

            Onkost = new Onkost();
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
