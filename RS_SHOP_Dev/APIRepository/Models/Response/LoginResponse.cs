using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Response
{
    public class LoginResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string User_Name { set; get; }
        public long User_Id { set; get; }
        public string Email_Id { set; get; }
    }
}