using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public class Onkost
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public decimal Bedrag { get; set; }

        [Required, MaxLength(255, ErrorMessage = "De maximum lengte voor het veld 'omschrijving' is 255 karakters")]
        public string Omschrijving { get; set; }

        [Required]
        public int ReisId { get; set; }

        // Navigation properties
        public Reis Reis { get; set; }
    }
}