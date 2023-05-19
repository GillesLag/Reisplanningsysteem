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
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Naam) && string.IsNullOrWhiteSpace(Naam))
                    return "Gelieve een naam in te vullen.";

                return "";
            }
        }

        public override string ToString()
        {
            return Naam;
        }

    }
}
