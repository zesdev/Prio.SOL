using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prio.BL.Database.Models
{
    public class ToDoModel
    {
        public ObjectId Id { get; set; }
        public int AktivitetKey { get; set; }
        public bool IsDone { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
