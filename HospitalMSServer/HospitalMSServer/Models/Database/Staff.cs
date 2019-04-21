using ArangoDB.Client;
using HospitalMSServer.Helpers;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "User")]
    public class Staff : User
    {
        public string Designation { get; set; }

        public override void InsertIntoDB(DatabaseManager databaseManager)
        {
            databaseManager.Database.Insert<Staff>(this);
        }

        public override void UpdateInDB(DatabaseManager databaseManager)
        {
            databaseManager.Database.Update<Staff>(this);
        }
    }
}
