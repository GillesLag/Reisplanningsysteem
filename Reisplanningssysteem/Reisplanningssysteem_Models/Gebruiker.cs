using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public partial class Gebruiker
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "De maximum lengte voor het veld 'voornaam' is 50 karakters")]
        public string Voornaam { get; set; }

        [Required, MaxLength(50, ErrorMessage = "De maximum lengte voor het veld 'achteernaam' is 50 karakters")]
        public string Achternaam { get; set; }

        [Required, MaxLength(100, ErrorMessage = "De maximum lengte voor het veld 'email' is 100 karakters")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = "De maximum lengte voor het veld 'telefoonNr' is 20 karakters")]
        public string? TelefoonNr { get; set; }

        [Required]
        public string Straat { get; set; }

        [Required, MaxLength(10, ErrorMessage = "De maximum lengte voor het veld 'huisnummer' is 10 karakters")]
        public string Huisnummer { get; set; }

        [Required]
        public bool BasisCursus { get; set; }

        [Required]
        public bool HoofmonitorCursus { get; set; }

        [Required]
        public bool IsLid { get; set; }

        [Required]
        public int GemeenteId { get; set; }

        public string MedischeGegevens { get; set; }

        // Navigation properties
        public Gemeente Gemeente { get; set; }

        public ICollection<GebruikerCursus> GebruikerCursussen { get; set; }

        public ICollection<Reis> Reizen { get; set; }

        public ICollection<Boeking> Boekingen { get; set; }
    }
}
