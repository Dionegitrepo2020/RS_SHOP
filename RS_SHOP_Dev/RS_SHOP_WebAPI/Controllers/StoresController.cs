using APIRepository.Interfaces;
using APIRepository.InterfacesImpl;
using APIRepository.Models;
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
    [RoutePrefix("api/store")]
    public class StoresController : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        [Route("add")]
        [HttpPost]
        public object addstore(TB_ECOMM_PICKUP_STORES store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IStores iorder = new StoresImpl();
            ApiResponse rs = iorder.addstore(store);
            return rs;

        }

        [Route("listall")]
        [HttpGet]

        public object GetStores()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IStores getstores = new StoresImpl();
            object ud = getstores.Getstores();
            return ud;
        }
    }
}
