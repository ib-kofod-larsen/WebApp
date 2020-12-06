using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ikl.web.Shared;

namespace ikl.web.Client.Shared
{
    public class DataService
    {
        private readonly HttpClient _httpClient;
        private Data _data;
        
        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Data> GetData()
        {
            if (_data != null)
            {
                return _data;
            }
            _data = await _httpClient.GetFromJsonAsync<Data>("Data");
            return _data;
        }
    }
}