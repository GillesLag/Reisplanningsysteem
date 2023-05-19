using Reisplanningssysteem_DAL;
using Reisplanningssysteem_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_WPF.ViewModels
{
    public class OnkostenViewModel : BaseViewModel
    {
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
            
        }

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
            Reizen = new ObservableCollection<Reis>(DatabaseOperations.ReizenOphalen());
        }


    }
}
