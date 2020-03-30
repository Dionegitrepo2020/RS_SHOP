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
    [RoutePrefix("api/payment")]
    public class CardDetailsController : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        [Route("add")]
        [HttpPost]
        public object addcard(CardDetails card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Icard addcard = new CardImpl();
            CardResponse rs = addcard.addcard(card);
            return rs;

        }

        [Route("addresponse")]
        [HttpPost]
        public object addpaymentdetail(CardDetails card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Icard addcard = new CardImpl();
            CardResponse rs = addcard.addpaymentdetail(card);
            return rs;

        }

        [Route("list/{CID}")]
        [HttpGet]

        public object Getcard(long cid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Icard getcard = new CardImpl();
            object ud = getcard.getcard(cid);
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
            Icard getcard = new CardImpl();
            List<Cards> ud = getcard.getcarddefault(uid);
            return ud;
        }

        [Route("listall/{UID}")]
        [HttpGet]

        public object Getcards(long uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Icard getcards = new CardImpl();
            List<TB_ECOMM_CARD_DETAILS> ud = getcards.getcards(uid);
            return ud;
        }

        [Route("updatepayment")]
        [HttpPut]

        public object updatecard(CardDetails card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Icard icard = new CardImpl();
            CardResponse rs = icard.updatecard(card);
            return rs;

        }

        [Route("removecard/{CID}")]
        [HttpDelete]

        public object deletecard(long CID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Icard iitem = new CardImpl();
            object rs = iitem.deletecard(CID);
            return rs;

        }

    }
}
