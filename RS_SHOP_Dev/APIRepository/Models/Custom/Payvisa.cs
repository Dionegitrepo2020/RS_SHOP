using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class Payvisa
    {
        public string ORDER_ID { get; set; }
        public string ORDER_REF_NO { get; set; }
        public string CARD_NUMBER { get; set; }
        public string EXPIRES { get; set; }
        public string CVV_NUM { get; set; }
        public string CH_NAME { get; set; }
        public string AMOUNT { get; set; }
        public string DATE_TIME { get; set; }
        public long USER_ID { get; set; }
    }
}