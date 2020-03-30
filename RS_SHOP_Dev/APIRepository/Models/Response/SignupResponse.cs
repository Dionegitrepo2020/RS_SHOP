using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIRepository.Models.Response
{
    public class SignupResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string USER_ID { get; set; }
    }
}