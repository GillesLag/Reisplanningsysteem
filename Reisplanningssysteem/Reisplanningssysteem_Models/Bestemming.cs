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

        // Navigation properties
        public ICollection<Reis> Reizen { get; set; }
    }
}