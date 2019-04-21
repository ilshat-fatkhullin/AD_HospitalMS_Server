using ArangoDB.Client;
using HospitalMSServer.Helpers;
using System.Linq;

namespace HospitalMSServer.Models.Database
{
    public class Credential
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }
        public string PasswordHash { get; set; }

        public static Credential GetByLoginAndHashFromDB(DatabaseManager databaseManager, string login, string passwordHash)
        {
            return databaseManager.Database.Query<Credential>().FirstOrDefault(c => c.Key == login && c.PasswordHash == passwordHash);
        }


    }
}
