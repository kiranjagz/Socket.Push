using Socket.Push.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Actors.Ranker
{
    public class RankerHandler : IRankerHandler
    {
        private IMongoRepository _mongoRepository;
        public RankerHandler(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<List<RankedPlayersModel>> RankedPlayers()
        {
            var players = await _mongoRepository.GetPlayers();
            var rankedPlayers = players.Select(x => new RankedPlayersModel
            {
                UserId = x.UserId,
                Points = Convert.ToInt32(CalculatePoints(x.TotalWagers)),
                Name = x.Name,
                Surname = x.Surname,
                Game = x.Game,
                Brand = x.Brand,
                TotalWagers = x.TotalWagers
            }).OrderByDescending(x => x.Points).ToList();

            foreach (var rankPlayer in rankedPlayers.Select((value, index) => new { Value = value, Index = index }))
            {
                var rank = rankPlayer.Index + 1;
                rankPlayer.Value.Rank = rank;
            }

            return rankedPlayers;
        }

        public double CalculatePoints(double totalWagers)
        {
            return (totalWagers * 0.009) * Math.PI;
        }
    }
}
