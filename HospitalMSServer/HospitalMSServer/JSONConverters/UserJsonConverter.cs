using HospitalMSServer.Models.Database;
using Newtonsoft.Json.Linq;
using System;

namespace HospitalMSServer.JSONConverters
{
    public class UserJsonConverter: JsonCreationConverter<User>
    {
        protected override User Create(Type objectType, JObject jObject)
        {
            if (jObject == null)
            {
                throw new ArgumentNullException("jObject");
            }

            if (jObject["UserType"].ToString() == UserType.PATIENT)
            {
                return new Patient();
            }
            else
            {
                return new Staff();
            }
        }
    }
}
