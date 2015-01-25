using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppStudio.Data
{
    public class InstagramDataProvider
    {
        private const string URL = "https://api.instagram.com/v1/tags/{0}/media/recent?client_id={1}";
        private const string URLUserID = "https://api.instagram.com/v1/users/{0}/media/recent?client_id={1}";

        private string _queryType;
        private string _query;
        private OAuthTokens _tokens;

        public InstagramDataProvider(string queryType, string query, OAuthTokens tokens)
        {
            _queryType = queryType;
            _query = query;
            _tokens = tokens;
        }

        public async Task<ObservableCollection<InstagramSchema>> Load()
        {
            try
            {
                if (_tokens == null)
                {
                    return new ObservableCollection<InstagramSchema>(new InstagramSchema[0]);
                }
                var url = string.Format(_queryType == "tag" ? URL : URLUserID, _query, _tokens["ClientId"]);
                var client = new HttpClient();
                var rawResponse = await client.GetStringAsync(url);
                var response = JsonConvert.DeserializeObject<InstagramResponse>(rawResponse);

                return new ObservableCollection<InstagramSchema>(response.ToSchema().ToList());
            }
            catch (HttpRequestException)
            {
                return new ObservableCollection<InstagramSchema>(GenerateErrorInstagram("The keys provided have been revoked or deleted."));
            }
        }

        private static IEnumerable<InstagramSchema> GenerateErrorInstagram(string text)
        {
            yield return new InstagramSchema()
            {
                Title = text,
                SourceUrl = null,
                ThumbnailUrl = "ms-appx:///Assets/ErrorImage.png",
                ImageUrl = "ms-appx:///Assets/ErrorImage.png",
                Published = DateTime.Now
            };
        }
    }
}
