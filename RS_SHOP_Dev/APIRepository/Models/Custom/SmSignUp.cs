using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class SmSignUp
    {
        public long USER_SID { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_NAME { get; set; }
        public string GENDER { get; set; }
    }
}