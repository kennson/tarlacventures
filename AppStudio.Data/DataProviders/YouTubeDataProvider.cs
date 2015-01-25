using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppStudio.Data
{
    /// <summary>
    /// YouTube data provider class
    /// </summary>
    public class YouTubeDataProvider
    {
        private Uri _uri;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url"></param>
        public YouTubeDataProvider(string url)
        {
            _uri = new Uri(url);
        }

        /// <summary>
        /// Starts loading the feed and parse the response.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<YouTubeSchema>> Load()
        {
            string xmlContent = await DownloadAsync(_uri);
            var atoms = XNamespace.Get("http://www.w3.org/2005/Atom");
            var medians = XNamespace.Get("http://search.yahoo.com/mrss/");
            var yt = XNamespace.Get("http://gdata.youtube.com/schemas/2007");

            var doc = XDocument.Parse(xmlContent);

            var result = (from entry in doc.Descendants(atoms.GetName("entry"))
                          select new YouTubeSchema()
                          {
                              VideoId = entry.Descendants(yt.GetName("videoid")).FirstOrDefault().Value,
                              Title = GetYouTubeTitle(atoms, entry, medians),
                              Summary = GetYouTubeSummary(atoms, entry, medians),
                              VideoUrl = GetVideoUrl(yt, entry),
                              ImageUrl = entry.Descendants(medians.GetName("thumbnail")).Select(thumbnail => thumbnail.Attribute("url").Value).FirstOrDefault(),
                              Published = GetPublishDate(atoms, entry)
                          }).ToArray();

            return result;
        }

        private DateTime GetPublishDate(XNamespace atoms, XElement entry)
        {
            DateTime publish = DateTime.MinValue;
            if (entry.Element(atoms.GetName("published")) != null && entry.Element(atoms.GetName("published")).Value != null)
            {
                DateTime.TryParse(entry.Element(atoms.GetName("published")).Value, out publish);
            }

            return publish;
        }

        private async Task<string> DownloadAsync(Uri url)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(url.AbsoluteUri);
        }

        private string GetVideoUrl(XNamespace yt, XElement entry)
        {
            string videoUrl = string.Empty;
            string id = string.Empty;
            if (entry.Element(yt.GetName("videoid")) != null && entry.Element(yt.GetName("videoid")).Value != null)
                id = entry.Element(yt.GetName("videoid")).Value.Split(':').Last();

            videoUrl = String.Format(CultureInfo.InvariantCulture, "http://gdata.youtube.com/feeds/api/videos/{0}", id);

            return videoUrl;
        }

        private string GetYouTubeTitle(XNamespace atoms, XElement entry, XNamespace medians)
        {
            string title = string.Empty;

            if (entry.Element(atoms.GetName("title")) != null)
                title = entry.Element(atoms.GetName("title")).Value;

            return title;
        }

        private string GetYouTubeSummary(XNamespace atoms, XElement entry, XNamespace medians)
        {
            string summary = string.Empty;
            if (entry.Element(atoms.GetName("summary")) != null)
                summary = entry.Element(atoms.GetName("summary")).Value;

            if (string.IsNullOrEmpty(summary))
                if (entry.Element(atoms.GetName("content")) != null)
                    summary = entry.Element(atoms.GetName("content")).Value;

            if (string.IsNullOrEmpty(summary))
                if (entry.Element(medians.GetName("group")) != null && entry.Element(medians.GetName("group")).Element(medians.GetName("description")) != null)
                    summary = entry.Element(medians.GetName("group")).Element(medians.GetName("description")).Value;

            return summary;
        }
    }
}
