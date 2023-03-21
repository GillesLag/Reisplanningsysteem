using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public class Thema
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "De maximum lengte voor het veld 'naam' is 50 karakters")]
        public string Naam { get; set; }

        // Navigation properties
        public ICollection<Reis> Reizen { get; set; }
    }
}