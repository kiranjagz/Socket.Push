using MongoDB.Driver;
using Socket.Push.Mongo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Mongo
{
    public class MongoRepository : IMongoRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoSetting _mongoSettings;

        public MongoRepository(IMongoSetting mongoSettings)
        {
            _mongoSettings = mongoSettings;
            _mongoClient = new MongoClient(_mongoSettings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_mongoSettings.Database);
        }

        public async Task<List<PlayerDbModel>> GetPlayers()
        {
            try
            {
                var col = _mongoDatabase.GetCollection<PlayerDbModel>(_mongoSettings.CollectionName);
                var players = await col.Find(x => true).ToListAsync();
                return players;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePlayer(PlayerDbModel playerDbModel)
        {
            try
            {
                var filter = Builders<PlayerDbModel>.Filter.Eq(x => x.UserId, playerDbModel.UserId);
                var update = Builders<PlayerDbModel>.Update.Inc(x => x.TotalWagers, playerDbModel.TotalWagers);
                var options = new FindOneAndUpdateOptions<PlayerDbModel> { IsUpsert = true };
                var col = _mongoDatabase.GetCollection<PlayerDbModel>(_mongoSettings.CollectionName);
                var player = await col.UpdateOneAsync(filter, update);
                return player.IsAcknowledged;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
