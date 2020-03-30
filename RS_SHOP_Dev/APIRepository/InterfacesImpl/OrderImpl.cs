using APIRepository.Interfaces;
using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace APIRepository.InterfacesImpl
{
    public class OrderImpl : IOrder
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public IEnumerable<TB_ECOMM_ORDER> addorder(Order order)
        {
            try
            {
                TB_ECOMM_ORDER OR = new TB_ECOMM_ORDER();
                OR.USER_ID = order.USER_ID;
                OR.ADDRESS_ID = order.ADDRESS_ID;
                OR.COUPON_ID = order.COUPON_ID;
                OR.CREATED_DATE = DateTime.Now;
                OR.DELIVERY_TYPE_ID = order.DELIVERY_TYPE_ID;
                OR.MODIFIED_DATE = DateTime.Now;
                OR.ORDER_AMOUNT = order.ORDER_AMOUNT;
                OR.ORDER_QR_TAG = order.ORDER_QR_TAG;
                OR.ORDER_STATUS = order.ORDER_STATUS;
                OR.PICKUP_STORE_ID = order.PICKUP_STORE_ID;
                
                entity.TB_ECOMM_ORDER.Add(OR);
                entity.SaveChanges();

                TB_ECOMM_ORDER_ITEM OI = new TB_ECOMM_ORDER_ITEM();
                foreach (var PRODUCT_ID in order.order_item)
                {
                    OI.ORDER_ID = OR.ORDER_ID;
                    OI.CREATED_DATE = DateTime.Now;
                    OI.MODIFIED_DATE = DateTime.Now;
                    OI.PRODUCT_ID = PRODUCT_ID.PRODUCT_ID;
                    OI.PRODUCT_QUANTITY = PRODUCT_ID.PRODUCT_QUANTITY;
                    OI.PRODUCT_AMOUNT = PRODUCT_ID.PRODUCT_AMOUNT;
                    entity.TB_ECOMM_ORDER_ITEM.Add(OI);
                    entity.SaveChanges();
                }
                TB_ECOMM_ORDER ord = entity.TB_ECOMM_ORDER.Find(OR.ORDER_ID);
                OR.ORDER_QR_TAG = "abxd" + OR.ORDER_ID;
                if (order.CATEGORY_ID == "10")
                {
                    OR.ORDER_REF_NO = "FD" + OR.ORDER_ID;
                }
                else if(order.CATEGORY_ID == "11")
                {
                    OR.ORDER_REF_NO = "ME" + OR.ORDER_ID;
                }
                this.entity.SaveChanges();
                return entity.TB_ECOMM_ORDER.Where(o => o.ORDER_ID == ord.ORDER_ID);
                //return new OrderResponse
                //{ Status = "Success",  Message = "Order Saved." };
                //{ Status = "Success", ORDER_QR_TAG = OR.ORDER_QR_TAG.ToString(), ORDER_ID = OR.ORDER_ID.ToString(), Message = "Order Saved."  };
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

        public object Getorders(long uid)
        {
            try
            {
                List <TB_ECOMM_ORDER> order = entity.TB_ECOMM_ORDER.ToList();
                List<TB_ECOMM_ORDER_ITEM> orderdetail = entity.TB_ECOMM_ORDER_ITEM.ToList();
                List<TB_ECOMM_PRODUCT> prod = entity.TB_ECOMM_PRODUCT.ToList();
                List<TB_ECOMM_SUB_CATEGORY> subcat = entity.TB_ECOMM_SUB_CATEGORY.ToList();
                List<TB_ECOMM_CATEGORY> cat = entity.TB_ECOMM_CATEGORY.ToList();
            
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<OrderList> query = from o in order
                                               join oi in orderdetail on o.ORDER_ID equals oi.ORDER_ID
                                               join c in cat on oi.TB_ECOMM_PRODUCT.TB_ECOMM_SUB_CATEGORY.TB_ECOMM_CATEGORY.CATEGORY_ID equals c.CATEGORY_ID
                                               where o.USER_ID == uid
                                               select new OrderList
                                               {
                                                   orderm = o,
                                              };
                return query.ToList();
                //return order;
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

        public object Getorder(long oid)
        {
            try
            {
                List<TB_ECOMM_ORDER> order = entity.TB_ECOMM_ORDER.ToList();
                List<TB_ECOMM_ORDER_ITEM> orderdetail = entity.TB_ECOMM_ORDER_ITEM.ToList();
                List<TB_ECOMM_USERS> userdetai = entity.TB_ECOMM_USERS.ToList();

                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<OrderList> query = from o in order
                                               join oi in orderdetail on o.ORDER_ID equals oi.ORDER_ID
                                               join ui in userdetai on o.USER_ID equals ui.USER_ID
                                               where o.ORDER_ID == oid
                                               select new OrderList
                                               {
                                                   orderm = o,
                                               };
                return query.Distinct().ToList();
                //return order;
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
    }
}