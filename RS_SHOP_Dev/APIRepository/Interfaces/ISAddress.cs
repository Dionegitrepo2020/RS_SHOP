using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Interfaces
{
    public interface ISAddress
    {
        AddressResponse addaddress(Address addres);
        List<TB_ECOMM_ADDRESS> GetSAddress(long uid);
        object getaddress(long aid);
        List<Addresslist> getaddressdefault(long uid);
        object deleteaddress(long AID);
    }
}