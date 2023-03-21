using System;
using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public class Boeking
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public bool IsMonitor { get; set; }

        [Required, MaxLength(50, ErrorMessage = "De maximum lengte voor het veld 'huisnummer' is 50 karakters")]
        public string Status { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime InschrijvingsDatum { get; set; }

        public int? GebruikerId { get; set; }

        [Required]
        public int ReisId { get; set; }

        // Navigation properties
        public Gebruiker? Gebruiker { get; set; }

        public Reis Reis { get; set; }
    }
}