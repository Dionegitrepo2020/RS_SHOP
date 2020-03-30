using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Interfaces
{
    public interface IItem
    {
        List<Products> GetItems(long categ, long subcateg, decimal pfrom, decimal pto);
        List<Products> GetItemsdetail(long pid);
        List<Products> GetItem(long pid);
        ApiResponse addcart(TB_ECOMM_CART_ITEM cart);
        ApiResponse updatecart(TB_ECOMM_CART_ITEM cart);
        List<CartItem> Getcart(long uid);
        List<CartItem> GetcartM(long uid);
        object deletecart(long ID);
        object deleteallcart(long UID);
        object deleteallcart(long UID,long catid);
        List<Searchitem> SearchItem(string param,long cat);
    }
}