using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikl_web.Data
{
    public class CustomerService
    {
        private readonly ICollection<Customer> _customers;

        public CustomerService(ICollection<Customer> customers)
        {
            _customers = customers;
            foreach (var customer in customers)
            {
                customer.Year = 1900 + customer.Year;
            }
        }

        public Task<ICollection<Customer>> GetCustomers()
        {
            return Task.FromResult<ICollection<Customer>>(_customers.OrderBy(c => c.Year).ToList());
        }
    }
}