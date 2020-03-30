using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.ShoppingModel
{
    public partial class TB_ECOMM_CART_ITEM
    {
        public long CART_ITEM_ID { get; set; }
        public Nullable<long> USER_ID { get; set; }
        public Nullable<long> PRODUCT_DETAIL_ID { get; set; }
        public Nullable<long> PRODUCT_ID { get; set; }
        public Nullable<int> CART_ITEM_QUANTITY { get; set; }
        public Nullable<System.DateTime> ADDED_DATE { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public Nullable<long> CATEGORY_ID { get; set; }
        
    }
}
