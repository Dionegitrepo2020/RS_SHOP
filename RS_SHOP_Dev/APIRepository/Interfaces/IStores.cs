using APIRepository.Models;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Interfaces
{
    public interface IStores
    {
        ApiResponse addstore(TB_ECOMM_PICKUP_STORES store);
        object Getstores();
    }
}