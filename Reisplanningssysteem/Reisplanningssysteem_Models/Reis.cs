using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reisplanningssysteem_Models
{
    public class Reis
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(100, ErrorMessage = "De maximum lengte voor het veld 'naam' is 100 karakters")]
        public string Naam { get; set; }

        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; }

        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }

        [Required]
        public decimal Prijs { get; set; }

        public int? ThemaId { get; set; }

        [Required]
        public int BestemmingsId { get; set; }

        [Required]
        public int HoofdmonitorId { get; set; }

        [Required]
        public int LeeftijdsCategorieId { get; set; }

        // Navigation properties
        public Thema? Thema { get; set; }

        public Bestemming Bestemming { get; set; }

        public Gebruiker Hoofdmonitor { get; set; }

        public LeeftijdsCategorie LeeftijdsCategorie { get; set; }

        public ICollection<Boeking> Boekingen { get; set; }

        public ICollection<Onkost> Onkosten { get; set; }
    }
}