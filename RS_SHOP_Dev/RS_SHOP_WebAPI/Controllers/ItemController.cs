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
    [RoutePrefix("api/item")]
    public class ItemController : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        IItem item = new ItemImpl();
        [Route("listallitem/{categ}/{subcateg}/{pfrom}/{pto}")]
        [HttpGet]
        public Object Getproducts(long categ, long subcateg, decimal pfrom, decimal pto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            List<Products> ud = iitem.GetItems(categ, subcateg, pfrom, pto);
            return ud;
        }

        [Route("listallitem/{pid}")]
        [HttpGet]
        public Object Getproduct(long pid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            List<Products> ud = iitem.GetItem(pid);
            return ud;
        }

        [Route("listallitemdetail/{pid}")]
        [HttpGet]
        public Object Getproductsdetail(long pid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            List<Products> ud = iitem.GetItemsdetail(pid);
            return ud;
        }

        [Route("addtocart")]
        [HttpPost]

        public object addcart(TB_ECOMM_CART_ITEM cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            ApiResponse rs = iitem.addcart(cart);
            return rs;

        }

        [Route("updatecart")]
        [HttpPut]

        public object updatecart(TB_ECOMM_CART_ITEM cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            ApiResponse rs = iitem.updatecart(cart);
            return rs;

        }

        [Route("listcart/{uid}")]
        [HttpGet]
        public Object Getcart(long uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            List<CartItem> ci = iitem.Getcart(uid);
            return ci;
        }

        [Route("listcartM/{uid}")]
        [HttpGet]
        public Object GetcartM(long uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            List<CartItem> ci = iitem.GetcartM(uid);
            return ci;
        }

        [Route("removefromcart/{ID}")]
        [HttpDelete]

        public object deletecart(long ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            object rs = iitem.deletecart(ID);
            return rs;

        }

        [Route("removeallfromcart/{UID}")]
        [HttpDelete]

        public object deleteallcart(long UID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            object rs = iitem.deleteallcart(UID);
            return rs;

        }

        [Route("removeallfromcart/{UID}/{catid}")]
        [HttpDelete]

        public object deleteallcart(long UID, long catid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            object rs = iitem.deleteallcart(UID,catid);
            return rs;

        }

        [Route("searchsuggest/{param}/{cat}")]
        [HttpGet]
        public Object Autosearch(string param, long cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            List<Searchitem> ud = iitem.SearchItem(param,cat);
            return ud;
        }
    }
}
