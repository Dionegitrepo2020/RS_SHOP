using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.OrderModels
{
    public class TBECOMMCATEGORY
    {
        public int CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }

    public class TBECOMMSUBCATEGORY
    {
        public TBECOMMCATEGORY TB_ECOMM_CATEGORY { get; set; }
        public int SUB_CATEGORY_ID { get; set; }
        public int CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }

    public class TBECOMMPRODUCT
    {
        public TBECOMMSUBCATEGORY TB_ECOMM_SUB_CATEGORY { get; set; }
        public int PRODUCT_ID { get; set; }
        public int SUB_CATEGORY_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string PRODUCT_DESCRIPTION { get; set; }
        public object PRODUCT_IMAGE1 { get; set; }
        public object PRODUCT_IMAGE2 { get; set; }
        public object PRODUCT_IMAGE3 { get; set; }
        public object PRODUCT_IMAGE4 { get; set; }
        public double PRODUCT_PRICE { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }

    public class TBECOMMORDERITEM
    {
        public TBECOMMPRODUCT TB_ECOMM_PRODUCT { get; set; }
        public int ORDER_ITEM_ID { get; set; }
        public int ORDER_ID { get; set; }
        public int PRODUCT_ID { get; set; }
        public int PRODUCT_QUANTITY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
        public double PRODUCT_AMOUNT { get; set; }
    }

    public class Orderm
    {
        public object TB_ECOMM_ADDRESS { get; set; }
        public object TB_ECOMM_COUPON { get; set; }
        public object TB_ECOMM_DELIVERY_TYPE { get; set; }
        public object TB_ECOMM_PICKUP_STORES { get; set; }
        public object TB_ECOMM_USERS { get; set; }
        public IList<TBECOMMORDERITEM> TB_ECOMM_ORDER_ITEM { get; set; }
        public int ORDER_ID { get; set; }
        public int USER_ID { get; set; }
        public object ADDRESS_ID { get; set; }
        public object COUPON_ID { get; set; }
        public int PICKUP_STORE_ID { get; set; }
        public object DELIVERY_TYPE_ID { get; set; }
        public double ORDER_AMOUNT { get; set; }
        public object ORDER_STATUS { get; set; }
        public string ORDER_QR_TAG { get; set; }
        public string ORDER_REF_NO { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }

    public class OrderModel
    {
        public Orderm orderm { get; set; }
    }
}
