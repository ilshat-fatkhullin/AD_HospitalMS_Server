using ArangoDB.Client;
using HospitalMSServer.Helpers;
using HospitalMSServer.JSONConverters;
using Newtonsoft.Json;
using System.Linq;

namespace HospitalMSServer.Models.Database
{
    [JsonConverter(typeof(UserJsonConverter))]
    [CollectionProperty(CollectionName = "User")]
    public abstract class User
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string VerificationLink { get; set; }
        public bool IsVerified { get; set; }
        public string AddressKey { get; set; }
        public string BirthDate { get; set; }
        public char Gender { get; set; }
        public string PhotoLink { get; set; }
        public string UserType { get; set; }
        public abstract void InsertIntoDB(DatabaseManager databaseManager);
        public abstract void UpdateInDB(DatabaseManager databaseManager);

        public void RemoveFromDB(DatabaseManager databaseManager)
        {
            databaseManager.Database.Remove<User>(this);
        }

        public bool IsInDB(DatabaseManager databaseManager)
        {
            return databaseManager.Database.Query<User>()
                .Where(u => u.Key == Key)
                .Count() > 0;
        }

        public static User GetByEmailFromDB(DatabaseManager databaseManager, string email)
        {
            return databaseManager.Database.Query<User>().FirstOrDefault(u => u.Email == email);
        }

        public bool IsInvalid()
        {
            return string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email);
        }

        public static User GetByVerificationLinkFromDB(DatabaseManager databaseManager, string link)
        {
            return databaseManager.Database.Query<User>().FirstOrDefault(u => u.VerificationLink == link);
        }
    }
}
