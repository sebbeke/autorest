using MongoDB.Driver;

namespace AutoRest.Mongo
{
    public class MongoConfiguration
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        public IMongoClient GetClient()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                return new MongoClient(Server);
            var credentials = MongoCredential.CreateCredential(Database, Username, Password);
            var settings = new MongoClientSettings()
            {
                Credential = credentials,
                Server = MongoServerAddress.Parse(Server.Replace("mongodb://", ""))
            };
            return new MongoClient(settings);
        }

        public IMongoDatabase GetDatabase()
        {
            return GetClient().GetDatabase(Database);
        }
    }
}
