using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Interfaces
{
    public interface IWishlist
    {
        List<CartItem> GetWishlist(long uid);
        ApiResponse addwishlist(TB_ECOMM_CART_ITEM cart);
        ApiResponse move(TB_ECOMM_CART_ITEM cart);
    }
}