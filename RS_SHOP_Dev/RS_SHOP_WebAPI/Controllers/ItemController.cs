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

        [Route("listcart/{cid}")]
        [HttpGet]
        public Object Getcart(long cid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            object ci = iitem.Getcart(cid);
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

        [Route("searchsuggest/{param}")]
        [HttpGet]
        public Object Autosearch(string param)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IItem iitem = new ItemImpl();
            List<Searchitem> ud = iitem.SearchItem(param);
            return ud;
        }
    }
}
