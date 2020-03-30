using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RS_SHOP_Dev.Models
{
    public class ProfileModel
    {
        [Required]
        public string Photo { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FirstLastName { get { return FirstName + " " + LastName; } }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Date { get; set; }
    }
}
