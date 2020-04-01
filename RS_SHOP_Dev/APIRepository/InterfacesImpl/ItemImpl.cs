using APIRepository.Interfaces;
using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIRepository.InterfacesImpl
{
    //Aouthor Rakshit
    public class ItemImpl : IItem
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public List<Products> GetItems(long categ, long subcateg, decimal pfrom, decimal pto)
        {
            List<TB_ECOMM_PRODUCT> products = entity.TB_ECOMM_PRODUCT.ToList();
            List<TB_ECOMM_PRODUCT_DETAILS> productDetails = entity.TB_ECOMM_PRODUCT_DETAILS.ToList();
            List<TB_ECOMM_CATEGORY> categories = entity.TB_ECOMM_CATEGORY.ToList();
            List<TB_ECOMM_SUB_CATEGORY> subcategories = entity.TB_ECOMM_SUB_CATEGORY.ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<Products> query = from p in products
                                              join sc in subcategories on p.SUB_CATEGORY_ID equals sc.SUB_CATEGORY_ID
                                              where sc.CATEGORY_ID == categ
                                              select new Products
                                              {
                                                  product = p,
                                              };
                if (!subcateg.Equals(0))
                {
                    query = query.Where(sub => sub.product.TB_ECOMM_SUB_CATEGORY.SUB_CATEGORY_ID == subcateg);
                }
                if (!(pfrom.Equals(1) && pto.Equals(0)))
                {
                    query = query.Where(price => price.product.PRODUCT_PRICE >= pfrom && price.product.PRODUCT_PRICE <= pto);
                }
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

        public List<Products> GetItemsdetail(long pid)
        {
            List<TB_ECOMM_PRODUCT> products = entity.TB_ECOMM_PRODUCT.ToList();
            List<TB_ECOMM_PRODUCT_DETAILS> productDetails = entity.TB_ECOMM_PRODUCT_DETAILS.ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<Products> query = from p in products
                                              join pd in productDetails on p.PRODUCT_ID equals pd.PRODUCT_ID
                                              where pd.PRODUCT_ID == pid
                                              select new Products
                                              {
                                                  productDetail = pd,
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

        public object Getcart(long cid)
        {
            List<TB_ECOMM_CART_ITEM> cartitem = entity.TB_ECOMM_CART_ITEM.ToList();
            List<TB_ECOMM_PRODUCT> product = entity.TB_ECOMM_PRODUCT.ToList();
            try
            {
                //entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<CartItem> query = from cp in cartitem
                                              join p in product on cp.PRODUCT_ID equals p.PRODUCT_ID
                                              where cp.USER_ID == cid
                                              select new CartItem
                                              {
                                                  cart = cp,
                                              };
                return query;
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
        public List<Searchitem> SearchItem(string param)
        {
            try
            {
                List<Searchitem> ud = new List<Searchitem>();
                string sConnString = "Data Source=RAKSHIT;Persist Security Info=False;" +
                    "Initial Catalog=ECOMM_DEV;User Id=sa;Password=sa@123;Connect Timeout=30;";

                SqlConnection myConn = new SqlConnection(sConnString);

                SqlCommand objComm = new SqlCommand("SELECT PRODUCT_ID,PRODUCT_NAME FROM dbo.TB_ECOMM_PRODUCT " +
                    "WHERE PRODUCT_NAME LIKE @param+'%'", myConn);
                myConn.Open();

                objComm.Parameters.AddWithValue("@param", param);
                SqlDataReader reader = objComm.ExecuteReader();

                while (reader.Read())
                {

                    ud.Add(new Searchitem
                    {
                        PRODUCT_ID = (long)reader["PRODUCT_ID"],
                        PRODUCT_NAME = reader["PRODUCT_NAME"].ToString()
                    });

                }
                myConn.Close();
                return ud;
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
        public ApiResponse addcart(TB_ECOMM_CART_ITEM cart)
        {
            try
            {
            var chkUser = (from s in entity.TB_ECOMM_CART_ITEM where s.USER_ID == cart.USER_ID && s.PRODUCT_ID==cart.PRODUCT_ID select s).FirstOrDefault();
            if (chkUser == null)
            {
                
                    TB_ECOMM_CART_ITEM CI = new TB_ECOMM_CART_ITEM();
                    if (CI.CART_ITEM_ID == 0)
                    {
                        CI.USER_ID = cart.USER_ID;
                        CI.PRODUCT_ID = cart.PRODUCT_ID;
                        CI.CART_ITEM_QUANTITY = cart.CART_ITEM_QUANTITY;
                        CI.ADDED_DATE = DateTime.Now;
                        CI.MODIFIED_DATE = DateTime.Now;
                        entity.TB_ECOMM_CART_ITEM.Add(CI);
                        entity.SaveChanges();
                        return new ApiResponse
                        { Status = "Success", Message = "Item Added to Cart." };
                    }

            }
            
            else
            {
                    TB_ECOMM_CART_ITEM CI = entity.TB_ECOMM_CART_ITEM.Find(chkUser.CART_ITEM_ID);
                    CI.CART_ITEM_QUANTITY += cart.CART_ITEM_QUANTITY;
                    if (CI.CART_ITEM_QUANTITY <= 5)
                    {
                        int id = this.entity.SaveChanges();
                        return new ApiResponse { Status = "Success", Message = "Item Added to Cart." };
                    }
                    else
                        return new ApiResponse { Status = "Alert", Message = "Maximum 5 Items Can Add." };
                }
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
            return new ApiResponse
            { Status = "Error", Message = "Invalid Data." };
        }

        public object deletecart(long ID)
        {
            try
            {
                TB_ECOMM_CART_ITEM item = entity.TB_ECOMM_CART_ITEM.Find(ID);
                entity.TB_ECOMM_CART_ITEM.Remove(item);


                int id = this.entity.SaveChanges();
                return ID + "-Removed";
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }
    }
}