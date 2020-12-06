using System.Collections.Generic;

namespace ikl.web.Shared
{
    public class Data
    {
        public Data(Drawing[] drawings, Customer[] customers)
        {
            Drawings = drawings;
            Customers = customers;
        }

        public Drawing[] Drawings { get; }

        public Customer[] Customers { get; }
    }
}