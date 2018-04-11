using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAuthenticationClient;

namespace WpfClient.DAL
{
    public class GameScoreRepository : IGameScore<GameScoreObject>
    {
        public string baseAddress { get {return ConfigurationManager.AppSettings["EndPoint"]; } }
        public GameScoreRepository()
        {
            PlayerAuthentication.baseWebAddress = ConfigurationManager.AppSettings["EndPoint"];
        }


        public List<GameScoreObject> getGameScores(int count, string GameName)
        {
            return PlayerAuthentication.getScores(count, GameName);
        }

        public List<GameScoreObject> getEntities()
        {
            throw new NotImplementedException();
        }

        public GameScoreObject getEntity(int id)
        {
            throw new NotImplementedException();
        }

        public bool PostEntity(GameScoreObject entity)
        {
            throw new NotImplementedException();
        }

        public bool PutEntity(GameScoreObject entity)
        {
            throw new NotImplementedException();
        }
    }
}
