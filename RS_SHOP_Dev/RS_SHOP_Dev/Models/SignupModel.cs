using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RS_SHOP_Dev.Models
{
    public class SignupModel : INotifyPropertyChanged
    {
        [Required, MaxLength(40)]
        public string FullName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Wrong email format")]
        public string UserEmail { get; set; }


        [Required, MinLength(5, ErrorMessage = "Enter atleast 5 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password do not match")]
        public string ConfirmPassword { get; set; }

        public Nullable<DateTime> DOB { get; set; }

       // public string DateOfBirth { get; set; }

        public string ConditionId { get; set; }

        
        public string ParentName { get; set; }

       
        public string ParentID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
