﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_DAL;
using Reisplanningssysteem_WPF.Views;
using Reisplanningssysteem_WPF.Utils;
using Reisplanningssysteem_DAL.Data.UnitOfWork;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class LeeftijdsCategorieënViewModel : BaseViewModel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new ReisplanningssysteemContext());
        private ObservableCollection<LeeftijdsCategorie> _Categorieën;
        private LeeftijdsCategorie _GeselecteerdeCategorie;
        private string _foutmelding;
        private string _filter;

        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenCategorieBewerken":
                case "Verwijderen":
                    if (GeselecteerdeCategorie == null)
                    {
                        return false;
                    }

                    return true;
                case "OpenCategorieToevoegen": return true;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": Verwijderen(); break;
                case "OpenCategorieToevoegen": OpenCategorieToevoegen(); break;
                case "OpenCategorieBewerken": OpenCategorieBewerken(); break;
            }
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; FilterCategorieën(); }
        }

        public List<LeeftijdsCategorie> AlleCategorieën { get; set; }

        public ObservableCollection<LeeftijdsCategorie> Categorieën
        {
            get { return _Categorieën; }
            set { _Categorieën = value; }
        }

        public LeeftijdsCategorie GeselecteerdeCategorie
        {
            get { return _GeselecteerdeCategorie; }
            set { _GeselecteerdeCategorie = value; }
        }

        public LeeftijdsCategorieënViewModel()
        {
            AlleCategorieën = _unitOfWork.LeeftijdsCategorieRepo.Ophalen().OrderBy(l => l.Naam).ToList();
            Categorieën = new ObservableCollection<LeeftijdsCategorie>(AlleCategorieën);
            GeselecteerdeCategorie = null;
        }
        private void OpenCategorieBewerken()
        {
            LeeftijdsCategorieBeherenViewModel viewmodel = new(GeselecteerdeCategorie);
            ViewOpenen(viewmodel);
        }

        private void OpenCategorieToevoegen()
        {
            LeeftijdsCategorieBeherenViewModel viewmodel = new();
            ViewOpenen(viewmodel);
        }
        private void ViewOpenen(LeeftijdsCategorieBeherenViewModel vm)
        {
            LeeftijdsCategorieBeherenView view = new();
            vm.CategoriënEventHandler += ViewModel_CategoriënEventHandler; ;
            view.DataContext = vm;
            view.ShowDialog();

            GeselecteerdeCategorie = null;
        }

        private void ViewModel_CategoriënEventHandler(object sender, UpdateGenericListEventArgs<LeeftijdsCategorie> e)
        {
            AlleCategorieën = e.GenericList;
            Categorieën = new ObservableCollection<LeeftijdsCategorie>(e.GenericList);
        }

        private void Verwijderen()
        {
            if (GeselecteerdeCategorie == null)
            {
                Foutmelding = "Selecteer eerst een bestemming!";
                return;
            }

            int ok = _unitOfWork.LeeftijdsCategorieRepo.Verwijderen(GeselecteerdeCategorie);

            if (ok == 0)
            {
                Foutmelding = "Bestemming is niet verwijderd kunnen worden";
                return;
            }

            AlleCategorieën = _unitOfWork.LeeftijdsCategorieRepo.Ophalen().OrderBy(l => l.Naam).ToList();
            Categorieën = new ObservableCollection<LeeftijdsCategorie>(AlleCategorieën);
            GeselecteerdeCategorie = null;
        }

        private void FilterCategorieën()
        {
            Categorieën = new ObservableCollection<LeeftijdsCategorie>(AlleCategorieën.Where(c => c.Naam.ToLower().Contains(Filter.ToLower())));
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
