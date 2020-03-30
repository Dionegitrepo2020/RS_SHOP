using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.PaymentModel
{
    public class PaymentListModel
    {
        public object TB_ECOMM_USERS { get; set; }
        public int CARD_ID { get; set; }
        public object CARD_NUMBER { get; set; }
        public string CARD_HOLDER_NAME { get; set; }
        public string CARD_EXP_DATE { get; set; }
        public string CARD_TYPE { get; set; }
        public string CARD_CVV { get; set; }
        public object UDF_1 { get; set; }
        public object CREATED_DATE { get; set; }
        public object MODIFIED_DATE { get; set; }
        public int USER_ID { get; set; }
        public object CARD_DEFAULT { get; set; }
    }
}
