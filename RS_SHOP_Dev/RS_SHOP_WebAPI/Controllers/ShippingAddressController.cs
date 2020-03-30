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
    //Author : Rakshit / Prakash
    [RoutePrefix("api/shipaddress")]
    public class ShippingAddressController : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        [Route("add")]
        [HttpPost]
        public object addaddress(Address addres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ISAddress address = new SAddressImpl();
            AddressResponse rs = address.addaddress(addres);
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
            ISAddress getaddress = new SAddressImpl();
            List<TB_ECOMM_ADDRESS> ud = getaddress.GetSAddress(uid);
            return ud;
        }

        [Route("list/{AID}")]
        [HttpGet]

        public object GetAddress(long aid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ISAddress address = new SAddressImpl();
            object ud = address.getaddress(aid);
            return ud;
        }

        [Route("listdefault/{UID}")]
        [HttpGet]

        public object Getcarddefault(long uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ISAddress getcardd = new SAddressImpl();
            List<Addresslist> ud = getcardd.getaddressdefault(uid);
            return ud;
        }

        [Route("removeaddress/{AID}")]
        [HttpDelete]

        public object deletecard(long AID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ISAddress add = new SAddressImpl();
            object rs = add.deleteaddress(AID);
            return rs;

        }

    }
}
