using ArangoDB.Client;
using HospitalMSServer.Helpers;
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
        public AuthentificationResponse SignIn(SignInRequest request)
        {
            AuthentificationResponse response = new AuthentificationResponse();

            if (request == null)
            {
                response.Message = "Wrong credentials";
            }
            else
            {
                string passwordHash = encryptionHelper.GetHash(request.Password);

                Credential credentials = databaseManager.Database.Query<Credential>()
                    .FirstOrDefault(c => c.Key == request.Login &&                                    
                                    c.PasswordHash == passwordHash);

                if (credentials == null)
                {
                    response.Message = "Wrong login or password";
                }
                else
                {
                    response.Message = "Authenticated";
                }
            }

            return response;
        }

        [Route("sign_up")]
        [HttpPost]
        public AuthentificationResponse SignUp(SignUpRequest signUpModel)
        {
            AuthentificationResponse response = new AuthentificationResponse();

            if (signUpModel == null || signUpModel.User == null ||
                signUpModel.User.Name == null || signUpModel.Password == null ||
                signUpModel.User.Phone == null || signUpModel.User.Email == null)
            {
                response.Message = "Wrong data";
                return response;
            }

            if (databaseManager.Database.Query<User>().FirstOrDefault(u => u.Email == signUpModel.User.Email) != null)
            {
                response.Message = "This email already registered";
                return response;
            }            

            if (signUpModel.User.UserType == 'A')
            {
                response.Message = "Security violation";
                return response;
            }

            signUpModel.User.IsVerified = false;
            signUpModel.User.VerificationLink = VerificationHelper.GetRandomVerificationLink();

            try
            {
                VerificationHelper.SendVerificationEmail(signUpModel.User.Email, signUpModel.User.VerificationLink);
            }
            catch (Exception ex)
            {                
                response.Message = ex.Message;
                return response;
            }

            databaseManager.Database.Insert<User>(signUpModel.User);

            Credential credentials = new Credential();

            try
            {
                credentials.Key = GetLoginFromName(signUpModel.User.Name);
            }
            catch (ArgumentException)
            {
                response.Message = "Wrong name format";
                return response;
            }

            credentials.PasswordHash = encryptionHelper.GetHash(signUpModel.Password);
            databaseManager.Database.Insert<Credential>(credentials);

            response.Message = "Registered";
            return response;
        }

        [Route("verify")]
        [HttpGet]
        public string Verify(string verificationLink)
        {            
            User user = databaseManager.Database.Query<User>().FirstOrDefault(u => u.VerificationLink == verificationLink);

            if (user == null)
            {
                return "Wrong verification link";
            }            

            user.IsVerified = true;
            databaseManager.Database.Update<User>(user);
            return "User verified";            
        }

        private string GetLoginFromName(string name)
        {
            name = name.ToLower();
            if (name == null || name.Length == 0)
            {
                throw new ArgumentException();
            }

            string[] parts = name.Split(' ');
            if (parts.Length != 2)
            {
                throw new ArgumentException();
            }

            string userName = parts[0][0] + '.' + parts[1];
            int repeats = databaseManager.Database.Query<Credential>()
                .Where(c => AQL.Contains(c.Key, userName)).Count();
            return userName + Convert.ToString(repeats);
        }
    }
}