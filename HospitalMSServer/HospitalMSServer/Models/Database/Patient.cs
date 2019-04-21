using ArangoDB.Client;
using HospitalMSServer.Helpers;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "User")]
    public class Patient : User
    {
        public float Weight { get; set; }
        public float Height { get; set; }

        public override void InsertIntoDB(DatabaseManager databaseManager)
        {
            databaseManager.Database.Insert<Patient>(this);
        }

        public override void UpdateInDB(DatabaseManager databaseManager)
        {
            databaseManager.Database.Update<Patient>(this);
        }
    }
}
