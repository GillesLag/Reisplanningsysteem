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
                if (columnName == nameof(Voornaam) && string.IsNullOrWhiteSpace(Voornaam))
                {
                    return "Voornaam moet ingevuld zijn";
                }
                if (columnName == nameof(Achternaam) && string.IsNullOrWhiteSpace(Achternaam))
                {
                    return "Achternaam moet ingevuld zijn";
                }
                if (columnName == nameof(Email) && string.IsNullOrWhiteSpace(Email))
                {
                    return "Email moet ingevuld zijn";
                }
                if (columnName == nameof(Email) && !string.IsNullOrWhiteSpace(Email))
                {
                    string error1 = string.Empty;
                    string error2 = string.Empty;
                    if (!Email.Contains('@'))
                    {
                        error1 = "Emailadres moet een '@' bevatten.";
                    }

                    if (!Email.Contains('.'))
                    {
                        error2 = "Emailadres moet een '.' bevatten";
                    }

                    return string.Join(Environment.NewLine, error1, error2);
                }
                if (columnName == nameof(Straat) && string.IsNullOrWhiteSpace(Straat))
                {
                    return "Straat moet ingevuld zijn";
                }
                if (columnName == nameof(Huisnummer) && string.IsNullOrWhiteSpace(Huisnummer))
                {
                    return "Huisnummer moet ingevuld zijn";
                }
                if (columnName == nameof(GemeenteId) && GemeenteId == -1)
                {
                    return "Gemeente moet geselecteerd zijn";
                }
                return "";
            }
        }

        public override string ToString()
        {
            return Voornaam + " " + Achternaam;
        }
    }
}