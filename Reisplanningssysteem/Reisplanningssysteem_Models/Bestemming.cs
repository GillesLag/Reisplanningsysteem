using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public class Bestemming
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(255, ErrorMessage = "De maximum lengte voor het veld 'naam' is 255 karakters")]
        public string Naam { get; set; }

        [Required, MaxLength(50, ErrorMessage = "De maximum lengte voor het veld 'land' is 50 karakters")]
        public string Land { get; set; }

        [Required]
        public string Straat { get; set; }

        [Required, MaxLength(10, ErrorMessage = "De maximum lengte voor het veld 'land' is 10 karakters")]
        public string Huisnummer { get; set; }

        [Required]
        public int GemeenteId { get; set; }

        // Navigation properties
        public Gemeente Gemeente { get; set; }
        public ICollection<Reis> Reizen { get; set; }
    }
}