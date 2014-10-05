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
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool OptIn { get; set; }
    }
}