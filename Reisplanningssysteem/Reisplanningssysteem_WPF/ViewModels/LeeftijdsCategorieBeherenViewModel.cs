﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_WPF.Utils;
using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class LeeftijdsCategorieBeherenViewModel : BaseViewModel
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

        private LeeftijdsCategorie _categorie;

        public LeeftijdsCategorie Categorie
        {
            get { return _categorie; }
            set { _categorie = value; }
        }

        public LeeftijdsCategorieBeherenViewModel()
        {
            Categorie = new LeeftijdsCategorie();
            BewerkenOfToevoegen = "Categorie toevoegen";
            BewerkenOfToevoegenButton = "Toevoegen";
        }

        public LeeftijdsCategorieBeherenViewModel(LeeftijdsCategorie categorie)
        {
            Categorie = categorie;
            BewerkenOfToevoegen = "Categorie bewerken";
            BewerkenOfToevoegenButton = "Bewerken";
        }

        private void Toevoegen()
        {
            if (Categorie == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Categorie.IsGeldig())
            {
                Foutmelding = Categorie.Error;
                return;
            }

            int ok = DatabaseOperations.LeeftijdsCategorieToevoegen(Categorie);

            if (ok == 0)
            {
                Foutmelding = "Het toevoegen van de bestemming is niet gelukt!";
                return;
            }

            Categorie = new LeeftijdsCategorie();
            Foutmelding = "";
            UpdateCategorieën();
        }
        private void Bewerken()
        {
            if (Categorie == null)
            {
                Foutmelding = "Er is iets mis gelopen!";
                return;
            }

            if (!Categorie.IsGeldig())
            {
                Foutmelding = Categorie.Error;
                return;
            }

            int ok = DatabaseOperations.LeeftijdsCategorieBewerken(Categorie);

            if (ok == 0)
            {
                Foutmelding = "Het bewerken van de leeftijdscategorie is niet gelukt!";
                return;
            }

            Foutmelding = "";
            UpdateCategorieën();
        }

        public delegate void LeeftijdsCategorieënUpdateHandler(object sender, LeeftijdsCategorieënUpdateEventArgs e);
        public event LeeftijdsCategorieënUpdateHandler CategoriënEventHandler;

        private void UpdateCategorieën()
        {
            CategoriënEventHandler?
                .Invoke(this, new LeeftijdsCategorieënUpdateEventArgs(DatabaseOperations.LeeftijdsCategorieënOphalen()));
        }
    }
}
