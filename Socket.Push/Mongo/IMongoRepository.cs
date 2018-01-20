using Socket.Push.Mongo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Mongo
{
    public interface IMongoRepository
    {
        Task<List<PlayerDbModel>> GetPlayers();

        Task<bool> UpdatePlayer(PlayerDbModel playerDbModel);
    }
}
