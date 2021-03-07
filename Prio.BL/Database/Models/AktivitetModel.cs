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
        public bool Måndag { get; set; }
        public bool Tisdag { get; set; }
        public bool Onsdag { get; set; }
        public bool Torsdag { get; set; }
        public bool Fredag { get; set; }
        public bool Lördag { get; set; }
        public bool Söndag { get; set; }
        public int Prioritet { get; set; }
        public int Key { get; set; }
    }
}
