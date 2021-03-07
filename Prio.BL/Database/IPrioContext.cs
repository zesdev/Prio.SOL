using MongoDB.Driver;

namespace Prio.BL.Database
{
    public interface IPrioContext
    {
        IMongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
    }
}