using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models
{
    public class PasswordResetModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ActivationCode { get; set; }
    }
}
