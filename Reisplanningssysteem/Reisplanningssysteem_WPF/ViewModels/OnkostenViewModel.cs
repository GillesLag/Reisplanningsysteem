using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_WPF.Utils;
using Reisplanningssysteem_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_DAL.Data.UnitOfWork;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class OnkostenViewModel : BaseViewModel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new ReisplanningssysteemContext());
        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenOnkostBewerken":
                case "Verwijderen": return GeselecteerdeOnkost != null;
                case "OpenOnkostToevoegen": return true;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenOnkostBewerken": OpenOnkostBewerken(); break;
                case "Verwijderen": Verwijderen(); break;
                case "OpenOnkostToevoegen": OpenOnkostToevoegen(); break;
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
            set { _filter = value; FilterReizen(); }
        }

        public List<Reis> AlleReizen { get; set; }

        private ObservableCollection<Reis> _reizen;

        public ObservableCollection<Reis> Reizen
        {
            get { return _reizen; }
            set { _reizen = value; }
        }

        private Onkost _geselecteerdeOnkost;

        public Onkost GeselecteerdeOnkost
        {
            get { return _geselecteerdeOnkost; }
            set { _geselecteerdeOnkost = value; }
        }

        public OnkostenViewModel()
        {
            AlleReizen = _unitOfWork.ReisRepo.Ophalen(r => r.Bestemming,
                r => r.Hoofdmonitor,
                r => r.Thema,
                r => r.LeeftijdsCategorie,
                r => r.Boekingen,
                r => r.Onkosten).OrderBy(r => r.Naam).ToList();

            Reizen = new ObservableCollection<Reis>(AlleReizen);
        }

        private void Verwijderen()
        {
            if (GeselecteerdeOnkost == null)
            {
                Foutmelding = "Selecteer eerst een Reis!";
                return;
            }

            int ok = _unitOfWork.OnkostRepo.Verwijderen(GeselecteerdeOnkost);

            if (ok == 0)
            {
                Foutmelding = "Reis is niet verwijderd kunnen worden";
                return;
            }

            AlleReizen = _unitOfWork.ReisRepo.Ophalen(r => r.Onkosten).OrderBy(r => r.Naam).ToList();
            Reizen = new ObservableCollection<Reis>(AlleReizen);
            GeselecteerdeOnkost = null;
        }
        private void OpenOnkostToevoegen()
        {
            OnkostBeherenViewModel viewmodel = new();
            ViewOpenen(viewmodel);
        }

        private void OpenOnkostBewerken()
        {
            OnkostBeherenViewModel viewmodel = new(GeselecteerdeOnkost);
            ViewOpenen(viewmodel);
        }
        private void ViewOpenen(OnkostBeherenViewModel vm)
        {
            OnkostBeherenView view = new();
            vm.ReizenUpdatedEvent += Viewmodel_ReizenUpdatedEvent;
            view.DataContext = vm;
            view.ShowDialog();

            GeselecteerdeOnkost = null;
        }

        private void FilterReizen()
        {
            Reizen = new ObservableCollection<Reis>(AlleReizen.Where(r => r.Naam.ToLower().Contains(Filter.ToLower())));
        }

        private void Viewmodel_ReizenUpdatedEvent(object sender, UpdateGenericListEventArgs<Reis> e)
        {
            AlleReizen = e.GenericList;
            Reizen = new ObservableCollection<Reis>(e.GenericList);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
