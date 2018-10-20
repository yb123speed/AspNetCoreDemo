using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBSample.Models
{
    public class ActionLog
    {
        public ObjectId _id { get; set; }
        public Guid ActionLogId { get; set; }
        public Guid CompanyId { get; set; }
        public string Context { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
