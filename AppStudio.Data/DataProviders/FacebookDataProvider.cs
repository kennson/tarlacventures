using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Xml.Linq;

namespace AppStudio.Data
{
    public class FacebookDataProvider
    {
        private const string USER_RSS_URL = "https://www.facebook.com/feeds/page.php?id={0}&format=rss20";
        private Uri _uri;

        public FacebookDataProvider(string userID)
        {
            _uri = new Uri(string.Format(USER_RSS_URL, userID));
        }

        public async Task<ObservableCollection<FacebookSchema>> Load()
        {
            var rssProvider = new RssDataProvider(_uri.ToString(), "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");

            var rssSchemaList = await rssProvider.Load();
            var result = new ObservableCollection<FacebookSchema>
                (
                    rssSchemaList.Select(rss => new FacebookSchema()
                    {
                        Author = rss.Author,
                        Content = rss.Content,
                        FeedUrl = rss.FeedUrl,
                        Id = rss.Id,
                        ImageUrl = this.ConvertImageUrlFromParameter(rss.ImageUrl),
                        PublishDate = rss.PublishDate,
                        Summary = rss.Summary,
                        Title = rss.Title
                    })
                    .OrderByDescending(f => f.PublishDate)
                );

            return result;
        }

        private string ConvertImageUrlFromParameter(string imageUrl)
        {
            string parsedImageUrl = null;
            try
            {
                if (!string.IsNullOrEmpty(imageUrl) && imageUrl.IndexOf("url=") > 0)
                {
                    Uri imageUri = new Uri(imageUrl);
                    var imageUriQuery = imageUri.Query.Replace("?", "").Replace("&amp;", "&");

                    var imageUriQueryParameters = imageUriQuery.Split('&').Select(q => q.Split('='))
                           .Where(s => s != null && s.Length >= 2)
                           .ToDictionary(k => k[0], v => v[1]);

                    parsedImageUrl = WebUtility.UrlDecode(imageUriQueryParameters["url"]);
                }
                else if (!string.IsNullOrEmpty(imageUrl))
                {
                    parsedImageUrl = WebUtility.HtmlDecode(imageUrl);
                }
            }
            catch
            {
            }

            return parsedImageUrl;
        }
    }
}
