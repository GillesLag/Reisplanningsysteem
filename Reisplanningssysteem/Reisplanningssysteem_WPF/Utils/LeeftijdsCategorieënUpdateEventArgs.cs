using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_WPF.Utils
{
    public class LeeftijdsCategorieënUpdateEventArgs : EventArgs
    {
        public List<LeeftijdsCategorie> Categoriën { get; set; }
        public LeeftijdsCategorieënUpdateEventArgs(List<LeeftijdsCategorie> categorieën)
        {
            Categoriën = categorieën;
        }
    }
}
