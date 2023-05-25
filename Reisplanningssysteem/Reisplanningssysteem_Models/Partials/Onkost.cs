using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models.Partials;

namespace Reisplanningssysteem_Models
{
    public partial class Onkost : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Bedrag) && Bedrag <= 0)
                    return "Gelieve een bedrag in te vullen.";

                if (columnName == nameof(Omschrijving) && string.IsNullOrWhiteSpace(Omschrijving))
                    return "Gelieve een omschrijving in te vullen.";

                if (columnName == nameof(ReisId) && ReisId < 0)
                    return "Gelieve een reis te selecteren";

                return "";
            }
        }
    }
}
