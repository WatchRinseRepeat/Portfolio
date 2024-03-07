using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using TwitchOverViewer.Models;

namespace TwitchOverViewer.Controllers
{
    public class TwitchAPIClient

    {
        private readonly HttpClient _httpClient;
        
        public TwitchAPIClient() 
        { 
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Client-Id", "");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer ");
            //_httpClient.BaseAddress = new Uri("https://api.twitch.tv/helix/");
        }

        public TwitchAPIClient(string clientID, string oAuthToken) 
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Client-Id", clientID);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {oAuthToken}");
            
        }

        public async Task<string> GetChannelBadgeInfo(string broadcasterId, KeyValuePair<string, string> badges)
        {
            var response = await _httpClient.GetAsync($"https://api.twitch.tv/helix/chat/badges?broadcaster_id={broadcasterId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                JObject parsed = JObject.Parse(jsonResponse);

                if (parsed != null)
                {
                    //find the subscriber element in the data array
                    var subscriber = parsed["data"].FirstOrDefault(x => (string)x["set_id"] == badges.Key);

                    if (subscriber != null)
                    {
                        //find the version based on the badge value
                        var version = subscriber["versions"].FirstOrDefault(x => (string)x["id"] == badges.Value);

                        if (version != null)
                        {
                            //Get the image url
                            string badgeinfo = (string)version["image_url_1x"];

                            //Debug.WriteLine(badgeinfo);
                            return badgeinfo;
                        }
                    }
                }
            }
            else { 
                Debug.WriteLine("Bad status code on API fetch");
                Debug.WriteLine(response.StatusCode);
            }

            return "";
        }

        public async Task<string> GetGlobalBadgeInfo(KeyValuePair<string, string> badges)
        {
            var response = await _httpClient.GetAsync($"https://api.twitch.tv/helix/chat/badges/global");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                JObject parsed = JObject.Parse(jsonResponse);

                if (parsed != null)
                {
                    //find the subscriber element in the data array
                    var subscriber = parsed["data"].FirstOrDefault(x => (string)x["set_id"] == badges.Key);

                    if (subscriber != null)
                    {
                        //find the version based on the badge value
                        var version = subscriber["versions"].FirstOrDefault(x => (string)x["id"] == badges.Value);

                        if (version != null)
                        {
                            //Get the image url
                            string badgeinfo = (string)version["image_url_1x"];

                            //Debug.WriteLine(badgeinfo);
                            return badgeinfo;
                        }
                    }
                }
            }
            else { Debug.WriteLine("Bad status code on API fetch");
                Debug.WriteLine(response.StatusCode);
            }

            return "";
        }

        public async Task<string> GetBroadcasterId(string channelName)
        {
            var response = await _httpClient.GetAsync($"https://api.twitch.tv/helix/users?login={channelName}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                //Parse the json 
                JObject parsed = JObject.Parse(jsonResponse);

                //reveal the ID
                if (parsed != null)
                {
                    try
                    {
                        string broadcasterId = parsed["data"][0]["id"].ToString();

                        return broadcasterId;

                    }
                    catch { }

                }
            }
            else { Debug.WriteLine("Bad status code on API fetch");
                Debug.WriteLine(response.StatusCode);
            }

            return "";
        }
        public async Task<string> GetBroadcasterProfilePicture(string channelName)
        {
            var response = await _httpClient.GetAsync($"https://api.twitch.tv/helix/users?login={channelName}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                //Parse the json 
                JObject parsed = JObject.Parse(jsonResponse);

                //reveal the ID
                if (parsed != null)
                {
                    try
                    {
                        string broadcasterId = parsed["data"][0]["profile_image_url"].ToString();

                        return broadcasterId;

                    }
                    catch { }

                }
            }
            else { Debug.WriteLine("Bad status code on API fetch");
                Debug.WriteLine(response.StatusCode);
            }

            return "";
        }


        public async Task<List<TwitchChannel>> GetFollowedChannels(string userID)
        {
            List<TwitchChannel> channels = new List<TwitchChannel>();

            //request followed for userId
            var response = await _httpClient.GetAsync($"https://api.twitch.tv/helix/streams/followed?user_id={userID}");
            //Cursor for pagination
            string? cursor;

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                if (jsonResponse != null)
                {
                    //Create Root Object to hold the TwitchChannel objects
                    Root root = JsonConvert.DeserializeObject<Root>(jsonResponse);

                    //For each TwitchChannel object put it into the List
                    foreach (TwitchChannel channel in root.data)
                    {
                        channels.Add(channel);
                        Debug.WriteLine("User Name: " + channel.user_name);
                    }

                    //Handle pagination
                    var parsedJson = JObject.Parse(jsonResponse);
                    cursor = parsedJson["pagination"]["cursor"]?.ToString();

                    while (cursor != null)
                    {
                        response = await _httpClient.GetAsync($"https://api.twitch.tv/helix/streams/followed?user_id={userID}?after={cursor}");
                        jsonResponse = await response.Content.ReadAsStringAsync();
                        if (jsonResponse != null)
                        {
                            root = JsonConvert.DeserializeObject<Root>(jsonResponse);

                            foreach (TwitchChannel channel in root.data)
                            {
                                channels.Add(channel);
                                Debug.WriteLine("User Name: " + channel.user_name);
                            }

                            parsedJson = JObject.Parse(jsonResponse);
                            cursor = parsedJson["pagination"]["cursor"]?.ToString();
                        }

                    }
                }
            }
            else { Debug.WriteLine("Bad status code on API fetch");
                Debug.WriteLine(response.StatusCode);
            }

            return channels;
        }

        public string GetUserToken (string username)
        {



            return "";
        }
    }
}
