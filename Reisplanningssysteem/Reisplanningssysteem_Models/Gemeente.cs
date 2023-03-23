using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Reisplanningssysteem_Models
{
    public class Gemeente
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "De maximum lengte voor het veld 'naam' is 50 karakters")]
        public string Naam { get; set; }

        [Required, MaxLength(20, ErrorMessage = "De maximum lengte voor het veld 'postcode' is 20 karakters")]
        public string Postcode { get; set; }

        // Navigation properties
        public ICollection<Gebruiker> Gebruikers { get; set; }
        public ICollection<Bestemming> Bestemmingen { get; set; }
    }
}