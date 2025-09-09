using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace hhh
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }


            public async Task<string> GetApiResponseAsString(string url)
        {
         
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode(); // Throws exception if not successful

                string responseString = await response.Content.ReadAsStringAsync();
           
                return responseString;
            }
            catch (Exception ex)
            {
                // Handle error (optional: log it)
                return $"Error: {ex.Message}";
            }
        }
    }
}

