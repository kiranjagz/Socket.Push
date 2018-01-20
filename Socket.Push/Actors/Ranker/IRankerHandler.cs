using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Actors.Ranker
{
    public interface IRankerHandler
    {
        double CalculatePoints(double totalWagers);
        Task<List<RankedPlayersModel>> RankedPlayers();
    }
}
