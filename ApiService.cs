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
           // String url = "http://dfsapi1.hp.gov.in:8082/test_rc_data_api/rest/rc_details/?Username=UnUzSUxnVXJqZTJsK3R6L2NBMWVNZz09&Password=V2QwK3lDMDJ4dUkwc0QrL1JNeVpjZz09&Enter_rc_or_aadhar=32767";

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

