﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GiveFoodNow.Models {
    public class Member 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContactFirstName { get; set; }

        [Required]
        public string ContactLastName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zip { get; set; }
        
        [Required]
        public int RangeInMeters { get; set; }

        [Required]
        public string Phone { get; set; }

        public Double Latitude { get; set; }

        public Double Longitude { get; set; }

        [Required]
        public string EIN { get; set; }
    }
}