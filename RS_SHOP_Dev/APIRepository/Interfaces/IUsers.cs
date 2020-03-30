using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRepository.Interfaces
{
    //Author : Rakshit
    public interface IUsers
    {
        SignupResponse Create(SignUp signUp);
        ApiResponse Createsm(SmSignUp smSignUp);
        LoginResponse CheckLogin(TB_ECOMM_USERS login);
        List<SignUp> GetUsers();
        object GetUser(int Id);
    }
}
