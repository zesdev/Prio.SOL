﻿using MongoDB.Bson;
using MongoDB.Driver;
using Prio.BL.Database.Models;
using System;
using System.Collections.Generic;
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
            var key = GetAllAktiviteter().Count + 1;
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
            UpdateProperty("IsDailyReoccurence", oldModel.Id, model.IsDailyReoccurence.ToString());
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