using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAuthenticationClient;

namespace WpfClient.DAL
{
    public interface IGameScore<T> : IRepo<T> 
    {
        List<GameScoreObject> getGameScores(int count, string GameName);

    }
}
