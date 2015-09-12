using E203.NOpenSRS.Enums;
using E203.NOpenSRS.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace E203.NOpenSRS
{
    public class OpenSrsClient : IOpenSrsClient
    {
        private const string LiveUri = "https://rr-n1-tor.opensrs.net:55443";
        private const string TestUri = "https://horizon.opensrs.net:55443";

        public HttpClient HttpClient { get; private set; }
        public OpenSrsMode OpenSrsMode { get; set; }
        public string Username { get; set; }
        public string ApiKey { get; set; }

        #region Constructors

        public OpenSrsClient(OpenSrsMode openSrsMode, string username, string apiKey)
        {
            HttpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });
            OpenSrsMode = openSrsMode;
            Username = username;
            ApiKey = apiKey;
        }

        public OpenSrsClient(OpenSrsMode openSrsMode, string username, string apiKey, HttpMessageHandler httpMessageHandler)
        {
            HttpClient = new HttpClient(httpMessageHandler);
            OpenSrsMode = openSrsMode;
            Username = username;
            ApiKey = apiKey;
        }

        public OpenSrsClient() : this(OpenSrsMode.Test, string.Empty, string.Empty) { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sends a request asynchronously to the OpenSRS API.
        /// </summary>
        /// <param name="payload">The XML payload to send to the OpenSRS API.</param>
        /// <returns>
        /// A parsed response object
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<OpenSrsResponse> SendRequestAsync(string payload)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Forms and posts the request to the OpenSRS API.
        /// </summary>
        /// <param name="request">The request payload.</param>
        /// <returns>A response string from the API.</returns>
        private async Task<string> PostRequestAsync(string request)
        {
            // Determine which URI to call
            var uri = OpenSrsMode == OpenSrsMode.Live ? LiveUri : TestUri;

            // Build the content payload
            var content = new ByteArrayContent(Encoding.UTF8.GetBytes(request));
            content.Headers.Add("Content-Type", "text/xml");
            content.Headers.Add("X-Username", Username);
            content.Headers.Add("X-Signature", MD5.GetHashString(MD5.GetHashString(request + ApiKey) + ApiKey));
            
            // Send and parse the response
            var response = await HttpClient.PostAsync(uri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        #endregion
    }
}
