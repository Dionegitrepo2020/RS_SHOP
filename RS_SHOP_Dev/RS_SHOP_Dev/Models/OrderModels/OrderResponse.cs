using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.OrderModels
{
    public class ORDERITEM
    {
        public int ORDER_ITEM_ID { get; set; }
        public int ORDER_ID { get; set; }
        public int PRODUCT_ID { get; set; }
        public int PRODUCT_QUANTITY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public decimal PRODUCT_AMOUNT { get; set; }
        public object TB_ECOMM_PRODUCT { get; set; }
    }

    public class OrderResponse
    {
        public int ORDER_ID { get; set; }
        public object ORDER_REF_NO { get; set; }
        public int USER_ID { get; set; }
        public object ADDRESS_ID { get; set; }
        public object COUPON_ID { get; set; }
        public int PICKUP_STORE_ID { get; set; }
        public object DELIVERY_TYPE_ID { get; set; }
        public decimal ORDER_AMOUNT { get; set; }
        public string ORDER_STATUS { get; set; }
        public string ORDER_QR_TAG { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public object TB_ECOMM_ADDRESS { get; set; }
        public object TB_ECOMM_COUPON { get; set; }
        public object TB_ECOMM_DELIVERY_TYPE { get; set; }
        public object TB_ECOMM_PICKUP_STORES { get; set; }
        public object TB_ECOMM_USERS { get; set; }
        public IList<ORDERITEM> TB_ECOMM_ORDER_ITEM { get; set; }
    }
}
