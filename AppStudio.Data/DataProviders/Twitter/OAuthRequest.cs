using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class OAuthRequest
    {
        public async Task<string> ExecuteAsync(Uri requestUri, OAuthTokens tokens)
        {
            var request = CreateRequest(requestUri, tokens);
            var response = await request.GetResponseAsync();

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        private static WebRequest CreateRequest(Uri requestUri, OAuthTokens tokens)
        {
            var requestBuilder = new OAuthRequestBuilder(requestUri, tokens);

            var request = (HttpWebRequest)WebRequest.Create(requestBuilder.EncodedRequestUri);

            request.UseDefaultCredentials = true;
            request.Method = OAuthRequestBuilder.Verb;
            request.Headers["Authorization"] = requestBuilder.AuthorizationHeader;

            return request;
        }
    }
}
