using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Interfaces
{
    public interface Icard
    {
        CardResponse addcard(CardDetails card);
        CardResponse addpaymentdetail(CardDetails card);
        object getcard(long cid);
        List<Cards> getcarddefault(long uid);
        List<TB_ECOMM_CARD_DETAILS> getcards(long uid);
        CardResponse updatecard(CardDetails cart);
        object deletecard(long CID);
    }
}