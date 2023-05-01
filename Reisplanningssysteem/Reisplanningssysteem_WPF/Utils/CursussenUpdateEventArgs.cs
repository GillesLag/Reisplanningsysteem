using Reisplanningssysteem_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_WPF.Utils
{
    public class CursussenUpdateEventArgs: EventArgs
    {
        public List<Cursus> Cursussen { get; set; }
        public CursussenUpdateEventArgs(List<Cursus> cursussen)
        {
            Cursussen = cursussen;
        }
    }
}
