using APIRepository.Interfaces;
using APIRepository.InterfacesImpl;
using APIRepository.Models;
using APIRepository.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RS_SHOP_WebAPI.Controllers
{
    //Author : Rakshit
    [RoutePrefix("api/country")]
    public class CountryController : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        ICountry item = new CountryImpl();
        [Route("listallcountry")]
        [HttpGet]
        public Object Getcountries()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ICountry icountry = new CountryImpl();
            List<Country> ud = icountry.Getcountry();
            return ud;
        }

        [Route("listcountry/{cid}")]
        [HttpGet]
        public Object Getcountry(long cid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ICountry icountry = new CountryImpl();
            List<Country> ud = icountry.Getcountry(cid);
            return ud;
        }
    }
}
