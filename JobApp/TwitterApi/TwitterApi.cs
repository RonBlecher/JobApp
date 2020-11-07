using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobApp.Twitter
{
	class TwitterApi
	{
		const string TwitterApiBaseUrl = "https://api.twitter.com/1.1/";
		readonly string consumerKey, consumerKeySecret, accessToken, accessTokenSecret;
		readonly HMACSHA1 sigHasher;
		readonly DateTime epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Creates an object for sending tweets to Twitter using Single-user OAuth.
		public TwitterApi(string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
		{
			this.consumerKey = consumerKey;
			this.consumerKeySecret = consumerKeySecret;
			this.accessToken = accessToken;
			this.accessTokenSecret = accessTokenSecret;

			sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes(string.Format("{0}&{1}", consumerKeySecret, accessTokenSecret)));
		}

		public TwitterApi()
		{
			this.consumerKey = Environment.GetEnvironmentVariable("CONSUMER_KEY");
			this.consumerKeySecret = Environment.GetEnvironmentVariable("CONSUMER_KEY_SECRET");
			this.accessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
			this.accessTokenSecret = Environment.GetEnvironmentVariable("ACCESS_TOKEN_SECRET");

			sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes(string.Format("{0}&{1}", this.consumerKeySecret, this.accessTokenSecret)));
		}

		// Sends a tweet with the supplied text and returns the response from the Twitter API
		public Task<string> Tweet(string text)
		{
			var data = new Dictionary<string, string> {
			{ "status", text },
			{ "trim_user", "1" }
		};

			return SendRequest("statuses/update.json", data);
		}

		Task<string> SendRequest(string url, Dictionary<string, string> data)
		{
			var fullUrl = TwitterApiBaseUrl + url;

			var timestamp = (int)((DateTime.UtcNow - epochUtc).TotalSeconds);

			// Add all the OAuth headers that we'll need to use when constructing the hash.
			data.Add("oauth_consumer_key", consumerKey);
			data.Add("oauth_signature_method", "HMAC-SHA1");
			data.Add("oauth_timestamp", timestamp.ToString());
			data.Add("oauth_nonce", "a");
			data.Add("oauth_token", accessToken);
			data.Add("oauth_version", "1.0");

			// Generate the OAuth signature and add it to our payload.
			data.Add("oauth_signature", GenerateSignature(fullUrl, data));

			// Build the OAuth HTTP Header from the data.
			string oAuthHeader = GenerateOAuthHeader(data);

			// Build the form data (exclude OAuth stuff that's already in the header).
			var formData = new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith("oauth_")));

			return SendRequest(fullUrl, oAuthHeader, formData);
		}

		// Generate an OAuth signature from OAuth header values.
		string GenerateSignature(string url, Dictionary<string, string> data)
		{
			var sigString = string.Join(
				"&",
				data
					.Union(data)
					.Select(kvp => string.Format("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
					.OrderBy(s => s)
			);

			var fullSigData = string.Format(
				"{0}&{1}&{2}",
				"POST",
				Uri.EscapeDataString(url),
				Uri.EscapeDataString(sigString.ToString())
			);

			return Convert.ToBase64String(sigHasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSigData.ToString())));
		}

		// Generate the raw OAuth HTML header from the values (including signature).
		string GenerateOAuthHeader(Dictionary<string, string> data)
		{
			return "OAuth " + string.Join(
				", ",
				data
					.Where(kvp => kvp.Key.StartsWith("oauth_"))
					.Select(kvp => string.Format("{0}=\"{1}\"", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
					.OrderBy(s => s)
			);
		}

		// Send HTTP Request and return the response.
		async Task<string> SendRequest(string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
		{
			using (var http = new HttpClient())
			{
				http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);

				var httpResp = await http.PostAsync(fullUrl, formData);
				var respBody = await httpResp.Content.ReadAsStringAsync();

				return respBody;
			}
		}
	}

}
