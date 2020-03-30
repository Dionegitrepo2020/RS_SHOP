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
    public class WishListImpl : IWishlist
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public List<CartItem> GetWishlist(long uid)
        {
            List<TB_ECOMM_CART_ITEM> cartitem = entity.TB_ECOMM_CART_ITEM.ToList();
            List<TB_ECOMM_PRODUCT> product = entity.TB_ECOMM_PRODUCT.ToList();

            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<CartItem> query = from cp in cartitem
                                              join p in product on cp.PRODUCT_ID equals p.PRODUCT_ID
                                              where cp.USER_ID == uid
                                              where cp.WISHLIST == true
                                              select new CartItem
                                              {
                                                  cart = cp,
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

        public ApiResponse addwishlist(TB_ECOMM_CART_ITEM cart)
        {
            try
            {
                TB_ECOMM_CART_ITEM CI = entity.TB_ECOMM_CART_ITEM.Find(cart.CART_ITEM_ID);
                CI.WISHLIST = true;
                int id = this.entity.SaveChanges();
                return new ApiResponse { Status = "Success", Message = "Item Added to Wishlist." };
                
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

        public ApiResponse move(TB_ECOMM_CART_ITEM cart)
        {
            try
            {
                TB_ECOMM_CART_ITEM CI = entity.TB_ECOMM_CART_ITEM.Find(cart.CART_ITEM_ID);
                CI.WISHLIST = false;
                int id = this.entity.SaveChanges();
                return new ApiResponse { Status = "Success", Message = "Item Added to Wishlist." };

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