using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Interfaces
{
    public interface IOrder
    {
        IEnumerable<TB_ECOMM_ORDER> addorder(Order order);
        object Getorders(long uid);
        object Getorder(long oid);
    }
}