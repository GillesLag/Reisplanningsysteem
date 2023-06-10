using Reisplanningssysteem_Models.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_Models
{
    public partial class LeeftijdsCategorie : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Naam) && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Geef een naam in";
                }

                if (columnName == nameof(LeeftijdMinimum))
                {
                    if (LeeftijdMaximum - LeeftijdMinimum == 0)
                    {
                        return "Minimum en maximum leeftijd moet verschillend zijn.";
                    }

                    if (LeeftijdMinimum > LeeftijdMaximum)
                    {
                        return "Minimum leeftijd kan niet hoger liggen dan maximum leeftijd";
                    }

                    if (LeeftijdMinimum < 0)
                    {
                        return "Minimum leeftijd kan niet minder zijn dan 0";
                    }
                }

                if (columnName == nameof(LeeftijdMaximum) && LeeftijdMaximum < 0)
                {
                    return "Maximum leeftijd kan niet minder zijn dan 0";
                }

                return "";
            }
        }

        public override string ToString()
        {
            return Naam;
        }
    }
}
