using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Response
{
    public class OrderResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string ORDER_QR_TAG { get; set; }
        public string ORDER_ID { get; set; }
    }
}