using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_DAL;
using Reisplanningssysteem_DAL.Data.UnitOfWork;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using Reisplanningssysteem_WPF.Views;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class BestemmingenViewModel : BaseViewModel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new ReisplanningssysteemContext());
        public override string this[string columnName] => throw new NotImplementedException(); 
        
        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenBestemmingBewerken":
                case "Verwijderen": 
                    if (GeselecteerdeBestemming == null)
                    {
                        return false;
                    }

                    return true;
                case "OpenBestemmingToevoegen": return true;
            }

            return true;
        }
        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": Verwijderen(); break;
                case "OpenBestemmingToevoegen":  OpenBestemmingToevoegen(); break;
                case "OpenBestemmingBewerken":  OpenBestemmingBewerken(); break;
            }
        }

        private string _foutmelding;

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        private string _filter;

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; FilterBestemmingen(); }
        }

        public List<Bestemming> AlleBestemmingen { get; set; }

        private ObservableCollection<Bestemming> _bestemmingen;

        public ObservableCollection<Bestemming> Bestemmingen
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

        public BestemmingenViewModel()
        {
            AlleBestemmingen = _unitOfWork.BestemmingRepo.Ophalen(b => b.Gemeente).OrderBy(b => b.Naam).ToList();
            Bestemmingen = new ObservableCollection<Bestemming>(AlleBestemmingen);
        }

        private void Verwijderen()
        {
            if (GeselecteerdeBestemming == null)
            {
                Foutmelding = "Selecteer eerst een bestemming!";
                return;
            }

            int ok = _unitOfWork.BestemmingRepo.Verwijderen(GeselecteerdeBestemming);

            if (ok == 0)
            {
                Foutmelding = "Bestemming is niet verwijderd kunnen worden";
                return;
            }

            AlleBestemmingen = _unitOfWork.BestemmingRepo.Ophalen(b => b.Gemeente).OrderBy(b => b.Naam).ToList();
            Bestemmingen = new ObservableCollection<Bestemming>(AlleBestemmingen);
            GeselecteerdeBestemming = null;
        }

        private void OpenBestemmingToevoegen()
        {
            BestemmingBeherenViewModel viewmodel = new();
            ViewOpenen(viewmodel);
        }

        private void OpenBestemmingBewerken()
        {
            BestemmingBeherenViewModel viewmodel = new(GeselecteerdeBestemming);
            ViewOpenen(viewmodel);
        }

        private void ViewOpenen (BestemmingBeherenViewModel vm)
        {
            BestemmingBeherenView view = new();
            vm.BestemmingenUpdatedEvent += Viewmodel_BestemmingenUpdatedEvent;
            view.DataContext = vm;
            view.ShowDialog();

            GeselecteerdeBestemming = null;
        }

        private void FilterBestemmingen()
        {
            Bestemmingen = new ObservableCollection<Bestemming>(AlleBestemmingen.Where(b => b.Naam.ToLower().Contains(Filter.ToLower())));
        }

        private void Viewmodel_BestemmingenUpdatedEvent(object sender, UpdateGenericListEventArgs<Bestemming> e)
        {
            AlleBestemmingen = e.GenericList;
            Bestemmingen = new ObservableCollection<Bestemming>(e.GenericList);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
