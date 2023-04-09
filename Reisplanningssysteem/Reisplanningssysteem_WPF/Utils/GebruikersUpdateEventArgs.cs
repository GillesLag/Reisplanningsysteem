using Reisplanningssysteem_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_WPF.Utils
{
    public class GebruikersUpdateEventArgs : EventArgs
    {
        public List<Gebruiker> UpdatedGebruikers { get; set; }

        public GebruikersUpdateEventArgs(List<Gebruiker> gebruikers)
        {
            UpdatedGebruikers = gebruikers;
        }
    }
}
