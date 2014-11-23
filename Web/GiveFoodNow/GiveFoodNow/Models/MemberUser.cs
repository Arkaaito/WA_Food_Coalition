using System;
using System.ComponentModel.DataAnnotations;

namespace GiveFoodNow.Models
{
    public class MemberUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string OrgName { get; set; }
    }
}