using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class Products
    {
        public TB_ECOMM_PRODUCT product { get; set; }
        public TB_ECOMM_PRODUCT_DETAILS productDetail { get; set; }
        public TB_ECOMM_SUB_CATEGORY subcategory { get; set; }
    }
}