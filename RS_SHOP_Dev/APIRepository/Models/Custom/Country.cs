using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class Country
    {
        public TB_ECOMM_COUNTRY country { get; set; }
        public TB_ECOMM_CITY city { get; set; }
    }
}