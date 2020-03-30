using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class ForgotPass
    {
        [Required, EmailAddress(ErrorMessage = "Enter Vallid email address")]
        public string Email { get; set; }
    }
}