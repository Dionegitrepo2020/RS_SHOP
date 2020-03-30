using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class Order
    {
        public long ORDER_ID { get; set; }
        public Nullable<long> USER_ID { get; set; }
        public Nullable<long> ADDRESS_ID { get; set; }
        public Nullable<long> COUPON_ID { get; set; }
        public Nullable<long> PICKUP_STORE_ID { get; set; }
        public Nullable<long> DELIVERY_TYPE_ID { get; set; }
        public Nullable<decimal> ORDER_AMOUNT { get; set; }
        public string ORDER_STATUS { get; set; }
        public string ORDER_QR_TAG { get; set; }
        public string CATEGORY_ID { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public List<TB_ECOMM_ORDER_ITEM> order_item { get; set; }
        public long ORDER_ITEM_ID { get; set; }
        public Nullable<long> PRODUCT_ID { get; set; }
        public Nullable<long> PRODUCT_QUANTITY { get; set; }
    }
}