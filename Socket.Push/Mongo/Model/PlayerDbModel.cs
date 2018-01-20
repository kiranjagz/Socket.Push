using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Mongo.Model
{
    public class PlayerDbModel
    {
        public ObjectId _id;
        public int UserId { get; set; }
        public int TotalWagers { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Game { get; set; }
        public string Brand { get; set; }
    }
}
