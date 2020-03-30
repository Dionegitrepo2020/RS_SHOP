using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.CartModels
{
    public class TBECOMMPRODUCT
    {
        public object TB_ECOMM_SUB_CATEGORY { get; set; }
        public IList<object> TB_ECOMM_CART_ITEM { get; set; }
        public int PRODUCT_ID { get; set; }
        public int SUB_CATEGORY_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PRODUCT_DESCRIPTION { get; set; }
        public object PRODUCT_IMAGE1 { get; set; }
        public object PRODUCT_IMAGE2 { get; set; }
        public object PRODUCT_IMAGE3 { get; set; }
        public object PRODUCT_IMAGE4 { get; set; }
        public decimal PRODUCT_PRICE { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }

    public class Cart
    {
        public object TB_ECOMM_PRODUCT_DETAILS { get; set; }
        public object TB_ECOMM_USERS { get; set; }
        public TBECOMMPRODUCT TB_ECOMM_PRODUCT { get; set; }
        public int CART_ITEM_ID { get; set; }
        public int USER_ID { get; set; }
        public object PRODUCT_DETAIL_ID { get; set; }
        public int CART_ITEM_QUANTITY { get; set; }
        public DateTime ADDED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public int PRODUCT_ID { get; set; }
        public decimal CART_PRICE { get; set; }
    }

    public class CartProducts
    {
        public Cart cart { get; set; }
    }
}
