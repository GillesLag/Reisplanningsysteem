using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_DAL.Data.UnitOfWork;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class ThemaBeherenViewModel : BaseViewModel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new ReisplanningssysteemContext());
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

        private Thema _thema;

        public Thema Thema
        {
            get { return _thema; }
            set { _thema = value; }
        }

        public ThemaBeherenViewModel()
        {
            Thema = new Thema();
            BewerkenOfToevoegen = "Thema toevoegen";
            BewerkenOfToevoegenButton = "Toevoegen";
        }

        public ThemaBeherenViewModel(Thema thema)
        {
            Thema = thema;
            BewerkenOfToevoegen = "Thema bewerken";
            BewerkenOfToevoegenButton = "Bewerken";
        }

        public void Toevoegen()
        {
            if (Thema == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Thema.IsGeldig())
            {
                Foutmelding = Thema.Error;
                return;
            }

            int ok = _unitOfWork.ThemaRepo.Toevoegen(Thema);

            if (ok == 0)
            {
                Foutmelding = "Het toevoegen van het thema is niet gelukt!";
                return;
            }

            Thema = new Thema();
            Foutmelding = "";
            UpdateThemas();
        }

        private void Bewerken()
        {
            if (Thema == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Thema.IsGeldig())
            {
                Foutmelding = Thema.Error;
                return;
            }

            int ok = _unitOfWork.ThemaRepo.Bewerken(Thema);

            if (ok == 0)
            {
                Foutmelding = "Het bewerken van het thema is niet gelukt!";
                return;
            }

            Foutmelding = "";
            UpdateThemas();
        }

        public delegate void ThemasUpdateEventHandler(object sender, UpdateGenericListEventArgs<Thema> e);
        public event ThemasUpdateEventHandler ThemasUpdatedEvent;

        private void UpdateThemas()
        {
            ThemasUpdatedEvent?.Invoke(this, new UpdateGenericListEventArgs<Thema>(_unitOfWork.ThemaRepo.Ophalen().OrderBy(t => t.Naam).ToList()));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
