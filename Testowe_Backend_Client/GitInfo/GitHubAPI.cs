using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Testowe_Backend_Client.GitInfo
{
    public class GitHubAPI
    {
        private readonly HttpClient _client;
        private readonly string _token;

        public GitHubAPI(string token)
        {
            _client = new HttpClient();
            _token = token;
        }

        // Method to get information about a GitHub repository
        public async Task<GitHubRepository> GetGitHubRepositoryAsync(string owner, string repo)
        {
            // Add the API token to the Authorization header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _token);
            // Set User header - without him the answer would be: Forbidden!
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Testowe_Backend", "1.0.0"));
            // Make API request to get information about the specified repository
            var response = await _client.GetAsync($"https://api.github.com/repos/{owner}/{repo}");

            // Ensure that the API request was successful
            response.EnsureSuccessStatusCode();

            // Read the response as a string
            var responseString = await response.Content.ReadAsStringAsync();

            // Deserialize the response into a GitHubRepository object
            var repository = JsonConvert.DeserializeObject<GitHubRepository>(responseString);

            // Return the GitHubRepository object
            return repository;
        }

    }
}
