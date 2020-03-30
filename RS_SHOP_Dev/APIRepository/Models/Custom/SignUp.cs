using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class SignUp
    {
        public long USER_ID { get; set; }
        [Required, EmailAddress(ErrorMessage ="Enter Vallid email address")]
        public string USER_EMAIL { get; set; }
        public string USER_PASSWORD { get; set; }
        [Required,RegularExpression("^[0-9a-zA-Z\\s]+$",ErrorMessage = "Enter Alphanumeric values, Special Characters not allowed!")]
        public string USER_NAME { get; set; }
        public string GENDER { get; set; }
        public Nullable<System.DateTime> DATE_OF_BIRTH { get; set; }
        public string COUNTRY { get; set; }
        public string CITY { get; set; }
        public long CONDITION_ID { get; set; } 
        public string PARENT_NAME { get; set; }
        public string PARENT_ID { get; set; }
        public string USER_PROFILE_IMAGE { get; set; }

    }
}