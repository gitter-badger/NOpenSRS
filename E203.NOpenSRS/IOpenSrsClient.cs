using E203.NOpenSRS.Enums;
using E203.NOpenSRS.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace E203.NOpenSRS
{
    public interface IOpenSrsClient
    {
        HttpClient HttpClient { get; }
        OpenSrsMode OpenSrsMode { get; set; }
        string Username { get; set; }
        string ApiKey { get; set; }

        /// <summary>
        /// Sends a request asynchronously to the OpenSRS API.
        /// </summary>
        /// <param name="payload">The XML payload to send to the OpenSRS API.</param>
        /// <exception cref="System.NullReferenceException">OpenSrsMode, Username, and ApiKey cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">payload;Payload cannot be null or empty</exception>
        /// <returns>A parsed response object</returns>
        Task<OpenSrsResponse> SendRequestAsync(string payload);
    }
}
