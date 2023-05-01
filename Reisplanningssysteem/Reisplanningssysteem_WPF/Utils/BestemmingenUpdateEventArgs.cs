using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_WPF.Utils
{
    public class BestemmingenUpdateEventArgs : EventArgs
    {
        public List<Bestemming> UpdatedBestemmingen { get; set; }
        public BestemmingenUpdateEventArgs(List<Bestemming> bestemmingen)
        {
            UpdatedBestemmingen = bestemmingen;
        }
    }
}
