using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
//using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebAPIAuthenticationClient
{
    public enum AUTHSTATUS { NONE,OK,INVALID,FAILED }


    public static class PlayerAuthentication
    {
        static public string baseWebAddress;
        static public string PlayerToken = "";
        static public AUTHSTATUS PlayerStatus = AUTHSTATUS.NONE;
        static public string IgdbUserToken = "2607d5eece3340285a339f5bc84c5426";
        static public List<GameScoreObject> getScores(int count, string Game )
            {
            using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(baseWebAddress + "api/GameScores/getTops/Count/" + count.ToString() + "/Game/" + Game + "/").Result;
                    var resultContent = response.Content.ReadAsAsync<List<GameScoreObject>>(
                        new[] { new JsonMediaTypeFormatter() }).Result;
                    return resultContent;
                }
            }

        static public dynamic getExtGame(int gameID)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("user-key", IgdbUserToken);
                var response = client.GetAsync("https://api-endpoint.igdb.com/games/" + gameID.ToString()).Result;
                var resultContent = response.Content.ReadAsAsync<JToken>(
                    new[] { new JsonMediaTypeFormatter() }).Result;


                var jname = resultContent.Children()["name"].Values<string>().FirstOrDefault();
                var jsummary = resultContent.Children()["summary"].Values<string>().FirstOrDefault();
                // url is nested in cover object
                var jcover = resultContent.Children()["cover"]["url"].Values<string>().FirstOrDefault();
                ExternalGameObject eobj = 
                                        new ExternalGameObject
                                        {
                                            Name = jname,
                                            Summary = jsummary,
                                            Cover = jcover
                                        };


                
                return eobj;
            }
        }

        static public PlayerProfile getPlayerProfile()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",PlayerToken);
                var response = client.GetAsync(baseWebAddress + "api/GameScores/playerInfo").Result;
                var resultContent = response.Content.ReadAsAsync<PlayerProfile>(
                    new[] { new JsonMediaTypeFormatter() }).Result;
                return resultContent;
            }
        }

        // Post a score for the current player  
        
        static public bool PostScore(PlayerScoreObject g)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync(baseWebAddress + "api/GameScores/postScore",g).Result;
                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Response Object is " + response.Content.ReadAsAsync<PlayerScoreObject>(
                        new[] { new JsonMediaTypeFormatter() }).Result.ToString());
                    return true;
                }
                return false;
            }

        }
    static public bool login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password),
                    });
                var result = client.PostAsync(baseWebAddress + "Token", content).Result;
                try
                {
                    var resultContent = result.Content.ReadAsAsync<Token>(
                        new[] { new JsonMediaTypeFormatter() }
                        ).Result;
                    string ServerError = string.Empty;
                    if (!(String.IsNullOrEmpty(resultContent.AccessToken)))
                    {
                        Console.WriteLine(resultContent.AccessToken);
                        PlayerToken = resultContent.AccessToken;
                        PlayerStatus = AUTHSTATUS.OK;
                        return true;
                    }
                    else
                    {
                        PlayerToken = "Invalid Login" ;
                        PlayerStatus = AUTHSTATUS.INVALID;
                        Console.WriteLine("Invalid credentials");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    PlayerStatus = AUTHSTATUS.FAILED;
                    PlayerToken = "Server Error -> " + ex.Message;
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }

        

}
}
