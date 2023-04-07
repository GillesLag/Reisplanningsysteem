using Reisplanningssysteem_Models.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_Models
{
    public partial class Gemeente : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Naam) && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Geef een naam in!";
                }

                if (columnName == nameof(Postcode) && string.IsNullOrWhiteSpace(Postcode))
                {
                    return "geef een postcode in ";
                }

                return "";
            }
        }

        public override string? ToString()
        {
            return Naam;
        }
    }
}
