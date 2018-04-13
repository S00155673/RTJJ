using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIAuthenticationClient;
//using WebAPIAuthenticationClient;

namespace ConsoleWebAPIClient
{
    class Program
    {
        static public Random r = new Random();
        static void Main(string[] args)
        {
            PlayerProfile currentPlayer;

            //Added the URL for the Web App that is depolyed to Azure...
            PlayerAuthentication.baseWebAddress = "http://rtjjmonogameserver.azurewebsites.net/";// "http://localhost:50574/";
            bool logged = PlayerAuthentication.login("powell.paul@itsligo.ie", "itsPaul$1");
            if (logged)
            {
                currentPlayer = PlayerAuthentication.getPlayerProfile();
                //Console.WriteLine("Token acquired {0}", PlayerAuthentication.PlayerToken);
                List<GameScoreObject> scores = PlayerAuthentication.getScores(4, "Battle Call");
                foreach (var item in scores)
                {
                    Console.WriteLine("Game {0} {1} Score for {1} is {3}", item.GameId, item.GameName, item.GamerTag, item.score);
                }
                if(currentPlayer != null)
                {
                    PlayerAuthentication.PostScore(new PlayerScoreObject
                        { GameId = scores.First().GameId, PlayerId = currentPlayer.Id, score = r.Next(900,1000) });
                }
                Console.WriteLine("Top 4 scores After New score Added");
                foreach (var item in PlayerAuthentication.getScores(4, "Battle Call"))
                {
                    Console.WriteLine("After New score Game {0} {1} Score for {1} is {3}", item.GameId, item.GameName, item.GamerTag, item.score);
                }

            }
            else
            {
                Console.WriteLine("Failed to acquire Token  ");
            }
            ExternalGameObject gameInfo = PlayerAuthentication.getExtGame(1);
            Console.WriteLine(" Name: {0} \n Summary {1} \n Image URL : {2}",
                                gameInfo.Name, gameInfo.Summary, gameInfo.Cover);


            Console.ReadKey();
        }

    }
}
