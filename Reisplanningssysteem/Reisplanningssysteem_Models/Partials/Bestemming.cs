﻿using Reisplanningssysteem_Models.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_Models
{
    public partial class Bestemming : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Naam) && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Geef een naam in!";
                }

                if (columnName == nameof(Land) && string.IsNullOrWhiteSpace(Land))
                {
                    return "Geef een land in!";
                }

                if (columnName == nameof(GemeenteId) && GemeenteId == 0)
                {
                    return "Selecteer een gemeente!";
                }

                if (columnName == nameof(Straat) && string.IsNullOrWhiteSpace(Straat))
                {
                    return "Geef een straat in!";
                }

                if (columnName == nameof(Huisnummer) && string.IsNullOrWhiteSpace(Huisnummer))
                {
                    return "Geef een huisnummer in!";
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
