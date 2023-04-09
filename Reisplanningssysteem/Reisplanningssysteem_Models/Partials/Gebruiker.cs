using Reisplanningssysteem_Models.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reisplanningssysteem_Models
{
    public partial class Gebruiker: BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Voornaam" && string.IsNullOrWhiteSpace(Voornaam))
                {
                    return "Voornaam moet ingevuld zijn";
                }
                if (columnName == "Achternaam" && string.IsNullOrWhiteSpace(Achternaam))
                {
                    return "Achternaam moet ingevuld zijn";
                }
                if (columnName == "Email" && string.IsNullOrWhiteSpace(Email))
                {
                    return "Email moet ingevuld zijn";
                }
                if (columnName == "Straat" && string.IsNullOrWhiteSpace(Straat))
                {
                    return "Straat moet ingevuld zijn";
                }
                if (columnName == "Huisnummer" && string.IsNullOrWhiteSpace(Huisnummer))
                {
                    return "Huisnummer moet ingevuld zijn";
                }
                if (columnName == "GemeenteId" && GemeenteId==null)
                {
                    return "Gemeente moet geselecteerd zijn";
                }
                return "";
            }
        }
    }
}