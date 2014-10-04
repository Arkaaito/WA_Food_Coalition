using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Donor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool OptIn { get; set; }

        public Double Latitude { get; set; }

        public Double Longitude { get; set; }
    }

    public class DonorDistanceResult : Donor
    {
        public Double Distance { get; set; }
    }
}