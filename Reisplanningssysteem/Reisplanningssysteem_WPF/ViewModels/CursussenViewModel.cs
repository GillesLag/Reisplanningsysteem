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

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class CursussenViewModel : BaseViewModel
    {
        private ObservableCollection<Cursus> _Cursussen;
        private Cursus _GeselecteerdeCursus;
        private string _foutmelding;

        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenCursusBewerken":
                case "Verwijderen":
                    if (GeselecteerdeCursus == null)
                    {
                        return false;
                    }

                    return true;
                case "OpenCursusToevoegen": return true;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": Verwijderen(); break;
                case "OpenCursusToevoegen": OpenCursusToevoegen(); break;
                case "OpenCursusBewerken": OpenCursusBewerken(); break;
            }
        }

        public string Foutmelding
        {
            get { return _foutmelding; }
            set { _foutmelding = value; }
        }

        public ObservableCollection<Cursus> Cursussen
        {
            get { return _Cursussen; }
            set { _Cursussen = value; }
        }

        public Cursus GeselecteerdeCursus
        {
            get { return _GeselecteerdeCursus; }
            set { _GeselecteerdeCursus = value; }
        }

        public CursussenViewModel()
        {
            Cursussen = new ObservableCollection<Cursus>(DatabaseOperations.CursussenOphalen());
            GeselecteerdeCursus = null;
        }
        private void OpenCursusBewerken()
        {
            CursusBeherenViewModel viewmodel = new(GeselecteerdeCursus);
            ViewOpenen(viewmodel);
        }

        private void OpenCursusToevoegen()
        {
            CursusBeherenViewModel viewmodel = new();
            ViewOpenen(viewmodel);
        }
        private void ViewOpenen(CursusBeherenViewModel vm)
        {
            CursusBeherenView view = new();
            vm.CursussenEventHandler += ViewModel_CursussenEventHandler; 
            view.DataContext = vm;
            view.ShowDialog();

            GeselecteerdeCursus = null;
        }

        private void ViewModel_CursussenEventHandler(object sender, UpdateGenericListEventArgs<Cursus> e)
        {
            Cursussen = new ObservableCollection<Cursus>(e.GenericList);
        }

        private void Verwijderen()
        {
            if (GeselecteerdeCursus == null)
            {
                Foutmelding = "Selecteer eerst een cursus!";
                return;
            }

            int ok = DatabaseOperations.CursusVerwijderen(GeselecteerdeCursus);

            if (ok == 0)
            {
                Foutmelding = "Cursus is niet verwijderd kunnen worden";
                return;
            }

            Cursussen = new ObservableCollection<Cursus>(DatabaseOperations.CursussenOphalen());
            GeselecteerdeCursus = null;
        }
    }
}
