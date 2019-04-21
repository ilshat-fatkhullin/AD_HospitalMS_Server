using ArangoDB.Client;
using HospitalMSServer.Helpers;
using HospitalMSServer.Models;
using HospitalMSServer.Models.Authentication;
using HospitalMSServer.Models.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HospitalMSServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private DatabaseManager databaseManager;

        private EncryptionHelper encryptionHelper;

        public AuthenticationController()
        {
            databaseManager = new DatabaseManager();
            encryptionHelper = new EncryptionHelper();
        }

        [Route("sign_in")]
        [HttpPost]
        public MessageResponse SignIn(SignInRequest request)
        {
            if (request == null)
            {
                return new MessageResponse("Wrong credentials");
            }

            if (Credential.GetByLoginAndHashFromDB(databaseManager, request.Login, encryptionHelper.GetHash(request.Password)) == null)
            {
                return new MessageResponse("Wrong login or password");
            }

            return new MessageResponse("Authenticated");
        }

        [Route("sign_up")]
        [HttpPost]
        public MessageResponse SignUp(SignUpRequest signUpModel)
        {
            if (signUpModel == null || signUpModel.IsInvalid() || signUpModel.User.IsInvalid())
            {
                return new MessageResponse("Wrong data");
            }

            if (Models.Database.User.GetByEmailFromDB(databaseManager, signUpModel.User.Email) != null)
            {
                return new MessageResponse("This email already registered");
            }

            if (signUpModel.User.UserType == UserType.ADMIN)
            {
                return new MessageResponse("Security violation");
            }

            signUpModel.User.IsVerified = false;
            signUpModel.User.VerificationLink = EmailHelper.GetRandomVerificationLink();

            try
            {
                EmailHelper.SendVerificationEmail(signUpModel.User.Email, signUpModel.User.VerificationLink);
            }
            catch (Exception ex)
            {
                return new MessageResponse(ex.Message);
            }

            databaseManager.Database.Insert<User>(signUpModel.User);

            Credential credentials = new Credential();

            try
            {
                credentials.Key = GetLoginFromName(signUpModel.User.Name);
            }
            catch (ArgumentException)
            {
                return new MessageResponse("Wrong name format");
            }

            credentials.PasswordHash = encryptionHelper.GetHash(signUpModel.Password);
            databaseManager.Database.Insert<Credential>(credentials);

            return new MessageResponse("Registered");
        }

        [Route("verify")]
        [HttpGet]
        public string Verify(string verificationLink)
        {
            User user = Models.Database.User.GetByVerificationLinkFromDB(databaseManager, verificationLink);

            if (user == null)
            {
                return "Wrong verification link";
            }

            user.IsVerified = true;
            user.UpdateInDB(databaseManager);
            return "User is verified";
        }

        private string GetLoginFromName(string name)
        {
            name = name.ToLower();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }

            string[] parts = name.Split(' ');
            if (parts.Length != 2)
            {
                throw new ArgumentException();
            }

            string userName = parts[0][0].ToString() + "." + parts[1];
            int repeats = databaseManager.Database.Query<Credential>()
                .Where(c => AQL.Contains(c.Key, userName)).Count();
            return userName + Convert.ToString(repeats);
        }

        [Route("change_password")]
        [HttpPut]
        public MessageResponse ChangePassword(ChangePasswordRequest request)
        {
            if (request == null || request.IsInvalid())
            {
                return new MessageResponse("Wrong request");
            }

            Credential credential = Credential.GetByLoginAndHashFromDB(databaseManager, request.Login, encryptionHelper.GetHash(request.Password));

            if (credential == null)
            {
                return new MessageResponse("Wrong login or password");
            }
            credential.PasswordHash = encryptionHelper.GetHash(request.NewPassword);
            databaseManager.Database.Update<Credential>(credential);
            return new MessageResponse("Password was successfuly changed");
        }

        public MessageResponse ResetPassword(ResetPasswordRequest request)
        {
            if (request == null || request.IsInvalid())
            {
                return new MessageResponse("Wrong request");
            }

            User user = Models.Database.User.GetByEmailFromDB(databaseManager, request.Email);
            if (user == null)
            {
                return new MessageResponse("There are no such user");
            }

            Credential credential = databaseManager.Database.Query<Credential>().FirstOrDefault(c => c.Key == user.Key);
            string newPassword = EmailHelper.GetRandomVerificationLink();
            EmailHelper.SendPasswordResetEmail(request.Email, newPassword);
            credential.PasswordHash = encryptionHelper.GetHash(newPassword);
            databaseManager.Database.Update<Credential>(credential);

            return new MessageResponse("Reset");
        }
    }
}