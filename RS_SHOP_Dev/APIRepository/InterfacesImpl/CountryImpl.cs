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
    public class CountryImpl: ICountry
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public List<Country> Getcountry()
        {
            List<TB_ECOMM_COUNTRY> country = entity.TB_ECOMM_COUNTRY.ToList();
            List<TB_ECOMM_CITY> city = entity.TB_ECOMM_CITY.ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<Country> query = from c in country
                                              select new Country
                                              {
                                                  country = c,
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
                    }
                    throw new Exception("Error" + entityValidationErrors.ValidationErrors);
                }
                throw new Exception("Error" + ex.Message);
            }
        }

        public List<Country> Getcountry(long cid)
        {
            List<TB_ECOMM_COUNTRY> country = entity.TB_ECOMM_COUNTRY.ToList();
            List<TB_ECOMM_CITY> city = entity.TB_ECOMM_CITY.ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<Country> query = from c in country
                                             join cc in city on c.COUNTRY_ID equals cc.COUNTRY_ID
                                             where cc.COUNTRY_ID == cid
                                             select new Country
                                             {
                                                 city = cc,
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
                    }
                    throw new Exception("Error" + entityValidationErrors.ValidationErrors);
                }
                throw new Exception("Error" + ex.Message);
            }
        }

    }
}