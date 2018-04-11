using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAuthenticationClient;

namespace WpfClient.DAL
{
    class PlayerRepository : IPlayer<PlayerProfile>
    {
        public string baseAddress { get { return ConfigurationManager.AppSettings["EndPoint"]; } }
        private PlayerProfile currentPlayer;
        public string Token
        {
            get
            {
                return _token;
            }

            set
            {
                _token = value;
            }
        }

        private string _token;
        public PlayerRepository()
        {
            PlayerAuthentication.baseWebAddress = ConfigurationManager.AppSettings["EndPoint"];
            
        }
        public List<PlayerProfile> getEntities()
        {
            throw new NotImplementedException();
        }

        public PlayerProfile getEntity(int id)
        {
            throw new NotImplementedException();
        }

        public bool PostEntity(PlayerProfile entity)
        {
            throw new NotImplementedException();
        }

        public bool PutEntity(PlayerProfile entity)
        {
            throw new NotImplementedException();
        }
        public PlayerProfile getCurrentPlayer()
        {
            if (currentPlayer != null)
                return currentPlayer;

            throw new NullReferenceException ();
        }

        public bool login(string username, string password)
        {
            if(PlayerAuthentication.login(username, password))
            {
                _token = PlayerAuthentication.PlayerToken;
                currentPlayer = PlayerAuthentication.getPlayerProfile();
                return true;
            }
            return false;
        }
    }
}
