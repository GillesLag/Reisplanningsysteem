﻿using Reisplanningssysteem_DAL;
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
using Reisplanningssysteem_DAL.Data.UnitOfWork;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class PersoonViewModel : BaseViewModel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new ReisplanningssysteemContext());
        private Gebruiker _gebruikerRecord;
        private string _foutmelding;
        private bool _bewerkModus;
        private ObservableCollection<Gemeente> _gemeentes;
        public Gemeente _geselecteerdGemeente { get; set; }
        
        public PersoonViewModel(Gebruiker gebruikerRecord)
        {
            GebruikerRecord = gebruikerRecord;
            Bewerkmodus = true;
            List<Gemeente> gemeentes = _unitOfWork.GemeenteRepo.Ophalen().OrderBy(g => g.Naam).ToList();
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
            GebruikerRecord.GemeenteId = -1;
            Bewerkmodus = false;
            List<Gemeente> gemeentes = _unitOfWork.GemeenteRepo.Ophalen().OrderBy(g => g.Naam).ToList();
            Gemeentes = new ObservableCollection<Gemeente>(gemeentes);
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }
        public bool Bewerkmodus
        {
            get { return _bewerkModus; }
            set { _bewerkModus = value; }
        }


        public override string this[string columnName]
        {
            get { return ""; }
        }

        public Gebruiker GebruikerRecord
        {
            get { return _gebruikerRecord; }
            set { _gebruikerRecord = value; }
        }

        public ObservableCollection<Gemeente> Gemeentes
        {
            get { return _gemeentes; }
            set { _gemeentes = value; }
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
                        int ok = _unitOfWork.GebruikerRepo.Toevoegen(GebruikerRecord);
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
                        int ok = _unitOfWork.GebruikerRepo.Bewerken(GebruikerRecord);
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
                else
                {
                    Foutmelding = GebruikerRecord.Error;
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
            GebruikersUpdated?.Invoke(this, new UpdateGenericListEventArgs<Gebruiker>(_unitOfWork.GebruikerRepo.Ophalen(p => p.Gemeente, p => p.GebruikerCursussen, p => p.Boekingen).OrderBy(g => g.ToString()).ToList()));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
