using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_WPF.Utils
{
    public class UpdateGenericListEventArgs<T> : EventArgs
    {
        public List<T> GenericList { get; set; }
        public UpdateGenericListEventArgs(List<T> genericList)
        {
            GenericList = genericList;
        }
    }
}
