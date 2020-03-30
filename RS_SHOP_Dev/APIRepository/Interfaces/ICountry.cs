using APIRepository.InterfacesImpl;
using APIRepository.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Interfaces
{
    public interface ICountry
    {
        List<Country> Getcountry();
        List<Country> Getcountry(long cid);
    }
}