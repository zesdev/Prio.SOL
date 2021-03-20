using MongoDB.Bson;
using MongoDB.Driver;
using Prio.BL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prio.BL.Database.ModelLogic
{
    public class AktivitetLogic : IAktivitetLogic
    {
        PrioContext context;
        IMongoCollection<AktivitetModel> aktivitetDb;
        TodoLogic toDoLogic;

        public AktivitetLogic(PrioContext _context, TodoLogic _toDoLogic)
        {
            toDoLogic = _toDoLogic;
            context = _context;
            aktivitetDb = context.Database.GetCollection<AktivitetModel>("aktiviteter");
        }

        public List<AktivitetModel> GetAllAktiviteter()
        {
            var models = aktivitetDb.Find<AktivitetModel>(x => true).ToList();
            return models;
        }
        public void AddNewAktivitet(AktivitetModel model)
        {
            var key = 0;
            try
            {
                key = GetAllAktiviteter().LastOrDefault().Key+1;
            }
            catch
            {
            }
            model.Key = key;
            aktivitetDb.InsertOne(model);
        }
        public void RemoveAktivitet(int key)
        {
            toDoLogic.RemoveAllToDosForAktivitet(key);
            aktivitetDb.FindOneAndDelete<AktivitetModel>(x => x.Key == key);
        }
        public void UpdateAktivitet(int key, AktivitetModel model)
        {
            var oldModel = aktivitetDb.Find<AktivitetModel>(x => x.Key == key).First();
            UpdateProperty("Måndag", oldModel.Id, model.Måndag.ToString());
            UpdateProperty("Tisdag", oldModel.Id, model.Tisdag.ToString());
            UpdateProperty("Onsdag", oldModel.Id, model.Onsdag.ToString());
            UpdateProperty("Torsdag", oldModel.Id, model.Torsdag.ToString());
            UpdateProperty("Fredag", oldModel.Id, model.Fredag.ToString());
            UpdateProperty("Lördag", oldModel.Id, model.Lördag.ToString());
            UpdateProperty("Söndag", oldModel.Id, model.Söndag.ToString());
            UpdateProperty("TitelPåAktivitet", oldModel.Id, model.TitelPåAktivitet.ToString());
            UpdateProperty("Prioritet", oldModel.Id, model.Prioritet.ToString());

        }
        private void UpdateProperty(string property, ObjectId IdOfItemBeingEdited, string newpropertyContent)
        {
            var filter = Builders<AktivitetModel>.Filter.Eq("Id", IdOfItemBeingEdited);
            var update = Builders<AktivitetModel>.Update.Set(property, newpropertyContent);
            aktivitetDb.UpdateOne(filter, update);
        }
    }
}
