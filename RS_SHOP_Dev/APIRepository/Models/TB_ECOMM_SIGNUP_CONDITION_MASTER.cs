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
    
    public partial class TB_ECOMM_SIGNUP_CONDITION_MASTER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_ECOMM_SIGNUP_CONDITION_MASTER()
        {
            this.TB_ECOMM_USERS = new HashSet<TB_ECOMM_USERS>();
        }
    
        public long CONDITION_ID { get; set; }
        public string CONDITION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_ECOMM_USERS> TB_ECOMM_USERS { get; set; }
    }
}