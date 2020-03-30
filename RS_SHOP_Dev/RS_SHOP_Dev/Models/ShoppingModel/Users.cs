using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.ShoppingModel
{
    public class Users
    {
        public int USER_ID { get; set; }
        public string USER_EMAIL { get; set; }
        public object USER_PASSWORD { get; set; }
        public string USER_NAME { get; set; }
        public string GENDER { get; set; }
        public Nullable<DateTime> DATE_OF_BIRTH { get; set; }

        public string COUNTRY { get; set; }
        public string CITY { get; set; }

        public string USER_PROFILE_IMAGE { get; set; }
    }
}
