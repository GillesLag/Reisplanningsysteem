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
    public class ThemasViewModel : BaseViewModel
    {
        private ObservableCollection<Thema> _themas;
        private Thema _geselecteerdeThema;
        private string _foutmelding;
        private string _filter;
        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenThemaBewerken":
                case "Verwijderen": return GeselecteerdeThema != null;
                case "OpenThemaToevoegen": return true;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Verwijderen": Verwijderen(); break;
                case "OpenThemaToevoegen": OpenThemaToevoegen(); break;
                case "OpenThemaBewerken": OpenThemaBewerken(); break;
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

        public List<Thema> AlleThemas { get; set; }

        public ObservableCollection<Thema> Themas
        {
            get { return _themas; }
            set { _themas = value; }
        }

        public Thema GeselecteerdeThema
        {
            get { return _geselecteerdeThema; }
            set { _geselecteerdeThema = value; }
        }

        public ThemasViewModel()
        {
            AlleThemas = DatabaseOperations.ThemasOphalen();
            Themas = new ObservableCollection<Thema>(AlleThemas);
            GeselecteerdeThema = null;
        }
        private void OpenThemaBewerken()
        {
            ThemaBeherenViewModel viewmodel = new(GeselecteerdeThema);
            ViewOpenen(viewmodel);
        }

        private void OpenThemaToevoegen()
        {
            ThemaBeherenViewModel viewmodel = new();
            ViewOpenen(viewmodel);
        }
        private void ViewOpenen(ThemaBeherenViewModel vm)
        {
            ThemaBeherenView view = new();
            vm.ThemasUpdatedEvent += ViewModel_ThemasEventHandler; ;
            view.DataContext = vm;
            view.ShowDialog();

            GeselecteerdeThema = null;
        }

        private void ViewModel_ThemasEventHandler(object sender, UpdateGenericListEventArgs<Thema> e)
        {
            AlleThemas = e.GenericList;
            Themas = new ObservableCollection<Thema>(e.GenericList);
        }

        private void Verwijderen()
        {
            if (GeselecteerdeThema == null)
            {
                Foutmelding = "Selecteer eerst een bestemming!";
                return;
            }

            int ok = DatabaseOperations.ThemaVerwijderen(GeselecteerdeThema);

            if (ok == 0)
            {
                Foutmelding = "Bestemming is niet verwijderd kunnen worden";
                return;
            }

            AlleThemas = DatabaseOperations.ThemasOphalen();
            Themas = new ObservableCollection<Thema>(AlleThemas);
            GeselecteerdeThema = null;
        }

        private void FilterCategorieën()
        {
            Themas = new ObservableCollection<Thema>(AlleThemas.Where(c => c.Naam.ToLower().Contains(Filter.ToLower())));
        }
    }
}
