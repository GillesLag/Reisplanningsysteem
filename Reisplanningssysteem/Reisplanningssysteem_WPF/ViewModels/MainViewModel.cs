using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public override string this[string columnName] => throw new NotImplementedException();

        public override bool CanExecute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Bestemmingen": return true;
            }

            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Bestemmingen": BestemmingView(); break;
            }
        }

        private void BestemmingView()
        {
            BestemmingenViewModel viewmodel = new BestemmingenViewModel();
            Views.BestemmingenView view = new Views.BestemmingenView();
            view.DataContext = viewmodel;
            view.Show();
        }
    }
}
