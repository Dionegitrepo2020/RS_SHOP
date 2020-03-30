using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.ShoppingModel
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

    public class Product
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
        public decimal PRODUCT_PRICE { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime MODIFIED_DATE { get; set; }
    }

    public class Products
    {
        public Product product { get; set; }
        public object productdetail { get; set; }
        public object category { get; set; }
        public object subcategory { get; set; }
    }

}
