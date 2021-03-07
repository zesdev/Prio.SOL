using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prio.BL.Database.Models
{
    public class AktivitetModel
    {
        public ObjectId Id { get; set; }
        public string TitelPåAktivitet { get; set; }
        public bool IsDailyReoccurence { get; set; }
        public int Prioritet { get; set; }
        public int Key { get; set; }
    }
}
