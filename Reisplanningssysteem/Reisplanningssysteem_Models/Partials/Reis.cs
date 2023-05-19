using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reisplanningssysteem_Models.Partials;

namespace Reisplanningssysteem_Models
{
    public partial class Reis : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == nameof(Naam) && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Vul een naam in";
                }

                if (columnName == nameof(Prijs) && Prijs <= 0)
                {
                    return "Prijs moet groter zijn dan 0";
                }

                if (columnName == nameof(BestemmingsId) && BestemmingsId == 0)
                {
                    return "Kies een bestemming";
                }

                if (columnName == nameof(HoofdmonitorId) && HoofdmonitorId == 0)
                {
                    return "Kies een hoofdmonitor";
                }

                if (columnName == nameof(LeeftijdsCategorieId) && LeeftijdsCategorieId == 0)
                {
                    return "kies een leeftijdscategorie";
                }

                if (columnName == nameof(BeginDatum))
                {
                    int datumVergelijking = DateTime.Compare(BeginDatum, EindDatum);
                    if (datumVergelijking >= 0)
                    {
                        return "Begindatum moet voor eindatum liggen";
                    }
                }

                if (columnName == nameof(EindDatum))
                {
                    int datumVergelijking = DateTime.Compare(BeginDatum, EindDatum);
                    if (datumVergelijking >= 0)
                    {
                        return "Einddatum moet voor begindatum liggen";
                    }
                }

                return "";
            }
        }

        public int AantalLeden
        {
            get
            {
                if (Boekingen == null) return 0;
                return Boekingen.Where(b => b.Gebruiker.IsLid && !b.IsMonitor).Count();
            }
        }
        public int AantalNietLeden
        {
            get
            {
                if (Boekingen == null) return 0;
                return Boekingen.Where(b => !b.Gebruiker.IsLid && !b.IsMonitor).Count();
            }
        }
        public decimal Budget
        {
            get
            {
                if (Boekingen == null) return 0;

                List<Gebruiker> leden = Boekingen.Where(b => b.Gebruiker.IsLid && !b.IsMonitor).Select(g => g.Gebruiker).ToList();
                List<Gebruiker> nietLeden = Boekingen.Where(b => !b.Gebruiker.IsLid && !b.IsMonitor).Select(g => g.Gebruiker).ToList();

                decimal totaalBedragLeden = leden.Count * (Prijs * (decimal)0.9);
                decimal totaalBedragNietLeden = nietLeden.Count * Prijs;

                return (totaalBedragLeden + totaalBedragNietLeden) * (decimal)0.05;
            }
        }
    }
}
