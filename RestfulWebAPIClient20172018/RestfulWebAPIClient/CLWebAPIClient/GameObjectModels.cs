using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPIAuthenticationClient
{
    public class ExternalGameObject
    {
        string name;
        string cover;
        string summary;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Cover
        {
            get
            {
                return cover;
            }

            set
            {
                cover = value;
            }
        }

        public string Summary
        {
            get
            {
                return summary;
            }

            set
            {
                summary = value;
            }
        }
    }

    public class PlayerProfile
        {
        private string id;
        private string gamerTag;
        private string email;
        private string userName;
        private int xP;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string GamerTag
        {
            get
            {
                return gamerTag;
            }

            set
            {
                gamerTag = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public int XP
        {
            get
            {
                return xP;
            }

            set
            {
                xP = value;
            }
        }
    }

    public class GameObject
    {

        public int GameID { get; set; }

        public string GameName { get; set; }


    }
    public class GameScoreObject
    {
        public int GameId { get; set; }

        public string GameName { get; set; }

        public string GamerTag { get; set; }

        public int score { get; set; }

    }

    [Serializable]
    public class PlayerScoreObject
    {
        public int GameId { get; set; }

        public string PlayerId { get; set; }

        public int score { get; set; }

        public override string ToString()
        {
            return GameId.ToString() + " " + PlayerId + " " + score.ToString();
        }
    }

}
