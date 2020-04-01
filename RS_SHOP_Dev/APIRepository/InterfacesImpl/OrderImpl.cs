using APIRepository.Interfaces;
using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.InterfacesImpl
{
    //public class OrderImpl : IOrder
    //{
    //    ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
    //    public object addorder(Order order)
    //    {
    //        try
    //        {
    //            TB_ECOMM_ORDER OR = new TB_ECOMM_ORDER();
    //            int id = this.entity.SaveChanges();
    //            return new ApiResponse
    //            { Status = "Success", Message = "Order Added." };
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("Error" + ex.Message);
    //        }
    //    }
    //}
}