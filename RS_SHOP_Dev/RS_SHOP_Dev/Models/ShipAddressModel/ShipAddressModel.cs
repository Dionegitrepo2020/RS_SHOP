using System;
using System.Collections.Generic;
using System.Text;

namespace RS_SHOP_Dev.Models.ShipAddressModel
{
   public class ShipAddressModel
    {

		//[{"TB_ECOMM_USERS":null,"TB_ECOMM_ORDER":[],"ADDRESS_ID":11,"USER_ID":10115,"FULL_NAME":"asdfg",
		//"ADDRESS1":"vijay","ADDRESS2":"11","COUNTRY":"india","POST_CODE":"560000","CITY":"bangalore",
		//"PHONE":"123456789","IS_DEFAULT":false,"CREATED_DATE":"2020-03-12T15:09:28.54","MODIFIED_DATE":"2020-03-12T15:09:28.54"}]


		public object TB_ECOMM_USERS { get; set; }
		public int CARD_ID { get; set; }
		public int ADDRESS_ID { get; set; }
		public int USER_ID { get; set; }
		public string FULL_NAME { get; set; }
		public string ADDRESS1 { get; set; }

		public string ADDRESS2 { get; set; }
		public string COUNTRY { get; set; }
		public string POST_CODE { get; set; }
		public string CITY { get; set; }

		public string PHONE { get; set; }

		public bool IS_DEFAULT { get; set; }


	}
}
