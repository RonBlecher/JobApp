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
        [StringLength(30, ErrorMessage = "{0} length can't be more than {1} characters long.")]
        [RegularExpression("^[A-Za-z '-]+$", ErrorMessage = "{0} is not valid.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^05\d-?\d{7}$", ErrorMessage = "{0} is not valid.")]
        public string PhoneNum { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
