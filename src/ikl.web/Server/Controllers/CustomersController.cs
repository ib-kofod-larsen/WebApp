using ikl.web.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace ikl.web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> logger;
        private readonly ICollection<Customer> customers;

        public CustomersController(ILogger<CustomersController> logger, ICollection<Customer> customers)
        {
            this.logger = logger;
            this.customers = customers;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return customers.OrderBy(c => c.Year);
        }
    }
}
