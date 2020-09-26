using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobApp.Models
{
    public abstract class User
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(15, ErrorMessage = "Name length can't be more than 15.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNum { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
