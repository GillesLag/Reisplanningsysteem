using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public class LeeftijdsCategorie
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "De maximum lengte voor het veld 'naam' is 50 karakters")]
        public string Naam { get; set; }

        [Required]
        public int LeeftijdMinimum { get; set; }

        [Required]
        public int LeeftijdMaximum { get; set; }

        // Navigation properties
        public ICollection<Reis> Reizen { get; set; }
    }
}