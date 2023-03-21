using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public class Cursus
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(100, ErrorMessage = "De maximum lengte voor het veld 'naam' is 100 karakters")]
        public string Naam { get; set; }

        // Navigation properties
        public ICollection<GebruikerCursus> GebruikerCursussen { get; set; }
    }
}