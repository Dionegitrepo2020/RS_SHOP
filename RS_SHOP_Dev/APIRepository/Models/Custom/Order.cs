using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class Order
    {
        public TB_ECOMM_ORDER order { get; set; }
        public TB_ECOMM_ORDER_ITEM orderitem { get; set; }
    }
}