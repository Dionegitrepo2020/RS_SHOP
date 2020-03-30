using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class Address
    {
        public long ADDRESS_ID { get; set; }
        [Required]
        public Nullable<long> USER_ID { get; set; }
        [Required]
        public string FULL_NAME { get; set; }
        [Required]
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        [Required]
        public string COUNTRY { get; set; }
        [Required]
        public string POST_CODE { get; set; }
        [Required]
        public string CITY { get; set; }
        [Required]
        public string PHONE { get; set; }
        public Nullable<bool> IS_DEFAULT { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
    }
}