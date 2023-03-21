using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public class GebruikerCursus
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public int GebruikerId { get; set; }

        [Required]
        public int CursusId { get; set; }

        // Navigation properties
        public Gebruiker Gebruiker { get; set; }

        public Cursus Cursus { get; set; }
    }
}