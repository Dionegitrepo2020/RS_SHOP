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
    //Aouthor Rakshit B
    public class ItemImpl : IItem
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public List<Products> GetItems(long categ, long subcateg, decimal pfrom, decimal pto)
        {
            List<TB_ECOMM_PRODUCT> products = entity.TB_ECOMM_PRODUCT.ToList();
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

        public List<Products> GetItem(long pid)
        {
            List<TB_ECOMM_PRODUCT> product = entity.TB_ECOMM_PRODUCT.ToList();
            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<Products> query = from p in product
                                              where p.PRODUCT_ID == pid
                                              select new Products
                                              {
                                                  product = p,
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

        public List<CartItem> Getcart(long uid)
        {
            List<TB_ECOMM_CART_ITEM> cartitem = entity.TB_ECOMM_CART_ITEM.ToList();
            List<TB_ECOMM_PRODUCT> product = entity.TB_ECOMM_PRODUCT.ToList();

            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<CartItem> query = from cp in cartitem
                                              join p in product on cp.PRODUCT_ID equals p.PRODUCT_ID 
                                              where cp.USER_ID == uid
                                              where cp.CATEGORY_ID==10
                                              where cp.WISHLIST==false
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

        public List<CartItem> GetcartM(long uid)
        {
            List<TB_ECOMM_CART_ITEM> cartitem = entity.TB_ECOMM_CART_ITEM.ToList();
            List<TB_ECOMM_PRODUCT> product = entity.TB_ECOMM_PRODUCT.ToList();

            try
            {
                entity.Configuration.LazyLoadingEnabled = false;
                IEnumerable<CartItem> query = from cp in cartitem
                                              join p in product on cp.PRODUCT_ID equals p.PRODUCT_ID
                                              where cp.USER_ID == uid
                                              where cp.CATEGORY_ID == 11
                                              where cp.WISHLIST == false
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
        public List<Searchitem> SearchItem(string param, long cat)
        {
            try
            {
                List<Searchitem> ud = new List<Searchitem>();
                string sConnString = "Data Source=dionesql;Persist Security Info=False;" +
                    "Initial Catalog=ECOMM_DEV;User Id=prashanth;Password=WMPrashanth!;Connect Timeout=60;";
                //string sConnString = "Data Source=RAKSHIT;Persist Security Info=False;" +
                //    "Initial Catalog=ECOMM_DEV;User Id=sa;Password=sa@123;Connect Timeout=60;";

                SqlConnection myConn = new SqlConnection(sConnString);

                SqlCommand objComm = new SqlCommand("SELECT PRODUCT_ID,PRODUCT_NAME FROM dbo.TB_ECOMM_PRODUCT as a " +
                    "inner join dbo.TB_ECOMM_SUB_CATEGORY as b on a.SUB_CATEGORY_ID=b.SUB_CATEGORY_ID " +
                    "inner join dbo.TB_ECOMM_CATEGORY as c on c.CATEGORY_ID=b.CATEGORY_ID " +
                    "WHERE c.CATEGORY_ID=@cat and PRODUCT_NAME LIKE @param+'%'", myConn);
                myConn.Open();

                objComm.Parameters.AddWithValue("@param", param);
                objComm.Parameters.AddWithValue("@cat", cat);
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
                var prod = entity.TB_ECOMM_PRODUCT.Find(cart.PRODUCT_ID);
            var chkUser = (from s in entity.TB_ECOMM_CART_ITEM where s.USER_ID == cart.USER_ID && s.PRODUCT_ID==cart.PRODUCT_ID select s).FirstOrDefault();
            if (chkUser == null)
            {
                
                    TB_ECOMM_CART_ITEM CI = new TB_ECOMM_CART_ITEM();
                    if (CI.CART_ITEM_ID == 0)
                    {
                        CI.USER_ID = cart.USER_ID;
                        CI.PRODUCT_ID = cart.PRODUCT_ID;
                        CI.CART_ITEM_QUANTITY = cart.CART_ITEM_QUANTITY;
                        CI.CART_PRICE = prod.PRODUCT_PRICE * cart.CART_ITEM_QUANTITY;
                        CI.CATEGORY_ID = cart.CATEGORY_ID;
                        CI.WISHLIST = false;
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
                    CI.CART_PRICE += prod.PRODUCT_PRICE * cart.CART_ITEM_QUANTITY;
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

        public ApiResponse updatecart(TB_ECOMM_CART_ITEM cart)
        {
            try
            {
                //var prod = entity.TB_ECOMM_PRODUCT.Find(cart.USER_ID);
                //var chkUser = (from s in entity.TB_ECOMM_CART_ITEM where s.USER_ID == cart.USER_ID && s.PRODUCT_ID == cart.PRODUCT_ID select s).FirstOrDefault();
                //if (chkUser != null)
                //{
                    TB_ECOMM_CART_ITEM CI= entity.TB_ECOMM_CART_ITEM.Find(cart.CART_ITEM_ID);
                    CI.CART_ITEM_QUANTITY = cart.CART_ITEM_QUANTITY;
                    CI.CART_PRICE = CI.TB_ECOMM_PRODUCT.PRODUCT_PRICE * cart.CART_ITEM_QUANTITY;
                    if (CI.CART_ITEM_QUANTITY <= 5)
                    {
                        int id = this.entity.SaveChanges();
                        return new ApiResponse { Status = "Success", Message = "Item Added to Cart." };
                    }
                    else
                        return new ApiResponse { Status = "Alert", Message = "Maximum 5 Items Can Add." };
                //}
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

        public object deleteallcart(long UID)
        {
            try
            {
                var item = entity.TB_ECOMM_CART_ITEM.Where(a=> a.USER_ID==UID).ToList();
                foreach(var items in item)
                {
                    entity.TB_ECOMM_CART_ITEM.Remove(items);
                }
                

                int id = this.entity.SaveChanges();
                return UID + " -Removed";
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public object deleteallcart(long UID,long catid)
        {
            try
            {
                var item = entity.TB_ECOMM_CART_ITEM.Where(a => a.USER_ID == UID && a.CATEGORY_ID==catid).ToList();
                foreach (var items in item)
                {
                    entity.TB_ECOMM_CART_ITEM.Remove(items);
                }


                int id = this.entity.SaveChanges();
                return UID + " -Removed";
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

    }
}