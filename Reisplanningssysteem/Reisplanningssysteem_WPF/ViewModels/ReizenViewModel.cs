using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models;
using Reisplanningssysteem_DAL;
using System.Collections.ObjectModel;
using Reisplanningssysteem_WPF.Utils;
using Reisplanningssysteem_WPF.Views;
using Reisplanningssysteem_DAL.Data.UnitOfWork;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class ReizenViewModel : BaseViewModel, IDisposable
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork(new ReisplanningssysteemContext());
        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenReisBewerken":
                case "Verwijderen":
                    if (GeselecteerdeReis == null)
                    {
                        return false;
                    }

                    return true;
                case "OpenReisToevoegen": return true;
            }

            return true;
        }
        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": Verwijderen(); break;
                case "OpenReisToevoegen": OpenReisToevoegen(); break;
                case "OpenReisBewerken": OpenReisBewerken(); break;
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

        private Reis _geselecteerdeReis;

        public Reis GeselecteerdeReis
        {
            get { return _geselecteerdeReis; }
            set { _geselecteerdeReis = value; }
        }

        public ReizenViewModel()
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
            if (GeselecteerdeReis == null)
            {
                Foutmelding = "Selecteer eerst een Reis!";
                return;
            }

            int ok = _unitOfWork.ReisRepo.Verwijderen(GeselecteerdeReis);

            if (ok == 0)
            {
                Foutmelding = "Reis is niet verwijderd kunnen worden";
                return;
            }

            AlleReizen = _unitOfWork.ReisRepo.Ophalen(r => r.Bestemming,
                r => r.Hoofdmonitor,
                r => r.Thema,
                r => r.LeeftijdsCategorie,
                r => r.Boekingen,
                r => r.Onkosten).OrderBy(r => r.Naam).ToList();
            Reizen = new ObservableCollection<Reis>(AlleReizen);
            GeselecteerdeReis = null;
        }
        private void OpenReisToevoegen()
        {
            ReisBeherenViewModel viewmodel = new();
            ViewOpenen(viewmodel);
        }

        private void OpenReisBewerken()
        {
            ReisBeherenViewModel viewmodel = new(GeselecteerdeReis);
            ViewOpenen(viewmodel);
        }
        private void ViewOpenen(ReisBeherenViewModel vm)
        {
            ReisBeheerView view = new();
            vm.ReizenUpdatedEvent += Viewmodel_ReizenUpdatedEvent;
            view.DataContext = vm;
            view.ShowDialog();

            GeselecteerdeReis = null;
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
