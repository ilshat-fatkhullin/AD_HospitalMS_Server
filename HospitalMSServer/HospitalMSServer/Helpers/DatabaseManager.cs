using ArangoDB.Client;

namespace HospitalMSServer.Helpers
{
    public class DatabaseManager
    {
        public ArangoDatabase Database { get; private set; }

        public DatabaseManager()
        {
            DatabaseSharedSetting setting = new DatabaseSharedSetting();
            setting.Credential.Password = "root";
            setting.Credential.UserName = "root";
            setting.Database = "hospital";
            setting.Url = "http://95.213.191.243:8529";
            Database = new ArangoDatabase(setting);
        }
    }
}
