using ArangoDB.Client;
using HospitalMSServer.Helpers;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "User", Naming = NamingConvention.UnChanged)]
    public class Doctor : Staff
    {
        public string DoctorType { get; set; }
        public string Designation { get; set; }

        public override void InsertIntoDB(DatabaseManager databaseManager)
        {
            databaseManager.Database.Insert<Doctor>(this);
        }

        public override void UpdateInDB(DatabaseManager databaseManager)
        {
            databaseManager.Database.Update<Doctor>(this);
        }
    }
}
