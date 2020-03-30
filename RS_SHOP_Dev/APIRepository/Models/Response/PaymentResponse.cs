using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Response
{
    public class PaymentResponse
    {
        public string ORDER_REF_NO { get; set; }
        public string STATUS { get; set; }
        public string Message { get; set; }
    }
}