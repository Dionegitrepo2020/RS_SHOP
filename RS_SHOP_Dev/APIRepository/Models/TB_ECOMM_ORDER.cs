//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIRepository.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_ECOMM_ORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_ECOMM_ORDER()
        {
            this.TB_ECOMM_ORDER_ITEM = new HashSet<TB_ECOMM_ORDER_ITEM>();
        }
    
        public long ORDER_ID { get; set; }
        public Nullable<long> USER_ID { get; set; }
        public Nullable<long> ADDRESS_ID { get; set; }
        public Nullable<long> COUPON_ID { get; set; }
        public Nullable<long> PICKUP_STORE_ID { get; set; }
        public Nullable<long> DELIVERY_TYPE_ID { get; set; }
        public Nullable<decimal> ORDER_AMOUNT { get; set; }
        public string ORDER_STATUS { get; set; }
        public string ORDER_QR_TAG { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        public string ORDER_REF_NO { get; set; }
    
        public virtual TB_ECOMM_ADDRESS TB_ECOMM_ADDRESS { get; set; }
        public virtual TB_ECOMM_COUPON TB_ECOMM_COUPON { get; set; }
        public virtual TB_ECOMM_DELIVERY_TYPE TB_ECOMM_DELIVERY_TYPE { get; set; }
        public virtual TB_ECOMM_PICKUP_STORES TB_ECOMM_PICKUP_STORES { get; set; }
        public virtual TB_ECOMM_USERS TB_ECOMM_USERS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_ECOMM_ORDER_ITEM> TB_ECOMM_ORDER_ITEM { get; set; }
    }
}
