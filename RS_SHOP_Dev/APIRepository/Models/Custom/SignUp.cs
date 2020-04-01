using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class SignUp
    {
        public long USER_ID { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_PASSWORD { get; set; }
        public string USER_NAME { get; set; }
        public string GENDER { get; set; }
        public Nullable<System.DateTime> DATE_OF_BIRTH { get; set; }
        public string COUNTRY { get; set; }
        public string CITY { get; set; }
    }
}