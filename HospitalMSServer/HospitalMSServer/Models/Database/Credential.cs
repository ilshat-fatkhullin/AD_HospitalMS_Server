using ArangoDB.Client;
using HospitalMSServer.Helpers;
using System.Linq;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "Credential", Naming = NamingConvention.UnChanged)]
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
