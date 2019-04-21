using HospitalMSServer.Helpers;
using HospitalMSServer.Models;
using HospitalMSServer.Models.Authentication;
using HospitalMSServer.Models.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalMSServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private DatabaseManager databaseManager;

        public UserController()
        {
            databaseManager = new DatabaseManager();
        }

        [Route("get_users")]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return databaseManager.Database.Query<User>().ToList();
        }

        [Route("add_user")]
        [HttpPost]
        public MessageResponse AddUser(User user)
        {            
            if (Models.Database.User.IsInDB(databaseManager, user.Key))
            {                
                return new MessageResponse("User already exists");
            }
            user.InsertIntoDB(databaseManager);
            return new MessageResponse("User added");
        }

        [Route("edit_user")]
        [HttpPut]
        public MessageResponse EditUser(User user)
        {
            if (Models.Database.User.IsInDB(databaseManager, user.Key))
            {
                user.UpdateInDB(databaseManager);
                return new MessageResponse("User updated");
            }
            return new MessageResponse("There is no such user");
        }

        [Route("delete_user")]
        [HttpDelete]
        public MessageResponse DeleteUser(DeleteUserRequest request)
        {
            if (Models.Database.User.IsInDB(databaseManager, request.Key))
            {
                Models.Database.User.RemoveFromDB(databaseManager, request.Key);
                return new MessageResponse("User removed");
            }
            return new MessageResponse("There is no such user");
        }
    }
}
