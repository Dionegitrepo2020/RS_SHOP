using APIRepository.Helper;
using APIRepository.Interfaces;
using APIRepository.InterfacesImpl;
using APIRepository.Models;
using APIRepository.Models.Binding;
using APIRepository.Models.Custom;
using APIRepository.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace RS_SHOP_WebAPI.Controllers
{
    //Author : Rakshit / Prakash
    [RoutePrefix("api/user")]
    public class LoginSignupController : ApiController
    {
        ECOMM_DEVEntities entity = new ECOMM_DEVEntities();
        IUsers users = new UsersImpl();
        [Route("signup")]
        [HttpPost]
        public object UserSignup(SignUp signUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApiResponse rs = users.Create(signUp);
            return rs;
        }

        [Route("smsignup")]
        [HttpPost]
        public object UsersmSignup(SmSignUp smSignUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApiResponse rs = users.Createsm(smSignUp);
            return rs;
        }

        [Route("login")]
        [HttpPost]

        public object CheckLogin(TB_ECOMM_USERS login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IUsers loginint = new UsersImpl();
            LoginResponse rs = loginint.CheckLogin(login);
            return rs;

        }

        [Route("listallusers")]
        [HttpGet]

        public object GetUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IUsers getusers = new UsersImpl();
            List<SignUp> ud = getusers.GetUsers();
            return ud;
        }

        [Route("listuser/{ID}")]
        [HttpGet]
        public object GetUsers(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IUsers getusers = new UsersImpl();
            object ud = getusers.GetUser(Id);
            return ud;
        }

        [Route("forgotpass")]
        [HttpPost]
        public object ForgotPassword(ForgotPass forgotPass)
        {

            string message = "";
           bool status = false;
            var account = entity.TB_ECOMM_USERS.Where(a => a.USER_EMAIL == forgotPass.Email).FirstOrDefault();
            if (account != null)
            {
                //Send email for reset password
                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.USER_EMAIL, resetCode, "ResetPassword");
                account.RESET_CODE = resetCode;
                //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property
                entity.Configuration.ValidateOnSaveEnabled = false;
                entity.SaveChanges();
                message = "Reset password link has been sent to your email id.";
            }
            else
            {
                message = "Account not found";
            }

            return message;

        }

        [Route("updatepass")]
        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public object ResetPassword(ResetPasswordModel model)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                var user = entity.TB_ECOMM_USERS.Where(a => a.RESET_CODE == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    var keyNew = HelperMethods.GeneratePassword(10);
                    user.USER_PASSWORD = HelperMethods.EncodePassword(model.NewPassword, keyNew);
                    user.RESET_CODE = "";
                    user.PASSWORD_SALT = keyNew;
                    entity.Configuration.ValidateOnSaveEnabled = false;
                    entity.SaveChanges();
                    message = "New password updated successfully";
                }

            }
            else
            {
                message = "Something invalid";
            }
            //ViewBag.Message = message;
            //return View(model);
            return message;
        }

        [Route("update/{field}/{value}/{ID}")]
        [HttpPut]
        public object updateuser(string field, string value, int ID)
        {
            try
            {
                TB_ECOMM_USERS user = entity.TB_ECOMM_USERS.Find(ID);
                if (user != null)
                {
                    if (field == "USER_NAME")
                    {
                        user.USER_NAME = value;
                    }
                    if (field == "USER_EMAIL")
                    {
                        user.USER_EMAIL = value;
                    }
                    if (field == "GENDER")
                    {
                        user.GENDER = value;
                    }
                    if (field == "DATE_OF_BIRTH")
                    {
                        user.DATE_OF_BIRTH = Convert.ToDateTime(value);
                    }
                    if (field == "COUNTRY")
                    {
                        user.COUNTRY = value;
                    }
                    if (field == "CITY")
                    {
                        user.CITY = value;
                    }
                    //if (field == user.USER_PROFILE_IMAGE)
                    //{
                    //    user.USER_PROFILE_IMAGE = value;
                    //}



                }
                int id = this.entity.SaveChanges();
                return ID + "-Updated";
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }


        [Route("activateaccountlink")]
        [HttpPost]
        public object ActivateAccountlink(ForgotPass forgotPass)
        {

            string message = "";
            bool status = false;
            //var account = entity.TB_ECOMM_USERS.Where(a => a.USER_EMAIL == forgotPass.Email).FirstOrDefault();
            if (forgotPass.Email != null)
            {
                //Send email for reset password
                //string resetCode = Guid.NewGuid().ToString();
                SendEVerificationLinkEmail(forgotPass.Email, "VerifyAccount");
                //account.RESET_CODE = resetCode;
                //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property
                entity.Configuration.ValidateOnSaveEnabled = false;
                //entity.SaveChanges();
                message = "Activate link has been sent to your email id.";
            }
            else
            {
                message = "Account not found";
            }

            return message;

        }

        [Route("activateaccount")]
        [HttpPost]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public object Activateaccount(ForgotPass forgotPass)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                var user = entity.TB_ECOMM_USERS.Where(a => a.USER_EMAIL == forgotPass.Email).FirstOrDefault();
                if (user != null)
                {
                    user.EMAIL_CONFIRMED = true;
                    entity.Configuration.ValidateOnSaveEnabled = false;
                    entity.SaveChanges();
                    message = "Your acoount has been successfully activated";
                }

            }
            else
            {
                message = "Something invalid";
            }
            //ViewBag.Message = message;
            //return View(model);
            return message;
        }

        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "ResetPassword")
        {
            //var verifyUrl = "/User/" + emailFor + "?code=" + activationCode;
            var verifyUrl = "/User/" + emailFor + "?code=" + activationCode;
            var link = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("rakshitrb@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "9742508070"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/>You have requested to reset the password of your account:"
                    + emailID +
                    "<br/><br/> Please <a href=" + link + ">Click here to change your password</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        public void SendEVerificationLinkEmail(string emailID, string emailFor = "VerifyAccount")
        {
            //var verifyUrl = "/User/" + emailFor + "?code=" + activationCode;
            var verifyUrl = "app://deeplink";
            var link = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("rakshitrb@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "9742508070"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Email Varification!";
                body = "<br/><br/>Thank you for verifying your Email Address: " + emailID +
                    " Please click here <a href='"+link+ "'>" + link + "</a> test";
                    //" Please click here <a href='" + link + "'>" + link + "</a> to activate your account";
                    //" <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }

            //MailMessage mail = new MailMessage();
            //mail.To.Add(emailID);
            ////mail.To.Add("Another Email ID where you wanna send same email");
            //mail.From = new MailAddress("rakshitrb@gmail.com");
            //mail.Subject = "Inquiry";


            //mail.Body = body;

            //mail.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            //smtp.Credentials = new System.Net.NetworkCredential
            //     ("rakshitrb@gmail.com", "9742508070");
            ////Or your Smtp Email ID and Password
            //smtp.EnableSsl = true;
            //smtp.Send(mail);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        public object ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return "NotFound";
            }


            var user = entity.TB_ECOMM_USERS.Where(a => a.RESET_CODE == id).FirstOrDefault();
            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = id;
                return (model);
            }
            else
            {
                return "NotFound";
            }

        }
    }
}
