using APIRepository.Interfaces;
using APIRepository.Models;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace APIRepository.InterfacesImpl
{
    public class DeliveryImpl : IDelivery
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public ApiResponse adddeliverytype(TB_ECOMM_DELIVERY_TYPE delivery)
        {
            try
            {
                TB_ECOMM_DELIVERY_TYPE DT = new TB_ECOMM_DELIVERY_TYPE();
                DT.CREATED_DATE = DateTime.Now;
                DT.DELIVERY_TYPE_COST = delivery.DELIVERY_TYPE_COST;
                DT.DELIVERY_TYPE_NAME = delivery.DELIVERY_TYPE_NAME;
                DT.MODIFIED_DATE = DateTime.Now;
                DT.STORE_ADDRESS1 = delivery.STORE_ADDRESS1;
                DT.STORE_ADDRESS2 = delivery.STORE_ADDRESS2;
                entity.TB_ECOMM_DELIVERY_TYPE.Add(DT);
                entity.SaveChanges();
                return new ApiResponse
                { Status = "Success", Message = "Delivert Type Saved." };
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public object getdeliverytype()
        {
            try
            {
                List<TB_ECOMM_DELIVERY_TYPE> delivery = entity.TB_ECOMM_DELIVERY_TYPE.ToList();
                return delivery;
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