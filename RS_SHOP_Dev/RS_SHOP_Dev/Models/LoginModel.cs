using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RS_SHOP_Dev.Models
{
    public class LoginModel
    {


        [Required, EmailAddress(ErrorMessage = "Wrong email format")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
