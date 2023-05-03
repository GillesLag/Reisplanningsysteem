using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models.Partials;

namespace Reisplanningssysteem_Models
{
    public partial class Thema : BasisKlasse
    {
        public override string this[string columnName] => throw new NotImplementedException();

        public override string ToString()
        {
            return Naam;
        }

    }
}
