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
        ApiResponse addcart(TB_ECOMM_CART_ITEM cart);
        object Getcart(long cid);
        object deletecart(long ID);
        List<Searchitem> SearchItem(string param);
    }
}