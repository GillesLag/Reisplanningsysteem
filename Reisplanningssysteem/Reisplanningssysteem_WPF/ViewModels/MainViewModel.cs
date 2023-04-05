using Reisplanningssysteem_WPF.Views;
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
            return true;
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "OpenPersonenBeheren": OpenPersonenBeherenView(); break;
            }
        }

        private void OpenPersonenBeherenView()
        {
            var vm = new PersonenBeherenViewModel();
            var view = new PersonenBeherenView();
            view.DataContext = vm;
            view.Show();
        }
    }
}
