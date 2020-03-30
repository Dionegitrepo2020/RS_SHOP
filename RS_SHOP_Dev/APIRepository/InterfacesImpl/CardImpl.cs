using APIRepository.Interfaces;
using APIRepository.Models;
using APIRepository.Models.Custom;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace APIRepository.InterfacesImpl
{
    public class CardImpl : Icard
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public CardResponse addcard(CardDetails address)
        {
            try
            {
                TB_ECOMM_CARD_DETAILS AD = new TB_ECOMM_CARD_DETAILS();
                AD.USER_ID = address.USER_ID;
                AD.CARD_CVV = address.CARD_CVV;
                AD.CARD_EXP_DATE = address.CARD_EXP_DATE;
                AD.CARD_HOLDER_NAME = address.CARD_HOLDER_NAME;
                AD.CARD_NUMBER = address.CARD_NUMBER;
                AD.CARD_TYPE = address.CARD_TYPE;
                AD.CARD_DEFAULT = address.CARD_DEFAULT;
                AD.CREATED_DATE = DateTime.Now;
                AD.MODIFIED_DATE = DateTime.Now;
                entity.TB_ECOMM_CARD_DETAILS.Add(AD);
                entity.SaveChanges();
                return new CardResponse
                { Status = "Success", Message = "Card Saved." };
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

        public CardResponse addpaymentdetail(CardDetails address)
        {
            try
            {
                TB_ECOMM_CARD_DETAILS AD = new TB_ECOMM_CARD_DETAILS();
                AD.USER_ID = address.USER_ID;
                AD.CARD_CVV = address.CARD_CVV;
                AD.CARD_EXP_DATE = address.CARD_EXP_DATE;
                AD.CARD_HOLDER_NAME = address.CARD_HOLDER_NAME;
                AD.CARD_NUMBER = address.CARD_NUMBER;
                AD.CARD_TYPE = address.CARD_TYPE;
                AD.CARD_DEFAULT = address.CARD_DEFAULT;
                AD.CREATED_DATE = DateTime.Now;
                AD.MODIFIED_DATE = DateTime.Now;
                entity.TB_ECOMM_CARD_DETAILS.Add(AD);
                entity.SaveChanges();
                return new CardResponse
                { Status = "Success", Message = "Card Saved." };
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

        public object getcard(long cid)
        {
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                return entity.TB_ECOMM_CARD_DETAILS.Find(cid);
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

        public List<Cards> getcarddefault(long uid)
        {
            List<TB_ECOMM_CARD_DETAILS> card = entity.TB_ECOMM_CARD_DETAILS.ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<Cards> query = from c in card
                                           where c.USER_ID == uid
                                           where c.CARD_DEFAULT==true
                                           select new Cards
                                           {
                                               card = c,
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

        public List<TB_ECOMM_CARD_DETAILS> getcards(long uid)
        {
            List<TB_ECOMM_CARD_DETAILS> card = entity.TB_ECOMM_CARD_DETAILS.ToList();
            List<TB_ECOMM_CARD_DETAILS> TO = (from o in entity.TB_ECOMM_CARD_DETAILS where o.USER_ID == uid select o).ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<Cards> query = from c in card
                                              where c.USER_ID == uid
                                              select new Cards
                                              {
                                                  card = c,
                                              };
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

        public CardResponse updatecard(CardDetails card)
        {
            try
            {
                TB_ECOMM_CARD_DETAILS CD = entity.TB_ECOMM_CARD_DETAILS.Find(card.CARD_ID);
                CD.CARD_CVV = card.CARD_CVV;
                CD.CARD_EXP_DATE = card.CARD_EXP_DATE;
                CD.CARD_HOLDER_NAME = card.CARD_HOLDER_NAME;
                CD.CARD_NUMBER = card.CARD_NUMBER;
                CD.CARD_TYPE = card.CARD_TYPE;
                CD.CARD_DEFAULT = card.CARD_DEFAULT;
                CD.MODIFIED_DATE = DateTime.Now;
                this.entity.SaveChanges();
                entity.SaveChanges();
                return new CardResponse
                { Status = "Success", Message = "Card Updated." };
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
        public object deletecard(long CID)
        {
            try
            {
                TB_ECOMM_CARD_DETAILS card = entity.TB_ECOMM_CARD_DETAILS.Find(CID);
                entity.TB_ECOMM_CARD_DETAILS.Remove(card);


                int id = this.entity.SaveChanges();
                return CID + "-Removed";
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

    }
}