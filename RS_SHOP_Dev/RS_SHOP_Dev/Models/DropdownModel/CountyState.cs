using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.DropdownModel
{
    public class CountyState
    {
        public class Country
        {
            public int COUNTRY_ID { get; set; }
            public string COUNTRY_NAME { get; set; }
            public DateTime CREATED_DATE { get; set; }
            public DateTime MODIFIED_DATE { get; set; }
        }

        public class City
        {
            public int CITY_ID { get; set; }
            public int COUNTRY_ID { get; set; }
            public string CITY_NAME { get; set; }
            public DateTime CREATED_DATE { get; set; }
            public DateTime MODIFIED_DATE { get; set; }

        }


        public class Root
        {

            public Country country { get; set; }
            public City city { get; set; }


        }
    }
}
