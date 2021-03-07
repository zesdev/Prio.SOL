using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prio.BL.Database
{
    public class PrioContext : IPrioContext
    {
        public IMongoDatabase Database { get; set; }
        public IMongoClient Client { get; set; }
        public PrioContext(string username, string password)
        {
            Client = new MongoClient("mongodb+srv://" + username + ":" + password + "@cluster0.mswfg.mongodb.net/BildStudionDV?retryWrites=true&w=majority");
            Database = Client.GetDatabase("Prio");
        }
    }
}
