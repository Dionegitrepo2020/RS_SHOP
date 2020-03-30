using APIRepository.Models;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Interfaces
{
    public interface IDelivery
    {
        ApiResponse adddeliverytype(TB_ECOMM_DELIVERY_TYPE delivery);
        object getdeliverytype();
    }
}