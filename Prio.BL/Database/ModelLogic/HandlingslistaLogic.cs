using MongoDB.Driver;
using Prio.BL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prio.BL.Database.ModelLogic
{
    public class HandlingslistaLogic : IHandlingslistaLogic
    {
        PrioContext context;
        IMongoCollection<HandlingItemModel> handlingDb;

        public HandlingslistaLogic(PrioContext _context)
        {
            context = _context;
            handlingDb = context.Database.GetCollection<HandlingItemModel>("handlingItems");
        }

        public void AddNewHandlingItem(HandlingItemModel model)
        {
            var key = 0;
            try
            {
                key = GetItems().LastOrDefault().Key + 1;
            }
            catch
            {
            }
            model.Key = key;

            handlingDb.InsertOne(model);
        }
        public void RemoveItem(int key)
        {
            handlingDb.FindOneAndDelete<HandlingItemModel>(x => x.Key == key);

        }
        public List<HandlingItemModel> GetItems()
        {
            return handlingDb.Find(x => true).ToList();
        }

        public void UpdateItem(HandlingItemModel model)
        {
            UpdateProperty("Antal", model.Key, model.Antal);
            UpdateProperty("Title", model.Key, model.Title);
        }
        private void UpdateProperty(string property, int IdOfItemBeingEdited, string newpropertyContent)
        {
            var filter = Builders<HandlingItemModel>.Filter.Eq("Key", IdOfItemBeingEdited);
            var update = Builders<HandlingItemModel>.Update.Set(property, newpropertyContent);
            handlingDb.UpdateOne(filter, update);
        }
    }
}
