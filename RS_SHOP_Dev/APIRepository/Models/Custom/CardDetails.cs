using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Custom
{
    public class CardDetails
    {
        public long CARD_ID { get; set; }
        [Required]
        public Nullable<long> CARD_NUMBER { get; set; }
        [Required]
        public string CARD_HOLDER_NAME { get; set; }
        [Required]
        public string CARD_EXP_DATE { get; set; }
        public string CARD_TYPE { get; set; }
        public string CARD_CVV { get; set; }
        public string UDF_1 { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<System.DateTime> MODIFIED_DATE { get; set; }
        [Required]
        public Nullable<long> USER_ID { get; set; }
        public Nullable<bool> CARD_DEFAULT { get; set; }

    }
}