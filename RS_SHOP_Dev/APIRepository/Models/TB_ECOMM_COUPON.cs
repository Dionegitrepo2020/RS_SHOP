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
    
    public partial class TB_ECOMM_COUPON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_ECOMM_COUPON()
        {
            this.TB_ECOMM_ORDER = new HashSet<TB_ECOMM_ORDER>();
        }
    
        public long COUPON_ID { get; set; }
        public string COUPON_NAME { get; set; }
        public string COUPON_TYPE { get; set; }
        public Nullable<decimal> COUPON_DISCOUNT { get; set; }
        public Nullable<bool> COUPON_VALID { get; set; }
        public Nullable<long> COUPON_QUANTITY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<System.DateTime> EXPIRE_DATE { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_ECOMM_ORDER> TB_ECOMM_ORDER { get; set; }
    }
}