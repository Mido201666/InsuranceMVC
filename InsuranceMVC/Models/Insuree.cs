using System;
using System.ComponentModel.DataAnnotations;

namespace InsuranceMVC.Models
{
    public class Insuree
    {
        public int InsureeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int CarYear { get; set; }

        [Required]
        public string CarMake { get; set; }

        [Required]
        public string CarModel { get; set; }

        public bool DUI { get; set; }

        public int SpeedingTickets { get; set; }

        public bool FullCoverage { get; set; }

        public decimal Quote { get; set; }
    }
}