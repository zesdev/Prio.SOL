using MongoDB.Bson;
using MongoDB.Driver;
using Prio.BL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prio.BL.Database.ModelLogic
{
    public class TodoLogic : ITodoLogic
    {
        PrioContext context;
        IMongoCollection<ToDoModel> toDoDb;

        public TodoLogic(PrioContext _context)
        {
            context = _context;
            toDoDb = context.Database.GetCollection<ToDoModel>("todos");
        }

        public List<ToDoModel> GetDailyToDos()
        {
            var returningList = toDoDb.Find<ToDoModel>(x => true).ToList();
            returningList = returningList.Where(x => x.DateCreated.Year == DateTime.Now.Year && x.DateCreated.DayOfYear == DateTime.Now.DayOfYear).ToList();
            return returningList;
        }

        public void AddNewToDo(ToDoModel model)
        {
            model.DateCreated = DateTime.Now;
            toDoDb.InsertOne(model);
        }
        public void UpdateToDo(int aktivitetKey)
        {
            var models = toDoDb.Find<ToDoModel>(x => x.AktivitetKey == aktivitetKey).ToList();
            var model = models.Where(x => x.DateCreated.DayOfYear == DateTime.Now.DayOfYear && x.DateCreated.Year == DateTime.Now.Year).First();
            if (model != null)
            {
                UpdateProperty("IsDone", model.Id, Boolean.TrueString);
            }
        }
        private void UpdateProperty(string property, ObjectId IdOfItemBeingEdited, string newpropertyContent)
        {
            var filter = Builders<ToDoModel>.Filter.Eq("Id", IdOfItemBeingEdited);
            var update = Builders<ToDoModel>.Update.Set(property, newpropertyContent);
            toDoDb.UpdateOne(filter, update);
        }
        public void RemoveAllToDosForAktivitet(int key)
        {
            var toDoModelsToRemove = toDoDb.Find<ToDoModel>(x => x.AktivitetKey == key).ToList();
            foreach (var model in toDoModelsToRemove)
            {
                toDoDb.FindOneAndDelete<ToDoModel>(x => x.Id == model.Id);
            }
        }
    }
}
