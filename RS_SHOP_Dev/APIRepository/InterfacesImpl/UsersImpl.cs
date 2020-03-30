using APIRepository.Helper;
using APIRepository.Interfaces;
using APIRepository.Models;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace APIRepository.InterfacesImpl
{
    //Author : Rakshit
    public class UsersImpl : IUsers
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        public SignupResponse Create(SignUp signUp)

        {
            try
            {
                var chkUser = (from s in entity.TB_ECOMM_USERS where s.USER_EMAIL == signUp.USER_EMAIL select s).FirstOrDefault();
                if (chkUser == null)
                {
                    TB_ECOMM_USERS EL = new TB_ECOMM_USERS();
                    if (EL.USER_ID == 0)
                    {
                        if (signUp.USER_NAME != "null")
                        {
                            var keyNew = HelperMethods.GeneratePassword(10);
                            EL.USER_NAME = signUp.USER_NAME;
                            EL.USER_EMAIL = signUp.USER_EMAIL;
                            var User_Password = HelperMethods.EncodePassword(signUp.USER_PASSWORD, keyNew);
                            EL.USER_PASSWORD = User_Password;
                            EL.PASSWORD_SALT = keyNew;
                            EL.DATE_OF_BIRTH = Convert.ToDateTime(signUp.DATE_OF_BIRTH);
                            EL.CONDITION_ID = signUp.CONDITION_ID;
                            EL.PARENT_NAME = signUp.PARENT_NAME;
                            EL.PARENT_ID = signUp.PARENT_ID;
                            entity.TB_ECOMM_USERS.Add(EL);
                            entity.SaveChanges();
                            return new SignupResponse
                            { Status = "Success", USER_ID = EL.USER_ID.ToString(), Message = "User SuccessFully Saved." };
                        }
                        else return new SignupResponse { Status = "Error", Message = "Enter Valid Name!!!" };
                        
                        }
                }
                else return new SignupResponse { Status = "Error", USER_ID = chkUser.USER_ID.ToString(), Message = "User Already Exist!!!" };
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        return new SignupResponse { Status = "Error", Message = validationError.ErrorMessage };
                    }
                }
            }
            return new SignupResponse
            { Status = "Error", Message = "Invalid Data." };
        }

        public ApiResponse Createsm(SmSignUp smSignUp)
        {
            try
            {
                var chkUser = (from s in entity.TB_ECOMM_USERS where s.USER_EMAIL == smSignUp.USER_EMAIL || s.USER_NAME == smSignUp.USER_NAME select s).FirstOrDefault();
                if (chkUser == null)
                {
                    TB_ECOMM_USERS EL = new TB_ECOMM_USERS();
                    if (EL.USER_ID == 0)
                    {
                        var keyNew = HelperMethods.GeneratePassword(10);
                        EL.USER_NAME = smSignUp.USER_NAME;
                        EL.USER_EMAIL = smSignUp.USER_EMAIL;
                        EL.USER_ID = smSignUp.USER_SID;
                        EL.GENDER = smSignUp.GENDER;
                        //var User_Password = HelperMethods.EncodePassword(signUp.USER_PASSWORD, keyNew);
                        //EL.USER_PASSWORD = User_Password;
                        //EL.PASSWORD_SALT = keyNew;
                        entity.TB_ECOMM_USERS.Add(EL);
                        entity.SaveChanges();
                        return new ApiResponse
                        { Status = "Success",  Message = "User SuccessFully Saved." };
                    }
                }
                else return new ApiResponse { Status = "Error", Message = "User Already Exist!!!" };
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        return new ApiResponse { Status = "Error", Message = validationError.ErrorMessage };
                    }
                }
            }
            return new ApiResponse
            { Status = "Error", Message = "Invalid Data." };
        }

        public LoginResponse CheckLogin(TB_ECOMM_USERS loginm)
        {
            try
            {

                var getUser = (from s in entity.TB_ECOMM_USERS where (s.USER_EMAIL == loginm.USER_EMAIL || s.USER_NAME == loginm.USER_NAME) select s).FirstOrDefault();
                if (getUser != null)
                {
                    var hashCode = getUser.PASSWORD_SALT;
                    //Password Hasing Process Call Helper Class Method    
                    var encodingPasswordString = HelperMethods.EncodePassword(loginm.USER_PASSWORD, hashCode);
                    //Check Login Detail User Name Or Password    
                    var query = (from s in entity.TB_ECOMM_USERS where (s.USER_EMAIL == loginm.USER_EMAIL || s.USER_NAME == loginm.USER_NAME) && s.USER_PASSWORD.Equals(encodingPasswordString) select s).FirstOrDefault();
                    if (query != null)
                    {

                        return new LoginResponse
                        {
                            Status = "Success",
                            Message = "Login Successfully",
                            User_Name = query.USER_NAME,
                            User_Id = query.USER_ID,
                            Email_Id = query.USER_EMAIL
                        };
                    }
                    return new LoginResponse { Status = "Invalid", Message = "Invalid Password." };
                }
                return new LoginResponse { Status = "Invalid", Message = "User Does not Exist." };

            }
            catch (Exception e)
            {
                return new LoginResponse { Status = "Error", Message = e.Message.ToString() };
            }
        }

        public List<SignUp> GetUsers()
        {
            try
            {

                List<SignUp> dtl = new List<SignUp>();
                foreach (TB_ECOMM_USERS t in entity.TB_ECOMM_USERS)
                {
                    SignUp dto = new SignUp();
                    dto.USER_ID = t.USER_ID;
                    dto.USER_EMAIL = t.USER_EMAIL;
                    dto.USER_NAME = t.USER_NAME;
                    dto.GENDER = t.GENDER;
                    dto.DATE_OF_BIRTH =t.DATE_OF_BIRTH;
                    dto.COUNTRY = t.COUNTRY;
                    dto.CITY = t.CITY;
                    dto.USER_PROFILE_IMAGE = t.USER_PROFILE_IMAGE;
                    dtl.Add(dto);
                }
                return dtl;
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public object GetUser(int Id)
        {
            try
            {
                if (Id !=0)
                {
                    TB_ECOMM_USERS user = entity.TB_ECOMM_USERS.Find(Id);
                    SignUp userDTO = new SignUp();
                    userDTO.USER_ID = user.USER_ID;
                    userDTO.USER_EMAIL = user.USER_EMAIL;
                    userDTO.USER_NAME = user.USER_NAME;
                    userDTO.GENDER = user.GENDER;
                    userDTO.DATE_OF_BIRTH = user.DATE_OF_BIRTH;
                    userDTO.COUNTRY = user.COUNTRY;
                    userDTO.CITY = user.CITY;
                    userDTO.USER_PROFILE_IMAGE = user.USER_PROFILE_IMAGE;
                    return userDTO;
                }
                else { return new SignUp(); }
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public object UpdateUser(TB_ECOMM_USERS Reg, int Id)
        {
            try
            {
                TB_ECOMM_USERS user = entity.TB_ECOMM_USERS.Find(Id);
                if (user != null)
                {
                    var keyNew = HelperMethods.GeneratePassword(10);
                    var User_Password = HelperMethods.EncodePassword(Reg.USER_PASSWORD, keyNew);
                    Reg.USER_PASSWORD = User_Password;
                    Reg.PASSWORD_SALT = keyNew;
                    user.USER_NAME = Reg.USER_NAME;
                    user.USER_EMAIL = Reg.USER_EMAIL;
                    user.USER_PHONE = Reg.USER_PHONE;
                    user.GENDER = Reg.GENDER;
                    user.CITY = Reg.CITY;
                    user.COUNTRY = Reg.COUNTRY;
                    user.DATE_OF_BIRTH = Reg.DATE_OF_BIRTH;
                    user.PASSWORD_SALT = Reg.PASSWORD_SALT;
                    user.ROLE_ID = Reg.ROLE_ID;
                    user.USER_PASSWORD = Reg.USER_PASSWORD;
                    user.USER_PROFILE_IMAGE = Reg.USER_PROFILE_IMAGE;
                    //user.MODIFIED_DATE = DateTime;


                }
                int id = this.entity.SaveChanges();
                return Id + "-Updated";
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }

        public object DeleteUser(int Id)
        {
            try
            {
                TB_ECOMM_USERS user = entity.TB_ECOMM_USERS.Find(Id);
                entity.TB_ECOMM_USERS.Remove(user);


                int id = this.entity.SaveChanges();
                return Id + "-Removed";
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }
    }
}