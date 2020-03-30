using APIRepository.Interfaces;
using APIRepository.InterfacesImpl;
using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RS_SHOP_WebAPI.Controllers
{
    //Author : Rakshit
    [RoutePrefix("api/wishlist")]
    public class WishListController : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        IWishlist item = new WishListImpl();
        [Route("listwishlist/{uid}")]
        [HttpGet]
        public Object Getcart(long uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IWishlist iwish = new WishListImpl();
            List<CartItem> ci = iwish.GetWishlist(uid);
            return ci;
        }

        [Route("add")]
        [HttpPost]

        public object addwishlist(TB_ECOMM_CART_ITEM cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IWishlist iwish = new WishListImpl();
            ApiResponse rs = iwish.addwishlist(cart);
            return rs;

        }

        [Route("movetocart")]
        [HttpPost]

        public object move(TB_ECOMM_CART_ITEM cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IWishlist iwish = new WishListImpl();
            ApiResponse rs = iwish.move(cart);
            return rs;

        }
    }
}
