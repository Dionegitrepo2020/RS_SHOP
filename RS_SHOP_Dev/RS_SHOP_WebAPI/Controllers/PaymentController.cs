using APIRepository.Interfaces;
using APIRepository.InterfacesImpl;
using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace RS_SHOP_WebAPI.Controllers
{
    //Author : Rakshit
    [RoutePrefix("api/pay")]
    public class PaymentController : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        [Route("add")]
        [HttpPost]
        public async Task<object> addcardAsync(Payvisa visa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string Card = visa.CARD_NUMBER;
                string Expire = visa.EXPIRES;
                string cvv = visa.CVV_NUM;
                string Name = visa.CH_NAME;
                string MerchantID = "globaltmstest";
                string Secret = "secret";
                string OrderRef = visa.ORDER_REF_NO;
                long Userid = visa.USER_ID; 
                //await Task.Delay(10000);
                //TB_ECOMM_ORDER order = await entity.TB_ECOMM_ORDER.FindAsync(OrderID);
                string Amount = Convert.ToInt32(visa.AMOUNT).ToString();
                string Cur = "EUR";
                DateTime DateTime = Convert.ToDateTime(visa.DATE_TIME);
                string TimeStamp = CreateTimeStamp(DateTime).Result.Remove(14);
                string hashFormat = TimeStamp + "." + MerchantID + "." + OrderRef + "." + Amount + "." + Cur + "." + Card;
                string sha1hash = await GenerateSHA1HASH(hashFormat, Secret);


                //Generating XML
                Payment payment = new Payment();
                payment.Type = "auth";
                payment.Timestamp = TimeStamp;
                payment.Merchantid = MerchantID;
                payment.Orderid = OrderRef;

                Amount amount = new Amount();
                amount.Currency = Cur;
                amount.Text = Amount;
                payment.Amount = amount;

                Card card = new Card();
                card.Number = Card;
                card.Expdate = Expire;
                card.Chname = Name;
                card.Type = "VISA";
                payment.Card = card;

                Cvn cvn = new Cvn();
                cvn.CNumber = cvv;
                cvn.Presind = "1";
                payment.Card.Cvn = cvn;


                Autosettle autosettle = new Autosettle();
                autosettle.Flag = "1";
                payment.Autosettle = autosettle;
                payment.Sha1hash = sha1hash;

                string xml = await GetXMLFromObject(payment);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                //Api call for request payment
                var uri = new Uri(string.Format("https://remote.sandbox.addonpayments.com/remote", string.Empty));
                var content = new StringContent(xml, Encoding.UTF8, "application/xml");
                var client = new HttpClient();
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);
                var contents = await response.Content.ReadAsStringAsync();
                XmlSerializer serializer = new XmlSerializer(typeof(Response), new XmlRootAttribute("response"));
                StringReader stringReader = new StringReader(contents);
                Response re = (Response)serializer.Deserialize(stringReader);
                TB_ECOMM_PAYMENT_STATUS_DETAILS PS = new TB_ECOMM_PAYMENT_STATUS_DETAILS();
                if(re.Result=="00")
                {
                    PS.ACCOUNT = re.Account;
                    PS.AUTH_CODE = re.Authcode;
                    PS.AVS_ADDRESS_RESPONSE = re.Avsaddressresponse;
                    PS.BANK_COUNTRY = re.Cardissuer.Country;
                    PS.BANK_NAME = re.Cardissuer.Bank;
                    PS.BATCH_ID = re.Batchid;
                    PS.COUNTRY_CODE = re.Cardissuer.Countrycode;
                    PS.CREATED_DATE = DateTime.Now;
                    PS.CVN_RESULT = re.Cvnresult;
                    PS.MERCHANT_ID = re.Merchantid;
                    PS.MESSAGE = re.Message;
                    PS.MODIFIED_DATE = DateTime.Now;
                    PS.ORDER_ID = re.Orderid;
                    PS.PAYMENT_REF_NUMBER = re.Pasref;
                    PS.REGION = re.Cardissuer.Region;
                    PS.RESULT = re.Result;
                    PS.SHA_HASH = re.Sha1hash;
                    PS.TIMESTAMP = re.Timestamp;
                    PS.USER_ID = Userid;
                    entity.TB_ECOMM_PAYMENT_STATUS_DETAILS.Add(PS);
                    await entity.SaveChangesAsync();
                    TB_ECOMM_ORDER TO =(from o in entity.TB_ECOMM_ORDER where o.ORDER_REF_NO == re.Orderid select o).FirstOrDefault();
                    //TB_ECOMM_ORDER TO = entity.TB_ECOMM_ORDER.Find(Convert.ToInt64(visa.ORDER_REF_NO));
                    TO.ORDER_STATUS = "Paid";
                    await this.entity.SaveChangesAsync();
                    return new PaymentResponse { Message = "Transaction Success", ORDER_REF_NO = OrderRef.ToString(), STATUS=re.Result};

                }
                else if (re.Result == "101")
                {
                    PS.ACCOUNT = re.Account;
                    PS.AUTH_CODE = re.Authcode;
                    PS.AVS_ADDRESS_RESPONSE = re.Avsaddressresponse;
                    PS.BANK_COUNTRY = re.Cardissuer.Country;
                    PS.BANK_NAME = re.Cardissuer.Bank;
                    PS.BATCH_ID = re.Batchid;
                    PS.COUNTRY_CODE = re.Cardissuer.Countrycode;
                    PS.CREATED_DATE = DateTime.Now;
                    PS.CVN_RESULT = re.Cvnresult;
                    PS.MERCHANT_ID = re.Merchantid;
                    PS.MESSAGE = re.Message;
                    PS.MODIFIED_DATE = DateTime.Now;
                    PS.ORDER_ID = re.Orderid;
                    PS.PAYMENT_REF_NUMBER = re.Pasref;
                    PS.REGION = re.Cardissuer.Region;
                    PS.RESULT = re.Result;
                    PS.SHA_HASH = re.Sha1hash;
                    PS.TIMESTAMP = re.Timestamp;
                    PS.USER_ID = Userid;
                    entity.TB_ECOMM_PAYMENT_STATUS_DETAILS.Add(PS);
                    await entity.SaveChangesAsync();
                    TB_ECOMM_ORDER TO = (from o in entity.TB_ECOMM_ORDER where o.ORDER_REF_NO == re.Orderid select o).FirstOrDefault();
                    TO.ORDER_STATUS = "Pay Declined";
                    await this.entity.SaveChangesAsync();
                    return new PaymentResponse { Message = "Transaction Declined", ORDER_REF_NO = OrderRef.ToString(), STATUS=re.Result };

                }
                else {
                    TB_ECOMM_ORDER TO = (from o in entity.TB_ECOMM_ORDER where o.ORDER_REF_NO == re.Orderid select o).FirstOrDefault();
                    TO.ORDER_STATUS = "Pay Failed";
                    await this.entity.SaveChangesAsync();
                    return new PaymentResponse { Message = "Transaction Failed", ORDER_REF_NO = OrderRef.ToString(), STATUS=re.Result }; 
                    //return contents;
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        throw new Exception("Error" + validationError.ErrorMessage);
                        //return new ApiResponse { Status = "Error", Message = validationError.ErrorMessage };
                    }
                    throw new Exception("Error" + entityValidationErrors.ValidationErrors);
                }
                throw new Exception("Error" + ex.Message);
            }
            

        }

        private static async Task<string> CreateTimeStamp(DateTime dateTime)
        {
            string date = dateTime.Date.ToString("yyyy-MM-dd").Replace("-", "");
            string time = dateTime.Date.ToLongTimeString().Replace(":", "");
            return date + time;
        }

        //Generating SHA1HASH
        private static async Task<string> GenerateSHA1HASH(string hashFormat, string secret)
        {
            byte[] resultTemp, result;
            string hash, sha1hash;
            SHA1 sha = new SHA1CryptoServiceProvider();
            resultTemp = sha.ComputeHash(Encoding.UTF8.GetBytes(hashFormat));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < resultTemp.Length; i++)
            {
                sb.Append(resultTemp[i].ToString("x2"));
            }
            hash = sb.ToString();
            result = sha.ComputeHash(Encoding.UTF8.GetBytes(hash + "." + secret));
            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < resultTemp.Length; i++)
            {
                sb1.Append(result[i].ToString("x2"));
            }
            sha1hash = sb1.ToString();
            return sha1hash;
        }

        //Xreating XML Object
        public static async Task<string> GetXMLFromObject(Payment payment)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(payment.GetType());
                tw = new XmlTextWriter(sw);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                serializer.Serialize(tw, payment, ns);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
    }
}
