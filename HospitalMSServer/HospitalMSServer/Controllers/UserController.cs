using HospitalMSServer.Helpers;
using HospitalMSServer.Models;
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
            if (user.IsInDB(databaseManager))
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
            if (user.IsInDB(databaseManager))
            {
                user.UpdateInDB(databaseManager);
                return new MessageResponse("User updated");
            }
            return new MessageResponse("There is no such user");
        }

        [Route("delete_users")]
        [HttpDelete]
        public MessageResponse DeleteUser(User user)
        {
            if (user.IsInDB(databaseManager))
            {
                user.RemoveFromDB(databaseManager);
                return new MessageResponse("User removed");
            }
            return new MessageResponse("There is no such user");
        }
    }
}
