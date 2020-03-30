using APIRepository.Models.Custom;
using RS_SHOP_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS_SHOP_WebAPI.Service
{
    public class UserService
    {
        public User GetUserByCredentials(string email, string password)
        {
            User user = new User() { Id = "1", Email = "email@domain.com", Password = "password", Name = "Sample" };
            if (user != null)
            {
                user.Password = string.Empty;
            }
            return user;
        }
    }
}