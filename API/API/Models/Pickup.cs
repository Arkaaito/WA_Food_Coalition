using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Spatial;

namespace API.Models
{
    public class Pickup
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        public int DonorId { get; set; }

        [Required]
        [ForeignKey("DonorId")]
        public Donor Donor { get; set; }

        [Required]
        public string Items { get; set; }

        public string Notes { get; set; }

        public DateTime? Schedule { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zip { get; set; }

        public Double Latitude { get; set; }

        public Double Longitude { get; set; }

        public StatusTypes Status { get; set; }
    }
}