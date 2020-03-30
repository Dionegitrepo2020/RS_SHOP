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
    public class SAddressImpl : ISAddress
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public AddressResponse addaddress(Address addres)
        {
            try
            {
                TB_ECOMM_ADDRESS AD = new TB_ECOMM_ADDRESS();
                AD.USER_ID = addres.USER_ID;
                AD.FULL_NAME = addres.FULL_NAME;
                AD.ADDRESS1 = addres.ADDRESS1;
                AD.ADDRESS2 = addres.ADDRESS2;
                AD.COUNTRY = addres.COUNTRY;
                AD.CITY = addres.CITY;
                AD.PHONE = addres.PHONE;
                AD.IS_DEFAULT = addres.IS_DEFAULT;
                AD.POST_CODE = addres.POST_CODE;
                AD.CREATED_DATE = DateTime.Now;
                AD.MODIFIED_DATE = DateTime.Now;
                entity.TB_ECOMM_ADDRESS.Add(AD);
                entity.SaveChanges();
                return new AddressResponse
                { Status = "Success", Message = "Address Saved." };
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public List<TB_ECOMM_ADDRESS> GetSAddress(long uid)
        {
            List<TB_ECOMM_ADDRESS> card = entity.TB_ECOMM_ADDRESS.ToList();
            List<TB_ECOMM_ADDRESS> TO = (from o in entity.TB_ECOMM_ADDRESS where o.USER_ID == uid select o).ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                return TO.ToList();
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

        public object getaddress(long aid)
        {
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                return entity.TB_ECOMM_ADDRESS.Find(aid);
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

        public List<Addresslist> getaddressdefault(long uid)
        {
            List<TB_ECOMM_ADDRESS> add = entity.TB_ECOMM_ADDRESS.ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<Addresslist> query = from c in add
                                           where c.USER_ID == uid
                                           where c.IS_DEFAULT == true
                                           select new Addresslist
                                           {
                                               addr = c,
                                           };
                return query.ToList();

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

        public object deleteaddress(long AID)
        {
            try
            {
                TB_ECOMM_ADDRESS add = entity.TB_ECOMM_ADDRESS.Find(AID);
                entity.TB_ECOMM_ADDRESS.Remove(add);


                int id = this.entity.SaveChanges();
                return AID + "-Removed";
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }
    }
}