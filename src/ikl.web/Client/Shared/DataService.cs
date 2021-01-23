using System.Collections.Generic;
using System.Linq;
using ikl.web.Shared;

namespace ikl.web.Client.Shared
{
    public class DataService
    {
        private readonly Data _data;

        public DataService(Data data)
        {
            _data = data;
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
                .Where(d => d.CustomerId.Equals(customerId)).ToList();
        }
        
        public List<Drawing> SearchDrawings(string text)
        {
            var input = text.Split(' ');
            return _data
                .Drawings
                .Where(d => Match(d, input)).ToList();
        }

        
        private bool Match(Drawing d, string[] search)
        {
            var customer = GetCustomer(d.CustomerId);
            
            var results = search
                .Select(s => s.ToLower())
                .ToDictionary(s => s, s => false);
            var searchableValues = new HashSet<string>(d.GetSearchableValues()
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(v => v.ToLower()));

            foreach (var s in search)
            {
                if (searchableValues.Any(value => value.Contains(s)))
                {
                    results[s] = true;
                }
            }

            return results.All(p => p.Value);
        }

    }
}