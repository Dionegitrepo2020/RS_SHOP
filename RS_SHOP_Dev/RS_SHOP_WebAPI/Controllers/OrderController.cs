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
    [RoutePrefix("api/oder")]
    public class Ordercontroller : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        [Route("add")]
        [HttpPost]
        public IEnumerable<TB_ECOMM_ORDER> addorder(Order order)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            IOrder iorder = new OrderImpl();
            IEnumerable<TB_ECOMM_ORDER> rs = iorder.addorder(order);
            return rs;

        }

        [Route("listall/{UID}")]
        [HttpGet]

        public object GetSAddress(long uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IOrder getorders = new OrderImpl();
            object ud = getorders.Getorders(uid);
            return ud;
        }

        [Route("list/{OID}")]
        [HttpGet]

        public object Getorder(long oid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IOrder getorders = new OrderImpl();
            object ud = getorders.Getorder(oid);
            return ud;
        }

    }
}
