using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    public class TwitterProvider
    {
        private readonly OAuthRequest _request;

        public TwitterProvider()
        {
            _request = new OAuthRequest();
        }

        public async Task<IEnumerable<TwitterSchema>> GetUserTimeLineAsync(string screenName, OAuthTokens tokens)
        {
            try
            {
                var uri = new Uri(string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}", screenName));
                var rawResult = await _request.ExecuteAsync(uri, tokens);

                var result = JsonConvert.DeserializeObject<TwitterTimeLineItem[]>(rawResult);

                return result
                        .Select(r => new TwitterSchema(r))
                        .ToList();
            }
            catch (WebException wex)
            {
                HttpWebResponse response = wex.Response as HttpWebResponse;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return GenerateErrorTweet(string.Format("User '{0}' not found", screenName));
                    }
                    if ((int)response.StatusCode == 429)
                    {
                        return GenerateErrorTweet("Too many requests. Please refresh in a few minutes.");
                    }
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return GenerateErrorTweet("The keys provided have been revoked or deleted.");
                    }
                }
                throw;
            }
        }

        public async Task<IEnumerable<TwitterSchema>> GetHomeTimeLineAsync(OAuthTokens tokens)
        {
            try
            {
                var uri = new Uri("https://api.twitter.com/1.1/statuses/home_timeline.json");
                var rawResult = await _request.ExecuteAsync(uri, tokens);

                var result = JsonConvert.DeserializeObject<TwitterTimeLineItem[]>(rawResult);

                return result
                        .Select(r => new TwitterSchema(r))
                        .ToList();
            }
            catch (WebException wex)
            {
                HttpWebResponse response = wex.Response as HttpWebResponse;
                if (response != null)
                {
                    if ((int)response.StatusCode == 429)
                    {
                        return GenerateErrorTweet("Too many requests. Please refresh in a few minutes.");
                    }
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return GenerateErrorTweet("The keys provided have been revoked or deleted.");
                    }
                }
                throw;
            }
        }

        public async Task<IEnumerable<TwitterSchema>> SearchAsync(string hashTag, OAuthTokens tokens)
        {
            try
            {
                var uri = new Uri(string.Format("https://api.twitter.com/1.1/search/tweets.json?q={0}", Uri.EscapeDataString(hashTag)));
                var rawResult = await _request.ExecuteAsync(uri, tokens);

                var result = JsonConvert.DeserializeObject<TwitterSearchResult>(rawResult);

                return result.statuses
                                .Select(r => new TwitterSchema(r))
                                .ToList();
            }
            catch (WebException wex)
            {
                HttpWebResponse response = wex.Response as HttpWebResponse;
                if (response != null)
                {
                    if ((int)response.StatusCode == 429)
                    {
                        return GenerateErrorTweet("Too many requests. Please refresh in a few minutes.");
                    }
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return GenerateErrorTweet("The keys provided have been revoked or deleted.");
                    }
                }
                throw;
            }
        }

        private static IEnumerable<TwitterSchema> GenerateErrorTweet(string text)
        {
            return new TwitterSchema[]
                        {
                            new TwitterSchema()
                            {
                                CreationDateTime = DateTime.Now,
                                Text = text,
                                Id = string.Empty,
                                UserId = string.Empty,
                                UserName = string.Empty,
                                UserScreenName = string.Empty,
                                UserProfileImageUrl = "ms-appx:///Assets/ErrorImage.png",
                                Url = null
                            }
                        };
        }
    }
}
