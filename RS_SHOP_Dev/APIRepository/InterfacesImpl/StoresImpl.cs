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
    public class StoresImpl : IStores
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public ApiResponse addstore(TB_ECOMM_PICKUP_STORES store)
        {
            try
            {
                TB_ECOMM_PICKUP_STORES PS = new TB_ECOMM_PICKUP_STORES();
                PS.CREATED_DATE = DateTime.Now;
                PS.MODIFIED_DATE = DateTime.Now;
                PS.PICKUP_STORE_ADDRESS1 = store.PICKUP_STORE_ADDRESS1;
                PS.PICKUP_STORE_ADDRESS2 = store.PICKUP_STORE_ADDRESS2;
                PS.PICKUP_STORE_NAME = store.PICKUP_STORE_NAME;
                entity.TB_ECOMM_PICKUP_STORES.Add(PS);
                entity.SaveChanges();
                return new ApiResponse
                { Status = "Success", Message = "Store Saved." };
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public object Getstores()
        {
            try
            {
                List<TB_ECOMM_PICKUP_STORES> stores = entity.TB_ECOMM_PICKUP_STORES.ToList();
                return stores;
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