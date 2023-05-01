using Reisplanningssysteem_WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenBestemmingen": OpenBestemmingView(); break;
                case "OpenPersonenBeheren": OpenPersonenBeherenView(); break;
                case "OpenLeeftijdsCategorieën": OpenLeeftijdsCategorieënView(); break;
            }
        }

        private void OpenBestemmingView()
        {
            var viewmodel = new BestemmingenViewModel();
            var view = new BestemmingenView();
            OpenView(ref viewmodel, view);
        }

        private void OpenPersonenBeherenView()
        {
            var viewmodel = new PersonenBeherenViewModel();
            var view = new PersonenBeherenView();
            OpenView(ref viewmodel, view);
        }

        private void OpenLeeftijdsCategorieënView()
        {
            var viewmodel = new LeeftijdsCategorieënViewModel();
            var view = new LeeftijdsCategorieënView();
            OpenView(ref viewmodel, view);
        }

        private void OpenView<T>(ref T viewmodel, Window view)
        {
            view.DataContext = viewmodel;
            view.Show();
        }
    }
}
