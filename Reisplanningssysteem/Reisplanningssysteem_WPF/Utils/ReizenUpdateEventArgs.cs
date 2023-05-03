using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models;

namespace Reisplanningssysteem_WPF.Utils
{
    public class ReizenUpdateEventArgs : EventArgs
    {
        public List<Reis> UpdatedReizen { get; set; }
        public ReizenUpdateEventArgs(List<Reis> reizen)
        {
            UpdatedReizen = reizen;
        }
    }
}
