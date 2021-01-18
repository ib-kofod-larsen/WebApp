using System.Collections.Generic;
using System.Linq;
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

        public HashSet<string> Tags = new HashSet<string>();
        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Initialize()
        {
            _data = await _httpClient.GetFromJsonAsync<Data>("data");
            foreach (var drawing in _data.Drawings)
            {
                foreach (var tag in drawing.Tags)
                {
                    Tags.Add(tag);
                }
            }
        }

        public Customer[] GetCustomers()
        {
            return _data.Customers;
        }

        public Customer GetCustomer(string customerId)
        {
            return _data.Customers.First(c => c.Id == customerId);
        }


        public List<Drawing> GetDrawings(string customerId)
        {
            return _data
                .Drawings
                .Where(d => !(d.Ratios.Length == 1 && d.Ratios[0] == "1:1"))
                .Where(d => d.CustomerId.Equals(customerId)).ToList();
        }
        
        public List<Drawing> GetDrawings()
        {
            return _data
                .Drawings
                .Where(d => !(d.Ratios.Length == 1 && d.Ratios[0] == "1:1"))
                .ToList();
        }
        
        public List<Drawing> SearchDrawings(string text)
        {
            return _data
                .Drawings
                .Where(d => !(d.Ratios.Length == 1 && d.Ratios[0] == "1:1"))
                .Where(d =>
                    d.Description.Contains(text) ||
                    d.Title.Contains(text) ||
                    d.Tags.Contains(text) ||
                    d.Path.Contains(text) ||
                    d.Ratios.Any(r => r.Contains(text)))
                .ToList();
        }
    }
}