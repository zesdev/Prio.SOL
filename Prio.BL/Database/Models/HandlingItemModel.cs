using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prio.BL.Database.Models
{
    public class HandlingItemModel
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Antal { get; set; }
        public int Key { get; set; }
    }
}
