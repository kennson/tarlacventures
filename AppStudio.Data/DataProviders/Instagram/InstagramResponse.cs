using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class InstagramResponse
    {
        public InstagramResponseItem[] data { get; set; }

        public IEnumerable<InstagramSchema> ToSchema()
        {
            return this.data.Select(d => d.ToSchema());
        }
    }

    public class InstagramResponseItem
    {
        private readonly DateTime UnixEpochDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public string id { get; set; }
        public string link { get; set; }
        public int created_time { get; set; }
        public InstagramUser user { get; set; }
        public InstagramImages images { get; set; }
        public InstagramCaption caption { get; set; }

        public InstagramSchema ToSchema()
        {
            var result = new InstagramSchema();

            result.Id = this.id;
            result.SourceUrl = this.link;
            result.Published = UnixEpochDate.AddSeconds(this.created_time);
            if (this.user != null)
            {
                result.Author = this.user.username;
            }
            result.ThumbnailUrl = this.images.thumbnail.url;
            result.ImageUrl = this.images.standard_resolution.url;
            if (this.caption != null)
            {
                result.Title = this.caption.text;
            }

            return result;
        }
    }

    public class InstagramUser
    {
        public string username { get; set; }
    }

    public class InstagramImages
    {
        public InstagramImage thumbnail { get; set; }
        public InstagramImage standard_resolution { get; set; }
    }

    public class InstagramImage
    {
        public string url { get; set; }
    }

    public class InstagramCaption
    {
        public string text { get; set; }
    }
}
