﻿using ArangoDB.Client;
using HospitalMSServer.Helpers;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "User", Naming = NamingConvention.UnChanged)]
    public class Staff : User
    {
        public int Salary { get; set; }
        public string History { get; set; }

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
