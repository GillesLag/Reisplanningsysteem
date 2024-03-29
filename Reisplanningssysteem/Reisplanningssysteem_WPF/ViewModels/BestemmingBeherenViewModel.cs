﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_DAL;
using Reisplanningssysteem_WPF.Utils;
using Reisplanningssysteem_DAL.Data.UnitOfWork;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class BestemmingBeherenViewModel : BaseViewModel, IDisposable
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

        private ObservableCollection<Gemeente> _gemeenten;

        public ObservableCollection<Gemeente> Gemeenten
        {
            get { return _gemeenten; }
            set { _gemeenten = value; }
        }

        private Gemeente _geselecteerdeGemeente;

        public Gemeente GeselecteerdeGemeente
        {
            get { return _geselecteerdeGemeente; }
            set { _geselecteerdeGemeente = value; }
        }

        private Bestemming _bestemming;

        public Bestemming Bestemming
        {
            get { return _bestemming; }
            set { _bestemming = value; }
        }

        public BestemmingBeherenViewModel()
        {
            Bestemming = new Bestemming();
            BewerkenOfToevoegen = "Bestemming toevoegen";
            BewerkenOfToevoegenButton = "Toevoegen";
            Gemeenten = new ObservableCollection<Gemeente>(_unitOfWork.GemeenteRepo.Ophalen().OrderBy(g => g.Naam));
        }

        public BestemmingBeherenViewModel(Bestemming bestemming)
        {
            Bestemming = bestemming;
            BewerkenOfToevoegen = "Bestemming bewerken";
            BewerkenOfToevoegenButton = "Bewerken";
            Gemeenten = new ObservableCollection<Gemeente>(_unitOfWork.GemeenteRepo.Ophalen().OrderBy(g => g.Naam));
            GeselecteerdeGemeente = bestemming.Gemeente;
        }

        public void Toevoegen()
        {
            if (Bestemming == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Bestemming.IsGeldig())
            {
                Foutmelding = Bestemming.Error;
                return;
            }

            int ok = _unitOfWork.BestemmingRepo.Toevoegen(Bestemming);

            if (ok == 0)
            {
                Foutmelding = "Het toevoegen van de bestemming is niet gelukt!";
                return;
            }

            Bestemming = new Bestemming();
            GeselecteerdeGemeente = null;
            Foutmelding = "";
            UpdateBestemmingen();
        }

        public void Bewerken()
        {
            if (Bestemming == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Bestemming.IsGeldig())
            {
                Foutmelding = Bestemming.Error;
                return;
            }

            int ok = _unitOfWork.BestemmingRepo.Bewerken(Bestemming);

            if (ok == 0)
            {
                Foutmelding = "Het bewerken van de bestemming is niet gelukt!";
                return;
            }

            Foutmelding = "";
            UpdateBestemmingen();
        }

        public delegate void BestemmingenUpdateEventHandler(object sender, UpdateGenericListEventArgs<Bestemming> e);
        public event BestemmingenUpdateEventHandler BestemmingenUpdatedEvent;

        private void UpdateBestemmingen()
        {
            BestemmingenUpdatedEvent?.Invoke(this,
                new UpdateGenericListEventArgs<Bestemming>(_unitOfWork.BestemmingRepo.Ophalen().OrderBy(b => b.Naam).ToList()));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
